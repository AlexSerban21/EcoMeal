using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoMeal.API.Entities;

public class City
{
    [Key]
    public int Id { get; set; }
    public required string Name { get; set; }
    public ICollection<Business> Businesses = new List<Business>();
}
