using Microsoft.AspNetCore.Identity;

namespace EcoMeal.API.Entities;

public class User : IdentityUser<int>         //required = nu poate fi NULL 
{
    public string? Name { get; set; }
    public string? Contact { get; set; }
    public ICollection<Order> Orders { get; set; } = new List<Order>();
    public ICollection<FavouriteBusiness> FavouriteBusinesses { get; set; } = new List<FavouriteBusiness>();
    public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
}
