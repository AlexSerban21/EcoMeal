using EcoMeal.Site.Models;
using EcoMeal.Site.Services;
using Microsoft.AspNetCore.Components;

namespace EcoMeal.Site.Components.PackageCard;

public partial class PackageCard
{
    [Parameter]
    public required PackageModel Package { get; set; }
    [Inject]
    public required NavigationManager Navigation { get; set; }
    [Parameter]
    public EventCallback<int> OnDelete { get; set; }
    public async Task Delete()
    {
        await OnDelete.InvokeAsync(Package.Id);
    }
    public async Task Update()
    {
        Navigation.NavigateTo($"business/{Package.BusinessId}/updatePackage/{Package.Id}");
    }
}
