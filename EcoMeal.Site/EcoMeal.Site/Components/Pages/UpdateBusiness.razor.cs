using EcoMeal.Site.Models;
using EcoMeal.Site.Services;
using Microsoft.AspNetCore.Components;

namespace EcoMeal.Site.Components.Pages;

public partial class UpdateBusiness
{
    [Parameter]
    public int Id { get; set; }
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
    public BusinessAddModel? BusinessAddModel { get; set; } = new BusinessAddModel()
    {
        Name = string.Empty,
        Contact = string.Empty,
        Description = string.Empty,
    };
    protected override async Task OnInitializedAsync()
    {
        BusinessTypes = await BusinessTypeService.GetAll();
        BusinessTypes = BusinessTypes ?? new List<BusinessTypeModel>();
        Cities = await CityService.GetAll();
        var business = await BusinessService.GetOneById(Id);
        BusinessAddModel = new BusinessAddModel
        {
            Name = business.Name,
            Description = business.Description,
            Contact = business.Contact,
            BusinessTypeId = business.BusinessTypeId,
            CityId = business.CityId
        };
    }
    public async Task UpdateBusinessInService()
    {
        await BusinessService.Update(Id, BusinessAddModel);
        AppService.Message = "Ai editat cu succes restaurantul!";
        NavigationManager.NavigateTo(uri: $"/");
    }
}
