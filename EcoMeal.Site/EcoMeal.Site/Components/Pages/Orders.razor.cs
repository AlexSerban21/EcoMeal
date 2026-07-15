using EcoMeal.Site.Models;
using EcoMeal.Site.Services;
using Microsoft.AspNetCore.Components;

namespace EcoMeal.Site.Components.Pages;

public partial class Orders
{
    [Inject]
    public OrderService OrderService { get; set; }
    private List<OrderGetModel>? MyOrders;
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if(firstRender)
        {
            MyOrders = await OrderService.GetMyOrdersAsync();
            StateHasChanged();
        }
    }
}
