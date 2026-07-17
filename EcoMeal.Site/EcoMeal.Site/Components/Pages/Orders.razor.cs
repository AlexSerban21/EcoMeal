using EcoMeal.Site.Models;
using EcoMeal.Site.Services;
using Microsoft.AspNetCore.Components;

namespace EcoMeal.Site.Components.Pages;

public partial class Orders
{
    [Inject]
    public required OrderService OrderService { get; set; }
    private List<OrderGetModel>? OrderList;
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if(firstRender)
        {
            OrderList = await OrderService.GetMyOrdersAsync();
            StateHasChanged();
        }
    }
}
