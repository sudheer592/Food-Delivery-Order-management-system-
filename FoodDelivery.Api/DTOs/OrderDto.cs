using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Api.DTOs;

public class OrderDto
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerPhone { get; set; } = string.Empty;
    public string FoodItem { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string DeliveryAddress { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime OrderDate { get; set; }
}

public class CreateOrderRequest
{
    [Required]
    [StringLength(150)]
    public string CustomerName { get; set; } = string.Empty;

    [Required]
    [StringLength(20)]
    public string CustomerPhone { get; set; } = string.Empty;

    [Required]
    [StringLength(200)]
    public string FoodItem { get; set; } = string.Empty;

    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }

    [Range(0.01, double.MaxValue)]
    public decimal Price { get; set; }

    [Required]
    [StringLength(500)]
    public string DeliveryAddress { get; set; } = string.Empty;
}

public class UpdateOrderRequest
{
    [Required]
    [StringLength(150)]
    public string CustomerName { get; set; } = string.Empty;

    [Required]
    [StringLength(20)]
    public string CustomerPhone { get; set; } = string.Empty;

    [Required]
    [StringLength(200)]
    public string FoodItem { get; set; } = string.Empty;

    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }

    [Range(0.01, double.MaxValue)]
    public decimal Price { get; set; }

    [Required]
    [StringLength(500)]
    public string DeliveryAddress { get; set; } = string.Empty;
}

public class UpdateOrderStatusRequest
{
    [Required]
    public string Status { get; set; } = string.Empty;
}

public class OrderSummaryDto
{
    public int TotalOrders { get; set; }
    public int PlacedOrders { get; set; }
    public int PreparingOrders { get; set; }
    public int OutForDeliveryOrders { get; set; }
    public int DeliveredOrders { get; set; }
    public int CancelledOrders { get; set; }
    public decimal TotalRevenue { get; set; }
}
