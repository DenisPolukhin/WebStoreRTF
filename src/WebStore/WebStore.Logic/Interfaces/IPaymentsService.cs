using WebStore.Logic.Models.Payment;

namespace WebStore.Logic.Interfaces;

public interface IPaymentsService
{
    Task<(string, Guid)> CreatePaidOrderUrl(PaymentModel paymentModel);
}