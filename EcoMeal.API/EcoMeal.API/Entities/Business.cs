using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoMeal.API.Entities;

public class Business
{
    [Key]
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required string Contact { get; set; }
    //[ForeignKey(nameof(BusinessType))] ///nameof verifica daca exista clasa!
    public int BusinessTypeId { get; set; }
    public int CityId { get; set; }
    public City? City { get; set; }
    public BusinessType? BusinessType { get; set; }
    public ICollection<Package> Packages = new List<Package>();
    public ICollection<FavouriteBusiness> FavouriteBusinesses = new List<FavouriteBusiness>();
    public ICollection<Rating> Ratings = new List<Rating>();
}
