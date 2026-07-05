using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoMeal.API.Entities;

public class Business
{
    [Key]
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Adress { get; set; }
    public string? Description { get; set; }
    public required string Contact { get; set; }
    //[ForeignKey(nameof(BusinessType))] ///nameof verifica daca exista clasa!
    public int BusinessTypeId { get; set; }

    public required BusinessType BusinessType { get; set; }
    public ICollection<Package> Packages = new List<Package>();
}
