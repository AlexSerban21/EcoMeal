using EcoMeal.Site.Models;
using EcoMeal.Site.Services;
using Microsoft.AspNetCore.Components;

namespace EcoMeal.Site.Components.Pages;

public partial class AddBusiness
{
    [Inject]
    public required AppService AppService { get; set; }
    [Inject]
    public required BusinessService BusinessService { get; set; }
    [Inject]
    public required BusinessTypeService BusinessTypeService { get; set; }
    [Inject]
    public required NavigationManager NavigationManager { get; set; }
    [Inject]
    public required CityService CityService { get; set; }
    private List<BusinessTypeModel>? BusinessTypes;
    private List<CityModel>? Cities;
    public BusinessAddModel BusinessAddModel { get; set; } = new BusinessAddModel()
    {
        Name = string.Empty,
        Description = string.Empty,
        Contact = string.Empty
    };
    protected override async Task OnInitializedAsync()
    {
        BusinessTypes = await BusinessTypeService.GetAll();
        BusinessTypes = BusinessTypes ?? new List<BusinessTypeModel>();
        Cities = await CityService.GetAll();
    }
    public async Task AddBusinessInService()
    {
        await BusinessService.Add(BusinessAddModel);
        AppService.Message = "Ai adaugat cu succes restaurantul!";
        NavigationManager.NavigateTo(uri: $"/");
    }
}
