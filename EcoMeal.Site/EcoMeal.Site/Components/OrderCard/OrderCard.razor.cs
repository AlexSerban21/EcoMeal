using EcoMeal.Site.Models;
using EcoMeal.Site.Services;
using Microsoft.AspNetCore.Components;

namespace EcoMeal.Site.Components.OrderCard;

public partial class OrderCard
{
    [Parameter]
    public required OrderGetModel Order { get; set; }
    [Parameter]
    public bool IsAdmin { get; set; }
    [Parameter]
    public EventCallback<int> OnApprove { get; set; }
    [Parameter]
    public EventCallback<int> OnCancel { get; set; }

    public async Task ApproveOrder(int orderId)
    {
        await OnApprove.InvokeAsync(orderId);
        StateHasChanged();
    }
    public async Task CancelOrder(int orderId)
    {
        await OnCancel.InvokeAsync(orderId);
        StateHasChanged();
    }
}
