using WebStore.Logic.Models.Product;

namespace WebStore.Logic.Models.Order;

public class CreateOrderModel
{
    public IReadOnlyCollection<ProductInOrderModel> Products { get; set; } = default!;
}