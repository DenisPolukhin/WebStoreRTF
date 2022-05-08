using WebStore.Logic.Models.Order;

namespace WebStore.Logic.Interfaces;

public interface IOrdersService
{
    Task<(Guid, string)> CreateOrderAsync(Guid userId, CreateOrderModel createOrderModel);
    Task<IEnumerable<OrderModel>> GetOrdersAsync(Guid userId);
}