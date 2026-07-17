using FoodDelivery.Api.DTOs;

namespace FoodDelivery.Api.Services.Interfaces;

public interface IOrderService
{
    Task<IReadOnlyList<OrderDto>> GetOrdersAsync(CancellationToken cancellationToken);
    Task<OrderDto?> GetOrderByIdAsync(int id, CancellationToken cancellationToken);
    Task<IReadOnlyList<OrderDto>> SearchOrdersAsync(string? customerName, string? status, CancellationToken cancellationToken);
    Task<OrderDto> CreateOrderAsync(CreateOrderRequest request, CancellationToken cancellationToken);
    Task<OrderDto?> UpdateOrderAsync(int id, UpdateOrderRequest request, CancellationToken cancellationToken);
    Task<OrderDto?> UpdateOrderStatusAsync(int id, UpdateOrderStatusRequest request, CancellationToken cancellationToken);
    Task<bool> DeleteOrderAsync(int id, CancellationToken cancellationToken);
    Task<OrderSummaryDto> GetSummaryAsync(CancellationToken cancellationToken);
}
