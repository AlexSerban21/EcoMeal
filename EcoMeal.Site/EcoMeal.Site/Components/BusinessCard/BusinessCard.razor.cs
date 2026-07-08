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
    public required BusinessService BusinessService { get; set; }
    [Inject]
    public required NavigationManager Navigation {  get; set; }
    public async Task Delete ()
    {
        Console.WriteLine("b");
        await OnDelete.InvokeAsync(Business.Id);
    }
    public void NavigateToDetails()
    {
        Navigation.NavigateTo($"business/{Business.Id}");
    }
}
