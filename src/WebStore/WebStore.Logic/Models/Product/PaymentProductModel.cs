namespace WebStore.Logic.Models.Product;

public class PaymentProductModel
{
    public Guid Id { get; set; }
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}