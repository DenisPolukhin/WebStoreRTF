using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using WebStore.Logic.Interfaces;
using WebStore.Logic.Models.Order;

namespace WebStore.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrdersService _ordersService;

    public OrdersController(IOrdersService ordersService)
    {
        _ordersService = ordersService;
    }

    private Guid UserId => Guid.ParseExact(User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value, "D");

    [HttpPost]
    public async Task<IActionResult> CreateOrder(CreateOrderModel createOrderModel)
    {
        var (orderId, paymentUrl) = await _ordersService.CreateOrderAsync(UserId, createOrderModel);
        var result = new CreateOrderReturnModel(orderId, paymentUrl);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        var result = await _ordersService.GetOrdersAsync(UserId);
        return Ok(result);
    }
}