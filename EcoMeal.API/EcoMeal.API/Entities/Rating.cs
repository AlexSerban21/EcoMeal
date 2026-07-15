using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoMeal.API.Entities;

public class Rating
{
    [Key]
    public int Id { get; set; }
    [Range(1, 5)]
    public int Value { get; set; }
    public required int UserId { get; set; }
    public required int BusinessId { get; set; }
    public User? User { get; set; }
    public Business? Business { get; set; }
}
