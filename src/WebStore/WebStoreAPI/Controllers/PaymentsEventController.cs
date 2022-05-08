using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Logic.Interfaces;

namespace WebStore.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PaymentsEventController : ControllerBase
{
    private static readonly IEnumerable<IPAddress> iPAddresses = new[]
    {
        "185.71.76.0/27",
        "77.75.153.0/25",
        "77.75.156.11",
        "77.75.156.35",
        "77.75.154.128/25",
        "2a02:5180:0:1509::/64",
        "2a02:5180:0:2655::/64",
        "2a02:5180:0:1533::/64",
        "2a02:5180:0:2669::/64"
    }.Select(IPAddress.Parse);

    private readonly IPaymentsEventService _paymentsEventService;

    public PaymentsEventController(IPaymentsEventService paymentsEventService)
    {
        _paymentsEventService = paymentsEventService;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> ReceiveMessage(JsonElement message)
    {
        var ipAddress = HttpContext.Connection.RemoteIpAddress;
        if (!iPAddresses.Any(x => x.Equals(ipAddress)))
        {
            return BadRequest();
        }

        await _paymentsEventService.ReceiveMessage(message);
        return Ok();
    }
}