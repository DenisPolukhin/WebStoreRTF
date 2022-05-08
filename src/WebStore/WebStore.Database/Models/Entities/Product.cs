namespace WebStore.Database.Models.Entities;

public class Product
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
    public Guid ManufacturerId { get; set; }
    public Manufacturer Manufacturer { get; set; } = default!;
    public Guid CategoryId { get; set; } = default!;
    public Category Category { get; set; } = default!;
    public ICollection<Order> Orders { get; set; } = default!;
    public ICollection<ProductInOrder> ProductInOrders { get; set; } = default!;
    public ICollection<ProductPriceHistory> PriceHistories { get; set; } = default!;
}