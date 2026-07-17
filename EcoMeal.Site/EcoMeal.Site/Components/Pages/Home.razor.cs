using EcoMeal.Site.Models;
using EcoMeal.Site.Services;
using Microsoft.AspNetCore.Components;

namespace EcoMeal.Site.Components.Pages;

public partial class Home
{
    [Inject]
    public required BusinessService BusinessService { get; set; }
    public List<BusinessModel> Businesses { get; set; } = new List<BusinessModel>();
    protected override async Task OnInitializedAsync()
    {
        Businesses = await BusinessService.GetAll();
        StateHasChanged();
    }
}
