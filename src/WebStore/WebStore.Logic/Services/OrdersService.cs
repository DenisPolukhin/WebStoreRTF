using Microsoft.EntityFrameworkCore;
using WebStore.Common.Exceptions;
using WebStore.Database.Models;
using WebStore.Database.Models.Entities;
using WebStore.Logic.Interfaces;
using WebStore.Logic.Models.Order;
using WebStore.Logic.Models.Payment;
using WebStore.Logic.Models.Product;

namespace WebStore.Logic.Services;

public class OrdersService : IOrdersService
{
    private readonly IPaymentsService _paymentsService;
    private readonly IDbContextFactory<DatabaseContext> _dbContextFactory;
    public OrdersService(IDbContextFactory<DatabaseContext> dbContextFactory, IPaymentsService paymentsService)
    {
        _dbContextFactory = dbContextFactory;
        _paymentsService = paymentsService;
    }

    public async Task<(Guid, string)> CreateOrderAsync(Guid userId, CreateOrderModel createOrderModel)
    {
        var databaseContext = await _dbContextFactory.CreateDbContextAsync();
        var user = await databaseContext.Users.FindAsync(userId);
        if (user is null)
        {
            throw new EntityFindException();
        }

        var uniqueProductsId = new HashSet<Guid>();
        var allUnique = createOrderModel.Products
            .All(x => uniqueProductsId.Add(x.Id));
        if (!allUnique)
        {
            throw new InvalidOperationException();
        }

        var products = await databaseContext.Products
            .Where(p => uniqueProductsId.Contains(p.Id))
            .ToListAsync();
        if (products.Count != uniqueProductsId.Count)
        {
            throw new InvalidOperationException();
        }

        var productsInOrder = createOrderModel.Products
            .Select(p => new ProductInOrder
            {
                ProductId = p.Id,
                Quantity = p.Quantity
            }).ToList();

        var order = new Order
        {
            User = user,
            ProductsInOrder = productsInOrder
        };

        await databaseContext.Orders.AddAsync(order);

        var paymentModel = new PaymentModel
        {
            OrderId = order.Id,
            UserEmail = user.Email,
            UserPhoneNumber = user.PhoneNumber,
            TotalPrice = order.ProductsInOrder
                .Select(p => p.Quantity * p.Product.Price).Sum(),
            Products = order.ProductsInOrder.Select(x => new PaymentProductModel
            {
                Id = x.ProductId,
                Price = x.Product.Price,
                Description = x.Product.Description,
                Quantity = x.Quantity
            }).ToList()
        };

        var (paymentUrl, paymentId) = await _paymentsService.CreatePaidOrderUrl(paymentModel);
        await databaseContext.SaveChangesAsync();

        return (order.Id, paymentUrl);
    }

    public async Task<IEnumerable<OrderModel>> GetOrdersAsync(Guid userId)
    {
        var databaseContext = await _dbContextFactory.CreateDbContextAsync();
        var orders = await databaseContext.Orders
            .Include(x => x.Payment)
            .Include(x => x.Products)
            .ThenInclude(x => x.Manufacturer)
            .Include(x => x.Products)
            .ThenInclude(x => x.Category)
            .Include(x => x.Products)
            .ThenInclude(x => x.PriceHistories)
            .Where(x => x.UserId == userId)
            .ToListAsync();

        var result = orders.Select(order =>
        {
            var orderModel = new OrderModel
            {
                Id = order.Id,
                CreatedAt = order.CreatedAt.ToDateTimeUtc(),
                PaymentState = order.Payment.State,
                Products = order.Products.Select(pr =>
                {
                    var productModel = new ProductModel
                    {
                        Id = pr.Id,
                        Title = pr.Title,
                        ManufacturerName = pr.Manufacturer.Name,
                        Description = pr.Description,
                        CategoryName = pr.Category.Name,
                        Price = pr.PriceHistories.Where(pr => pr.CreatedAt <= order.CreatedAt)
                            .OrderByDescending(pr => pr.CreatedAt)
                            .FirstOrDefault()?.OldPrice ?? pr.Price
                    };

                    return productModel;
                })
            };

            return orderModel;
        }).ToList();

        return result;
    }
}