using System.ComponentModel.DataAnnotations.Schema;

namespace EcoMeal.API.Application;

public class BusinessDTO
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required string Contact { get; set; }
    public required string BusinessTypeName { get; set; }
    public required int BusinessTypeId { get; set; }
    public required int CityId { get; set; }
    public string? Image { get; set; }
    public string? CityName { get; set; }
    public double Rating { get; set; } = 0;
}
