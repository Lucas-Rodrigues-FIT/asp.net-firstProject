using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApplication2.Models;

public class OrderItem
{
    [Required]
    public int Id { get; set; }
    [Required]
    public int quantity { get; set; }
    public double subPrice
    {
        get
        {
            if (pizza != null)
                return ((double)(pizza.price * quantity));
            return 0.0;
        }
    }

    public Pizza pizza { get; set; }
    [JsonIgnore]
    public int pizzaId { get; set; }

    [JsonIgnore]
    public Order order { get; set; }
    [JsonIgnore]
    public int orderId { get; set; }

}

