using System.ComponentModel.DataAnnotations;

namespace EcoMeal.API.Application.Models;

public class PackageTypeDTO
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Image { get; set; }
}
