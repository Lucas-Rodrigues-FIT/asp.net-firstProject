using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApplication2.Models;

public class Pizza
{
    public int id { get; set; }
    public string name { get; set; }
    public bool isGlutenFree { get; set; }
    public double price { get; set; }

    [JsonIgnore]
    public List<OrderItem>? orderItems { get; set; }

    public bool validateName()
    {
        return !String.IsNullOrEmpty(this.name);
    }
    public bool isNamedPizza()
    {
        return this.name.Contains("Pizza");
    }
}