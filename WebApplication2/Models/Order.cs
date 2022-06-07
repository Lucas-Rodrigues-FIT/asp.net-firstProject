using System.ComponentModel.DataAnnotations;
namespace WebApplication2.Models
{
    public class Order
    {
        [Required]
        public int id { get; set; }
        public double totalPrice
        {
            get
            {
                if (orderItems == null)
                    return 0.0;
                double totalPrice = 0.0;
                foreach (var item in orderItems)
                {
                    totalPrice += item.subPrice;
                }
                return totalPrice;
            }
        }

        [Required]
        public List<OrderItem>? orderItems { get; } = new();

    }
}
