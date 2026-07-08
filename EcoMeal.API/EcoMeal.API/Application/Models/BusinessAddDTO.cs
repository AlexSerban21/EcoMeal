using System.ComponentModel.DataAnnotations.Schema;

namespace EcoMeal.API.Application;

public class BusinessAddDTO
{
    public required string Name { get; set; }
    public required string Adress { get; set; }
    public string? Description { get; set; }
    public required string Contact { get; set; }
    public required int BusinessTypeId { get; set; }
}
