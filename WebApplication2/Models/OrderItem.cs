using System.ComponentModel.DataAnnotations;
namespace WebApplication2.Models;

public class OrderItem
{
    [Required]
    public int Id { get; set; }

    [Required]
    public Pizza? pizza { get; set; }

    [Required]
    public int quantity { get; set; }

    public double SubTotalPrice()
    {
        if(pizza != null)
            return (pizza.price * quantity);
        return 0.0;
    }
}
