using EcoMeal.Site.Models;
using EcoMeal.Site.Services;
using Microsoft.AspNetCore.Components;

namespace EcoMeal.Site.Components.Pages;

public partial class AddBusiness
{
    [Inject]
    public BusinessService BusinessService { get; set; }
    [Inject]
    public NavigationManager NavigationManager { get; set; }
    private List<BusinessTypeModel> BusinessTypes;
    public BusinessAddModel BusinessAddModel { get; set; } = new BusinessAddModel()
    {
        Name = string.Empty,
        Adress = string.Empty,
        Description = string.Empty,
        Contact = string.Empty
    };
    protected override async Task OnInitializedAsync()
    {
        BusinessTypes = await BusinessService.GetBusinessTypes();
        BusinessTypes = BusinessTypes ?? new List<BusinessTypeModel>();
        Console.WriteLine("999");
    }
    public async Task AddBusinessInService()
    {
        await BusinessService.AddBusiness(BusinessAddModel);
        NavigationManager.NavigateTo(uri: $"/");
    }
}
