using EcoMeal.Site.Models;
using EcoMeal.Site.Services;
using Microsoft.AspNetCore.Components;

namespace EcoMeal.Site.Components.BusinessList;
public partial class BusinessList
{
    [Inject]
    public required BusinessService BusinessService { get; set; }
    private List <BusinessModel>? Businesses { get; set; }
    protected override async Task OnInitializedAsync()
    {
        Console.WriteLine("c");
        Businesses = await BusinessService.GetAllSync();
    }
    public async Task DeleteBusiness(int id)
    {
        await BusinessService.DeleteAsync(id);
        Console.WriteLine("a");
        if (Businesses != null)
        {
            Businesses.RemoveAll (b => b.Id == id);
        }
    }
}
