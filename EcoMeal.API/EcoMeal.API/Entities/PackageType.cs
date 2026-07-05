using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoMeal.API.Entities;
public class PackageType
{
    [Key]
    public int Id { get; set; }
    [MaxLength(20)]
    public required string Name { get; set; }
    public ICollection<Package> Packages { get; set; } = new List<Package>();
}
