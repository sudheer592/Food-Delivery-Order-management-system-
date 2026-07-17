using FoodDelivery.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Api.Data;

public class FoodDeliveryDbContext : DbContext
{
    public FoodDeliveryDbContext(DbContextOptions<FoodDeliveryDbContext> options)
        : base(options)
    {
    }

    public DbSet<Order> Orders => Set<Order>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(order => order.Id);
            entity.Property(order => order.CustomerName).IsRequired().HasMaxLength(150);
            entity.Property(order => order.CustomerPhone).IsRequired().HasMaxLength(20);
            entity.Property(order => order.FoodItem).IsRequired().HasMaxLength(200);
            entity.Property(order => order.DeliveryAddress).IsRequired().HasMaxLength(500);
            entity.Property(order => order.Status).IsRequired().HasMaxLength(30);
            entity.Property(order => order.Price).HasColumnType("decimal(12,2)");
        });
    }
}
