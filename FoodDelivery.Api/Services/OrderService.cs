using System.ComponentModel.DataAnnotations;
using FoodDelivery.Api.DTOs;
using FoodDelivery.Api.Models;
using FoodDelivery.Api.Repositories.Interfaces;
using FoodDelivery.Api.Services.Interfaces;

namespace FoodDelivery.Api.Services;

public class OrderService : IOrderService
{
    private static readonly string[] AllowedStatuses = { "Placed", "Preparing", "OutForDelivery", "Delivered", "Cancelled" };
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<IReadOnlyList<OrderDto>> GetOrdersAsync(CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.GetAllAsync(cancellationToken);
        return orders.Select(MapToDto).ToList();
    }

    public async Task<OrderDto?> GetOrderByIdAsync(int id, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(id, cancellationToken);
        return order is null ? null : MapToDto(order);
    }

    public async Task<IReadOnlyList<OrderDto>> SearchOrdersAsync(string? customerName, string? status, CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.SearchAsync(customerName, status, cancellationToken);
        return orders.Select(MapToDto).ToList();
    }

    public async Task<OrderDto> CreateOrderAsync(CreateOrderRequest request, CancellationToken cancellationToken)
    {
        ValidateCreateRequest(request);

        var order = new Order
        {
            CustomerName = request.CustomerName.Trim(),
            CustomerPhone = request.CustomerPhone.Trim(),
            FoodItem = request.FoodItem.Trim(),
            Quantity = request.Quantity,
            Price = request.Price,
            DeliveryAddress = request.DeliveryAddress.Trim(),
            Status = "Placed"
        };

        var createdOrder = await _orderRepository.AddAsync(order, cancellationToken);
        return MapToDto(createdOrder);
    }

    public async Task<OrderDto?> UpdateOrderAsync(int id, UpdateOrderRequest request, CancellationToken cancellationToken)
    {
        ValidateUpdateRequest(request);

        var existingOrder = await _orderRepository.GetByIdAsync(id, cancellationToken);
        if (existingOrder is null)
        {
            return null;
        }

        existingOrder.CustomerName = request.CustomerName.Trim();
        existingOrder.CustomerPhone = request.CustomerPhone.Trim();
        existingOrder.FoodItem = request.FoodItem.Trim();
        existingOrder.Quantity = request.Quantity;
        existingOrder.Price = request.Price;
        existingOrder.DeliveryAddress = request.DeliveryAddress.Trim();

        await _orderRepository.UpdateAsync(existingOrder, cancellationToken);
        return MapToDto(existingOrder);
    }

    public async Task<OrderDto?> UpdateOrderStatusAsync(int id, UpdateOrderStatusRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Status))
        {
            throw new ValidationException("Status is required.");
        }

        var normalizedStatus = request.Status.Trim();
        if (!AllowedStatuses.Contains(normalizedStatus, StringComparer.OrdinalIgnoreCase))
        {
            throw new ValidationException($"Status must be one of: {string.Join(", ", AllowedStatuses)}.");
        }

        var existingOrder = await _orderRepository.GetByIdAsync(id, cancellationToken);
        if (existingOrder is null)
        {
            return null;
        }

        existingOrder.Status = normalizedStatus;
        await _orderRepository.UpdateAsync(existingOrder, cancellationToken);
        return MapToDto(existingOrder);
    }

    public async Task<bool> DeleteOrderAsync(int id, CancellationToken cancellationToken)
    {
        var existingOrder = await _orderRepository.GetByIdAsync(id, cancellationToken);
        if (existingOrder is null)
        {
            return false;
        }

        await _orderRepository.DeleteAsync(existingOrder, cancellationToken);
        return true;
    }

    public async Task<OrderSummaryDto> GetSummaryAsync(CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.GetAllAsync(cancellationToken);

        return new OrderSummaryDto
        {
            TotalOrders = orders.Count,
            PlacedOrders = orders.Count(order => order.Status.Equals("Placed", StringComparison.OrdinalIgnoreCase)),
            PreparingOrders = orders.Count(order => order.Status.Equals("Preparing", StringComparison.OrdinalIgnoreCase)),
            OutForDeliveryOrders = orders.Count(order => order.Status.Equals("OutForDelivery", StringComparison.OrdinalIgnoreCase)),
            DeliveredOrders = orders.Count(order => order.Status.Equals("Delivered", StringComparison.OrdinalIgnoreCase)),
            CancelledOrders = orders.Count(order => order.Status.Equals("Cancelled", StringComparison.OrdinalIgnoreCase)),
            TotalRevenue = orders.Sum(order => order.Price)
        };
    }

    private static void ValidateCreateRequest(CreateOrderRequest request)
    {
        var validationResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(request, new ValidationContext(request), validationResults, true);

        if (!isValid)
        {
            throw new ValidationException(validationResults.First().ErrorMessage);
        }

        if (string.IsNullOrWhiteSpace(request.CustomerName) ||
            string.IsNullOrWhiteSpace(request.CustomerPhone) ||
            string.IsNullOrWhiteSpace(request.FoodItem) ||
            string.IsNullOrWhiteSpace(request.DeliveryAddress))
        {
            throw new ValidationException("All required fields must be provided.");
        }
    }

    private static void ValidateUpdateRequest(UpdateOrderRequest request)
    {
        var validationResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(request, new ValidationContext(request), validationResults, true);

        if (!isValid)
        {
            throw new ValidationException(validationResults.First().ErrorMessage);
        }

        if (string.IsNullOrWhiteSpace(request.CustomerName) ||
            string.IsNullOrWhiteSpace(request.CustomerPhone) ||
            string.IsNullOrWhiteSpace(request.FoodItem) ||
            string.IsNullOrWhiteSpace(request.DeliveryAddress))
        {
            throw new ValidationException("All required fields must be provided.");
        }
    }

    private static OrderDto MapToDto(Order order)
    {
        return new OrderDto
        {
            Id = order.Id,
            CustomerName = order.CustomerName,
            CustomerPhone = order.CustomerPhone,
            FoodItem = order.FoodItem,
            Quantity = order.Quantity,
            Price = order.Price,
            DeliveryAddress = order.DeliveryAddress,
            Status = order.Status,
            OrderDate = order.OrderDate
        };
    }
}
