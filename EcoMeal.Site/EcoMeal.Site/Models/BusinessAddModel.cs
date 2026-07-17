using System.ComponentModel.DataAnnotations;

namespace EcoMeal.Site.Models;

public class BusinessAddModel
{
    [Required(ErrorMessage = "Numele este obligatoriu!")]
    [StringLength(50, ErrorMessage = "Numele poate avea maximum 50 de caractere.")]
    public required string Name { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Alege un oraș.")]
    public int CityId { get; set; }

    [Required(ErrorMessage = "Descrierea este obligatorie!")]
    [StringLength(500, ErrorMessage = "Descrierea poate avea maximum 500 de caractere.")]
    public required string Description { get; set; }

    [Required(ErrorMessage = "Contactul este obligatoriu!")]
    [StringLength(100, ErrorMessage = "Contactul poate avea maximum 100 de caractere.")]
    public required string Contact { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Alege un tip de restaurant.")]
    public int BusinessTypeId { get; set; }
}