using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApplication2.Models;

public class Pizza
{
    public int id { get; set; }
    public string? name { get; set; }
    public bool? isGlutenFree { get; set; }
    public double? price { get; set; }

    [JsonIgnore]
    public List<OrderItem>? orderItem { get; set; }


    public bool isNamedPizza()
    {
        return this.name.Contains("Pizza");
    }
}