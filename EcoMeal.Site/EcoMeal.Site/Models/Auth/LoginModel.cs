using System.ComponentModel.DataAnnotations;

namespace EcoMeal.Site.Models.Auth;

public class LoginModel
{
    [Required(ErrorMessage = "Emailul este necesar")]
    [EmailAddress(ErrorMessage = "Email Invalid")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Parola este necesară")]
    public string Password { get; set; } = string.Empty;
}
