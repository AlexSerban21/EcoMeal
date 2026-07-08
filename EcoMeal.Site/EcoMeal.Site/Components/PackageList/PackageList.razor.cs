using EcoMeal.Site.Models;
using EcoMeal.Site.Services;
using Microsoft.AspNetCore.Components;

namespace EcoMeal.Site.Components.PackageList;

public partial class PackageList
{
    [Inject]
    public required BusinessService BusinessService { get; set; }
    [Parameter]
    public required int Id { get; set; }
    private List<PackageModel>? Packages { get; set; }
    protected override async Task OnInitializedAsync()
    {
        Packages = await BusinessService.GetPackagesFromBusinessId(Id);
    }
}
