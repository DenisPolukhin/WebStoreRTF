using WebStore.Logic.Models.Product;

namespace WebStore.Logic.Models.Payment;

public class PaymentModel
{
    public Guid OrderId { get; set; }
    public string? UserEmail { get; set; }
    public string? UserPhoneNumber { get; set; }
    public decimal TotalPrice { get; set; }
    public List<PaymentProductModel> Products { get; set; } = default!;

}