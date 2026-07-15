using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoMeal.API.Entities;
public class Order
{
    [Key]
    public int Id { get; set; }
    public required int UserId { get; set; }
    public required int PackageId { get; set; }
    public required string Status { get; set; }
    public required DateTime Date { get; set; }

    public User? User { get; set; }
    public Package? Package { get; set; }
}
