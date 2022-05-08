using System.Text.Json;

namespace WebStore.Logic.Interfaces;

public interface IPaymentsEventService
{
    Task ReceiveMessage(JsonElement element);
}