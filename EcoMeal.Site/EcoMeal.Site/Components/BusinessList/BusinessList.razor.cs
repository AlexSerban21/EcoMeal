using EcoMeal.Site.Models;
using EcoMeal.Site.Services;
using Microsoft.AspNetCore.Components;

namespace EcoMeal.Site.Components.BusinessList;
public partial class BusinessList
{
    [Inject]
    public required BusinessService BusinessService { get; set; }
    private List<BusinessModel>? Businesses;
    protected override async Task OnInitializedAsync()
    {
        Businesses = await BusinessService.GetAll();
    }
    public async Task DeleteBusiness(int id)
    {
        await BusinessService.Delete(id);
        if (Businesses != null)
        {
            Businesses.RemoveAll (b => b.Id == id);
        }
    }
}
