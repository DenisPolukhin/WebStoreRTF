namespace WebStore.Logic.Models.Product;

public class CreateProductModel
{
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
    public Guid ManufacturerId { get; set; }
    public Guid CategoryId { get; set; }
}