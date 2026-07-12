using System.ComponentModel.DataAnnotations;

namespace EcoMeal.Site.Models.Auth;

public class RegisterModel
{
    [Required(ErrorMessage = "Emailul este necesar")]
    [EmailAddress(ErrorMessage = "Email invalid")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Parola este necesară")]
    [MinLength(6, ErrorMessage = "Parola trebuie să aibă cel puțin 6 litere")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Confirmă parola")]
    [Compare(nameof(Password), ErrorMessage = "Parolele nu sunt identice")]
    public string ConfirmPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "Numele este necesar")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Contactul este necesar")]
    public string Contact { get; set; } = string.Empty;
}
