using NodaTime;

namespace WebStore.Database.Models.Entities;

public class ProductPriceHistory
{
    public Guid Id { get; set; }
    public Instant CreatedAt { get; set; } = SystemClock.Instance.GetCurrentInstant();
    public decimal OldPrice { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = default!;
}