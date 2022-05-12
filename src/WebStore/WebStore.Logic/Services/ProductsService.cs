using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using WebStore.Common.Exceptions;
using WebStore.Common.Helpers;
using WebStore.Common.Models;
using WebStore.Database.Models;
using WebStore.Database.Models.Entities;
using WebStore.Logic.Interfaces;
using WebStore.Logic.Models.Category;
using WebStore.Logic.Models.Product;

namespace WebStore.Logic.Services;

public class ProductsService : IProductsService
{
    private readonly IDbContextFactory<DatabaseContext> _dbContextFactory;
    private readonly IMapper _mapper;

    public ProductsService(IDbContextFactory<DatabaseContext> dbContextFactory, IMapper mapper)
    {
        _dbContextFactory = dbContextFactory;
        _mapper = mapper;
    }

    public async Task<IPaginatedList<ProductModel>> GetCategoryProductsAsync(Guid categoryId, PageModel pageModel)
    {
        var databaseContext = await _dbContextFactory.CreateDbContextAsync();
        var products = await databaseContext.Products
            .AsNoTracking()
            .Include(p => p.Category)
            .Include(p => p.Manufacturer)
            .Where(p => p.CategoryId == categoryId)
            .ProjectTo<ProductModel>(_mapper.ConfigurationProvider)
            .ToPaginatedList(pageModel.Page, pageModel.Size);

        return products;
    }

    public async Task<ProductModel> GetDetailsAsync(Guid id)
    {
        var databaseContext = await _dbContextFactory.CreateDbContextAsync();
        var product = await databaseContext.Products
            .Include(x => x.Category)
            .Include(x => x.Manufacturer)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (product is null)
        {
            throw new EntityFindException();
        }

        return _mapper.Map<ProductModel>(product);
    }

    public async Task<IEnumerable<CategoryModel>> GetCategoriesAsync()
    {
        var databaseContext = await _dbContextFactory.CreateDbContextAsync();
        var categories = await databaseContext.Categories
            .Include(x => x.ChildCategories)
            .ToListAsync();

        var categoryHierarchy = categories
            .Where(x => x.ParentCategoryId == null)
            .ToList();

        return _mapper.Map<IEnumerable<CategoryModel>>(categoryHierarchy);
    }

    public async Task<IPaginatedList<ProductModel>> SearchByTitleAsync(string title, PageModel pageModel)
    {
        var databaseContext = await _dbContextFactory.CreateDbContextAsync();
        var searchTextToLower = title.Trim().ToLower();
        var products = await databaseContext.Products
            .AsNoTracking()
            .Include(x => x.Category)
            .Include(x => x.Manufacturer)
            .Where(x => x.Title.ToLower().Contains(searchTextToLower) ||
                        x.Category.Name.ToLower().Contains(searchTextToLower))
            .ProjectTo<ProductModel>(_mapper.ConfigurationProvider)
            .ToPaginatedList(pageModel.Page, pageModel.Size);

        return products;
    }

    public async Task CreateAsync(CreateProductModel createProductModel)
    {
        var databaseContext = await _dbContextFactory.CreateDbContextAsync();
        var category = await databaseContext.Categories.FindAsync(createProductModel.CategoryId) ??
                       throw new EntityFindException();

        var manufacturer = await databaseContext.Manufacturers.FindAsync(createProductModel.ManufacturerId) ??
                           throw new EntityFindException();

        var product = _mapper.Map<Product>(createProductModel);

        await databaseContext.AddAsync(product);
        await databaseContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Guid id, UpdateProductModel updateProductModel)
    {
        var databaseContext = await _dbContextFactory.CreateDbContextAsync();
        var product = await databaseContext.Products.FindAsync(id);
        if (product is null)
        {
            throw new EntityFindException();
        }

        if (updateProductModel.CategoryId != product.CategoryId)
        {
            var _ = await databaseContext.Categories.FindAsync(updateProductModel.CategoryId) ??
                    throw new EntityFindException();
        }

        if (product.ManufacturerId != updateProductModel.ManufacturerId)
        {
            var _ = await databaseContext.Manufacturers.FindAsync(updateProductModel.ManufacturerId) ??
                    throw new EntityFindException();
        }

        if (product.Price != updateProductModel.Price)
        {
            var priceHistory = new ProductPriceHistory
            {
                OldPrice = product.Price,
                ProductId = product.Id
            };

            await databaseContext.ProductPriceHistories.AddAsync(priceHistory);
        }

        _mapper.Map(updateProductModel, product);
        await databaseContext.SaveChangesAsync();
    }
}