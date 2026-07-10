using EcoMeal.Site.Models;
using EcoMeal.Site.Services;
using Microsoft.AspNetCore.Components;

namespace EcoMeal.Site.Components.Pages;

public partial class UpdateBusiness
{
    [Parameter]
    public int Id { get; set; }
    [Inject]
    public BusinessService BusinessService { get; set; }
    [Inject]
    public NavigationManager NavigationManager { get; set; }
    private List<BusinessTypeModel> BusinessTypes;
    public BusinessAddModel? BusinessAddModel { get; set; } = new BusinessAddModel()
    {
        Name = string.Empty,
        Adress = string.Empty,
        Contact = string.Empty,
        Description = string.Empty,
    };
    protected override async Task OnInitializedAsync()
    {
        Console.WriteLine("222");
        BusinessTypes = await BusinessService.GetBusinessTypes();
        BusinessTypes = BusinessTypes ?? new List<BusinessTypeModel>();
        Console.WriteLine("333");
        var business = await BusinessService.GetOneById(Id);
        BusinessAddModel = new BusinessAddModel
        {
            Name = business.Name,
            Adress = business.Adress,
            Description = business.Description,
            Contact = business.Contact,
            BusinessTypeId = business.BusinessTypeId
        };
    }
    public async Task UpdateBusinessInService()
    {
        await BusinessService.UpdateBusiness(Id, BusinessAddModel);
        NavigationManager.NavigateTo(uri: $"/");
    }
}
