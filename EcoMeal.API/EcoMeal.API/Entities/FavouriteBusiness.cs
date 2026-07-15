using System.ComponentModel.DataAnnotations;

namespace EcoMeal.API.Entities;

public class FavouriteBusiness
{
    public int BusinessId { get; set; }
    public int UserId { get; set; }
    public Business? Business { get; set; }
    public User? User { get; set; }
}
