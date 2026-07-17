using EcoMeal.Site.Models;
using EcoMeal.Site.Services;
using Microsoft.AspNetCore.Components;

namespace EcoMeal.Site.Components.PackageList;

public partial class PackageList
{
    [Inject]
    public required AppService AppService { get; set; }
    [Inject]
    public required BusinessService BusinessService { get; set; }
    [Inject]
    public required PackageService PackageService { get; set; }
    [Inject]
    public required OrderService OrderService { get; set; }
    [Parameter]
    public required int Id { get; set; }
    [Parameter]
    public List <PackageModel>? Packages { get; set; }
    private async Task HideMessageAsync()
    {
        await Task.Delay(3000);

        AppService.Message = null;

        await InvokeAsync(StateHasChanged);
    }
    public async Task DeletePackage(int id)
    {
        await PackageService.Delete(id);
        Packages.RemoveAll(b => b.Id == id);
        AppService.Message = "Pachetul a fost șters cu succes!";
        StateHasChanged();
        _ = HideMessageAsync();
        StateHasChanged();
    }
    public async Task UpdatePackage(int id)
    {
        await OrderService.PlaceOrderAsync(id);
        Packages.RemoveAll (b => b.Id == id);
        AppService.Message = "Pachetul a fost rezervat cu succes!";
        StateHasChanged();
        _ = HideMessageAsync();
        StateHasChanged();
    }
}
