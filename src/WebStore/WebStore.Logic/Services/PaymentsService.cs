using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebStore.Database.Models;
using WebStore.Database.Models.Enums;
using WebStore.Logic.Interfaces;
using WebStore.Logic.Models.Configuration;
using WebStore.Logic.Models.Payment;
using Yandex.Checkout.V3;
using Payment = WebStore.Database.Models.Entities.Payment;

namespace WebStore.Logic.Services;

public class PaymentsService : IPaymentsService
{
    private readonly AsyncClient _client;
    private readonly IDbContextFactory<DatabaseContext> _dbContextFactory;

    public PaymentsService(IDbContextFactory<DatabaseContext> dbContextFactory, IConfiguration configuration)
    {
        _dbContextFactory = dbContextFactory;
        var yookassaConfig = configuration.GetSection(nameof(YookassaConfig))
            .Get<YookassaConfig>();
        _client = new Client(yookassaConfig.ShopId, yookassaConfig.SecretKey).MakeAsync();
    }

    public async Task<(string, Guid)> CreatePaidOrderUrl(PaymentModel paymentModel)
    {
        var databaseContext = await _dbContextFactory.CreateDbContextAsync();
        var newPayment = new NewPayment
        {
            Amount = new Amount
            {
                Currency = "RUB",
                Value = paymentModel.TotalPrice
            },
            Capture = true,
            Description = $"Payment for the order {paymentModel.OrderId} in the store",
            Confirmation = new Confirmation
            {
                Type = ConfirmationType.Redirect,
                ReturnUrl = "https://www.google.com/"
            },

            Receipt = new Receipt
            {
                Email = paymentModel.UserEmail,
                Phone = paymentModel.UserPhoneNumber,
                Items = paymentModel.Products.Select(x => new ReceiptItem
                {
                    Description = x.Description,
                    Quantity = x.Quantity,
                    VatCode = VatCode.NoVat,
                    ProductCode = x.Id.ToString(),
                    PaymentSubject = PaymentSubject.Commodity,
                    PaymentMode = PaymentMode.FullPrepayment,
                    Amount = new Amount
                    {
                        Currency = "RUB",
                        Value = x.Price
                    }
                }).ToList()
            }
        };

        var yookassaPayment = await _client.CreatePaymentAsync(newPayment);
        var payment = new Payment
        {
            YookassaPaymentId = yookassaPayment.Id,
            Amount = yookassaPayment.Amount.Value,
            State = PaymentState.Pending,
            OrderId = paymentModel.OrderId
        };
        await databaseContext.Payments.AddAsync(payment);
        await databaseContext.SaveChangesAsync();

        return (yookassaPayment.Confirmation.ConfirmationUrl, payment.Id);
    }
}