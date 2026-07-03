namespace EcoMeal.API.Entities;

public class User //required = nu poate fi NULL 
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Contact { get; set; }
}
