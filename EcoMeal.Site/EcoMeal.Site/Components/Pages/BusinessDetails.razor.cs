using EcoMeal.Site.Models;
using EcoMeal.Site.Services;
using Microsoft.AspNetCore.Components;

namespace EcoMeal.Site.Components.Pages;

public partial class BusinessDetails
{
    [Parameter]
    public int Id { get; set; }

    [Inject]
    public required BusinessService BusinessService { get; set; }
    [Inject]
    public required PackageService PackageService { get; set; }
    [Inject]
    public required PackageTypeService PackageTypeService { get; set; }

    private BusinessModel? Business;
    private List<PackageModel>? Packages;
    private List<PackageTypesModel>? PackageTypes;
    private int SelectedPackageTypeId = 0;
    private int MaxPrice = 0;
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            Business = await BusinessService.GetOneById(Id);
            Packages = await PackageService.GetFromBusinessId(Id, 0, 0);
            PackageTypes = await PackageTypeService.GetAll();
            StateHasChanged();
        }
    }
    public async Task FilterPackages ()
    {
        Packages = await PackageService.GetFromBusinessId(Id, SelectedPackageTypeId, MaxPrice);
        StateHasChanged();
    }
    public void NavigateToAddPackage()
    {
        NavigationManager.NavigateTo(uri: $"business/{Id}/addPackage");
    }
}
