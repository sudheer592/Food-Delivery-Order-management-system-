namespace FoodDelivery.Api.Models;

public class Order
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerPhone { get; set; } = string.Empty;
    public string FoodItem { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string DeliveryAddress { get; set; } = string.Empty;
    public string Status { get; set; } = "Placed";
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
}
