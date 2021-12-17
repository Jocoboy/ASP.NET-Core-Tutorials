namespace ContosoPizza.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
public class Topping
{

    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string? Name { get; set; }

    public decimal Calories { get; set; }
     [JsonIgnore]
    public ICollection<Pizza>? Pizzas { get; set; }
}