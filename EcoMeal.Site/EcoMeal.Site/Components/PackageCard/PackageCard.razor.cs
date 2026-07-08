using EcoMeal.Site.Models;
using EcoMeal.Site.Services;
using Microsoft.AspNetCore.Components;

namespace EcoMeal.Site.Components.PackageCard;

public partial class PackageCard
{
    [Parameter]
    public required PackageModel Package { get; set; }
    [Inject]
    public required BusinessService BusinessService { get; set; }
    [Inject]
    public required NavigationManager Navigation { get; set; }
}
