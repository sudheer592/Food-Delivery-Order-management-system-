using FoodDelivery.Api.DTOs;
using FoodDelivery.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<OrderDto>>> GetOrders(CancellationToken cancellationToken)
    {
        var orders = await _orderService.GetOrdersAsync(cancellationToken);
        return Ok(orders);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<OrderDto>> GetOrder(int id, CancellationToken cancellationToken)
    {
        var order = await _orderService.GetOrderByIdAsync(id, cancellationToken);
        if (order is null)
        {
            return NotFound(new { message = $"Order with id {id} was not found." });
        }

        return Ok(order);
    }

    [HttpGet("search")]
    public async Task<ActionResult<IReadOnlyList<OrderDto>>> SearchOrders([FromQuery] string? customerName, [FromQuery] string? status, CancellationToken cancellationToken)
    {
        var orders = await _orderService.SearchOrdersAsync(customerName, status, cancellationToken);
        return Ok(orders);
    }

    [HttpPost]
    public async Task<ActionResult<OrderDto>> CreateOrder([FromBody] CreateOrderRequest request, CancellationToken cancellationToken)
    {
        var order = await _orderService.CreateOrderAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<OrderDto>> UpdateOrder(int id, [FromBody] UpdateOrderRequest request, CancellationToken cancellationToken)
    {
        var order = await _orderService.UpdateOrderAsync(id, request, cancellationToken);
        if (order is null)
        {
            return NotFound(new { message = $"Order with id {id} was not found." });
        }

        return Ok(order);
    }

    [HttpPatch("{id:int}/status")]
    public async Task<ActionResult<OrderDto>> UpdateOrderStatus(int id, [FromBody] UpdateOrderStatusRequest request, CancellationToken cancellationToken)
    {
        var order = await _orderService.UpdateOrderStatusAsync(id, request, cancellationToken);
        if (order is null)
        {
            return NotFound(new { message = $"Order with id {id} was not found." });
        }

        return Ok(order);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteOrder(int id, CancellationToken cancellationToken)
    {
        var deleted = await _orderService.DeleteOrderAsync(id, cancellationToken);
        if (!deleted)
        {
            return NotFound(new { message = $"Order with id {id} was not found." });
        }

        return NoContent();
    }

    [HttpGet("summary")]
    public async Task<ActionResult<OrderSummaryDto>> GetSummary(CancellationToken cancellationToken)
    {
        var summary = await _orderService.GetSummaryAsync(cancellationToken);
        return Ok(summary);
    }
}
