namespace WebStore.Logic.Models.Order;

public record CreateOrderReturnModel(Guid OrderId, string PaymentUrl);