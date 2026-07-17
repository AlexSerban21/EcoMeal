namespace EcoMeal.Site.Models;
public class BusinessModel
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required string Contact {  get; set; }
    public required string BusinessTypeName { get; set; }
    public required int BusinessTypeId { get; set; }
    public string? Image { get; set; }
    public required string CityName { get; set; }
    public required int CityId { get; set; }
    public required double Rating { get; set; }
}
