using System.ComponentModel.DataAnnotations;

namespace EcoMeal.API.Application.Models;

public class BusinessTypeDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
}
