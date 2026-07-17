using System.ComponentModel.DataAnnotations;

namespace EcoMeal.Site.Models.Auth;

public class UpdateModel
{
    [Required(ErrorMessage = "Emailul este necesar")]
    [EmailAddress(ErrorMessage = "Email invalid")]
    public string Email { get; set; } = string.Empty;

    public string CurrentPassword { get; set; } = string.Empty;


    public string Password { get; set; } = string.Empty;

    public string ConfirmPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "Numele este necesar")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Contactul este necesar")]
    public string Contact { get; set; } = string.Empty;
}
