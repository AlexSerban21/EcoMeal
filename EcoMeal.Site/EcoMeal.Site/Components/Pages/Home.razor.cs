using EcoMeal.Site.Models;
using EcoMeal.Site.Services;
using Microsoft.AspNetCore.Components;

namespace EcoMeal.Site.Components.Pages;

public partial class Home
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }
    [Inject]
    public required BusinessTypeService BusinessTypeService { get; set; }
    [Inject]
    public required BusinessService BusinessService { get; set; }
    [Inject]
    public required CityService CityService { get; set; }
    private List<BusinessTypeModel> BusinessTypes { get; set; } = new List<BusinessTypeModel>();
    private List<BusinessModel> Businesses { get; set; } = new List<BusinessModel>();
    private List<CityModel> Cities { get; set; } = new List<CityModel>();
    private int SelectedBusinessTypeId { get; set; } = 0;
    private int SelectedBusinessCity { get; set; } = 0;
    protected override async Task OnInitializedAsync()
    {
        BusinessTypes = await BusinessTypeService.GetAll();
        Businesses = await BusinessService.GetAll(0, 0);
        Cities = await CityService.GetAll();
    }
    public async Task FilterBusinesses()
    {
        Businesses = await BusinessService.GetAll (SelectedBusinessTypeId, SelectedBusinessCity);
    }

    public void NavigateToAddBusiness()
    {
        NavigationManager.NavigateTo(uri: $"addBusiness");
    }
}
