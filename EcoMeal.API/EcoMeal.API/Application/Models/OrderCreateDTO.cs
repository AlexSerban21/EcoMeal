namespace EcoMeal.API.Application.Models;

public class OrderCreateDTO
{
    public int PackageId { get; set; }

    public int Id { get; set; }
    public required string PackageName { get; set; }
    public required string Status { get; set; }
    public double Price { get; set; }
    public int BusinessId { get; set; }
    public required string BusinessName { get; set; }
    public DateTime Date { get; set; }
    public string? UserName { get; set; }
    public string? UserContact { get; set; }
}
