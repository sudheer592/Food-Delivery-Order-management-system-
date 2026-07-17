using FoodDelivery.Api.Models;

namespace FoodDelivery.Api.Repositories.Interfaces;

public interface IOrderRepository
{
    Task<IReadOnlyList<Order>> GetAllAsync(CancellationToken cancellationToken);
    Task<Order?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Order>> SearchAsync(string? customerName, string? status, CancellationToken cancellationToken);
    Task<Order> AddAsync(Order order, CancellationToken cancellationToken);
    Task UpdateAsync(Order order, CancellationToken cancellationToken);
    Task DeleteAsync(Order order, CancellationToken cancellationToken);
}
