using Microsoft.AspNetCore.Identity;

namespace EcoMeal.API.Entities;

public class User : IdentityUser<int>         //required = nu poate fi NULL 
{
    public string? Name { get; set; }
    public string? Contact { get; set; }
}
