namespace EcoMeal.API.Application.Models;

public class PackageGetDTO
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string? Description { get; set; }
    public double Price { get; set; }
    public DateTime StartPickup { get; set; }
    public DateTime EndPickup { get; set; }
    public string? PackageType { get; set; }
    public int PackageTypeId { get; set; }
    public int BusinessId { get; set; }
}
