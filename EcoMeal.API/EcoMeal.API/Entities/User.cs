using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoMeal.API.Entities;

public class User //required = nu poate fi NULL 
{
    [Key]
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Contact { get; set; }
}
