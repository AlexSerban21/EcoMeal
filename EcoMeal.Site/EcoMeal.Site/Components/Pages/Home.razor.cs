using EcoMeal.Site.Models;
using EcoMeal.Site.Services;
using Microsoft.AspNetCore.Components;

namespace EcoMeal.Site.Components.Pages;

public partial class Home
{
    [Inject]
    public required AppService AppService { get; set; }
    [Inject]
    public required BusinessService BusinessService { get; set; }
    public List<BusinessModel> Businesses { get; set; } = new List<BusinessModel>();
    protected override async Task OnInitializedAsync()
    {
        Businesses = await BusinessService.GetAll();

        if (!string.IsNullOrEmpty(AppService.Message))
        {
            _ = HideMessageAsync();
        }
    }

    private async Task HideMessageAsync()
    {
        await Task.Delay(3000);

        AppService.Message = null;

        await InvokeAsync(StateHasChanged);
    }
}
