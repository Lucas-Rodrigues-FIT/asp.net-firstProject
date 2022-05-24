using System.ComponentModel.DataAnnotations;
namespace WebApplication2.Models
{
    public class Order
    {
        [Required]
        public int id { get; set; }

        [Required]
        public List<OrderItem>? Items { get; }

        public double TotalPrice()
        {
            if(Items == null)
                return 0.0;
            double totalPrice = 0.0;
            foreach (var item in Items)
            {
                totalPrice += item.SubTotalPrice();
            }
            return totalPrice;
        }
    }
}
