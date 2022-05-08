using WebStore.Database.Models.Enums;
using WebStore.Logic.Models.Product;

namespace WebStore.Logic.Models.Order;

public class OrderModel
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public PaymentState PaymentState { get; set; }
    public IEnumerable<ProductModel> Products { get; set; } = default!;
}