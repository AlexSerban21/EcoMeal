using System.ComponentModel.DataAnnotations;

namespace EcoMeal.Site.Models;

public class BusinessAddModel
{
    [Required(ErrorMessage = "Numele este obligatoriu!")]
    [StringLength(50)]
    public required string Name { get; set; }
    [Required(ErrorMessage = "Adresa este obligatoriu!")]
    [StringLength(50)]
    public required string Adress { get; set; }
    [Required(ErrorMessage = "Descrierea este obligatoriu!")]
    [StringLength(50)]
    public string? Description { get; set; }
    [Required(ErrorMessage = "Contact este obligatoriu!")]
    [StringLength(50)]
    public required string Contact { get; set; }
    [Range(1, 20, ErrorMessage = "Alege un tip de business.")]
    public int BusinessTypeId { get; set; }
}
