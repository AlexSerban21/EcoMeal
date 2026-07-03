using System.ComponentModel.DataAnnotations;

namespace EcoMeal.API.Entities;
public class PackageType
{
    [Key]
    public int Id { get; set; }
    [MaxLength(20)]
    public required string Name { get; set; }
}
