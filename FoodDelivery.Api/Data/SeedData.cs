using FoodDelivery.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Api.Data;

public static class SeedData
{
    public static async Task InitializeAsync(FoodDeliveryDbContext context)
    {
        if (await context.Orders.AnyAsync())
        {
            return;
        }

        var orders = new List<Order>
        {
            new()
            {
                CustomerName = "Asha Kumar",
                CustomerPhone = "9876543210",
                FoodItem = "Veggie Burger",
                Quantity = 2,
                Price = 18.50m,
                DeliveryAddress = "12, Main Street, Coimbatore",
                Status = "Placed",
                OrderDate = DateTime.UtcNow.AddHours(-2)
            },
            new()
            {
                CustomerName = "Ravi Menon",
                CustomerPhone = "9123456780",
                FoodItem = "Chicken Pizza",
                Quantity = 1,
                Price = 22.00m,
                DeliveryAddress = "45, Park Avenue, Chennai",
                Status = "Preparing",
                OrderDate = DateTime.UtcNow.AddHours(-5)
            }
        };

        await context.Orders.AddRangeAsync(orders);
        await context.SaveChangesAsync();
    }
}
