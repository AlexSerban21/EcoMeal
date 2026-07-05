using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoMeal.API.Entities;

public class Package
{
    [Key]
    public int Id { get; set; }
    public required int No_Package { get; set; }
    public required int BusinessId { get; set; }
    public required int PackageTypeId { get; set; }
    public string? Description { get; set; }
    public required int Price { get; set; }
    public required DateTime StartRidicare { get; set; }
    public required DateTime EndRidicare { get; set; }
    public required Business Business { get; set; }
    public required PackageType PackageType { get; set; }
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
