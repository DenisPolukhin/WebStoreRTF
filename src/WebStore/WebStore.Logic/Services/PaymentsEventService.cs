using System.Text.Json;
using WebStore.Logic.Interfaces;

namespace WebStore.Logic.Services;

public class PaymentsEventService : IPaymentsEventService
{
    public Task ReceiveMessage(JsonElement element)
    {
        throw new NotImplementedException();
    }
}