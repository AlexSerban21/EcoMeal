using EcoMeal.Site.Models;
using EcoMeal.Site.Services;
using Microsoft.AspNetCore.Components;

namespace EcoMeal.Site.Components.Pages;

public partial class AddBusiness
{
    [Inject]
    public BusinessService BusinessService { get; set; }
    [Inject]
    public BusinessTypeService BusinessTypeService { get; set; }
    [Inject]
    public NavigationManager NavigationManager { get; set; }
    private List<BusinessTypeModel>? BusinessTypes;
    public BusinessAddModel BusinessAddModel { get; set; } = new BusinessAddModel()
    {
        Name = string.Empty,
        Adress = string.Empty,
        Description = string.Empty,
        Contact = string.Empty
    };
    protected override async Task OnInitializedAsync()
    {
        BusinessTypes = await BusinessTypeService.GetAll();
        BusinessTypes = BusinessTypes ?? new List<BusinessTypeModel>();
    }
    public async Task AddBusinessInService()
    {
        await BusinessService.Add(BusinessAddModel);
        NavigationManager.NavigateTo(uri: $"/");
    }
}
