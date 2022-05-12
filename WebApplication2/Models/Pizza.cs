using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models;

public class Pizza
{
    [Required]
    public int id { get; set; }

    [Required]
    public string? name { get; set; }

    [Required]
    public bool isGlutenFree { get; set; }

}