using System.ComponentModel.DataAnnotations;

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

    public int orderItemId { get; set; }


    public bool isNamedPizza()
    {
        return this.name.Contains("Pizza");
    }
}