using NodaTime;
using WebStore.Database.Models.Enums;

namespace WebStore.Database.Models.Entities;

public class Payment
{
    public Guid Id { get; set; }
    public Instant CreatedAt { get; set; } = SystemClock.Instance.GetCurrentInstant();
    public Guid OrderId { get; set; }
    public Order Order { get; set; } = default!;
    public string YookassaPaymentId { get; set; } = default!;
    public PaymentState State { get; set; }
    public decimal Amount { get; set; }
}