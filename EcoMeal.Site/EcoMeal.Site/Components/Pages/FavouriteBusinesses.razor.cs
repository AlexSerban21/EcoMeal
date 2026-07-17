using EcoMeal.Site.Models;
using EcoMeal.Site.Services;
using Microsoft.AspNetCore.Components;

namespace EcoMeal.Site.Components.Pages;

public partial class FavouriteBusinesses
{
    [Inject]
    public required FavouriteBusinessService FavouriteBusinessService { get; set; }
    private List<BusinessModel> FavouriteBusinessesList { get; set; } = new List<BusinessModel>();
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            FavouriteBusinessesList = await FavouriteBusinessService.GetAll();
            StateHasChanged();
        }
    }
}
