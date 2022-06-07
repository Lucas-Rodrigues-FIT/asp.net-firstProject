using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApplication2.Models;

public class Pizza
{
    [Required]
    public int id { get; set; }
    [Required]
    public string? name { get; set; }
    [Required]
    public bool? isGlutenFree { get; set; }
    [Required]
    public double? price { get; set; }

    [JsonIgnore]
    public List<OrderItem> orderItem { get;}


    public bool isNamedPizza()
    {
        return this.name.Contains("Pizza");
    }
}