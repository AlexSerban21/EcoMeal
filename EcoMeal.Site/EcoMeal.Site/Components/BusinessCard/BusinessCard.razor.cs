using EcoMeal.Site.Models;
using EcoMeal.Site.Services;
using Microsoft.AspNetCore.Components;

namespace EcoMeal.Site.Components.BusinessCard;
public partial class BusinessCard
{
    [Parameter]
    public required BusinessModel Business {  get; set; }
    [Parameter]
    public EventCallback<int> OnDelete { get; set; }
    [Inject]
    public required NavigationManager Navigation {  get; set; }
    [Inject]
    public required FavouriteBusinessService FavouriteBusinesses { get; set; }
    [Inject]
    public required AuthService AuthService { get; set; }
    private bool IsFavourite { get; set; } = false;
    public async Task Delete ()
    {
        await OnDelete.InvokeAsync(Business.Id);
    }
    public async Task Update ()
    {
        Navigation.NavigateTo($"updateBusiness/{Business.Id}");
    }
    public void NavigateToDetails()
    {
        Navigation.NavigateTo($"business/{Business.Id}");
    }

    public async Task ToggleFavourite ()
    {
        IsFavourite = await FavouriteBusinesses.Check(Business.Id);
        if (IsFavourite == true)
        {
            IsFavourite = false;
            await FavouriteBusinesses.Delete(Business.Id);
        }
        else
        {
            IsFavourite = true;
            await FavouriteBusinesses.Add(Business.Id);
        }
    }
}
