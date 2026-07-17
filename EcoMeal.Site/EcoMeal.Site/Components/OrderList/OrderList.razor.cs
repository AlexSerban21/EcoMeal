using EcoMeal.Site.Components.Pages;
using EcoMeal.Site.Models;
using EcoMeal.Site.Services;
using Microsoft.AspNetCore.Components;

namespace EcoMeal.Site.Components.OrderList;

public partial class OrderList
{
    [Parameter]
    public List<OrderGetModel>? Orders { get; set; }
    [Parameter]
    public bool IsAdmin { get; set; }
    [Inject]
    public required AppService AppService { get; set; }
    [Inject]
    public required OrderService OrderService { get; set; }
    [Inject]
    public required BusinessService BusinessService { get; set; }
    private List<BusinessModel> Businesses { get; set; } = new List<BusinessModel>();
    public int SelectedBusiness { get; set; } = 0;
    private List<OrderGetModel> OrdersFiltered { get; set; } = new List<OrderGetModel>();
    public string SelectedStatus { get; set; } = string.Empty;
    protected override async Task OnInitializedAsync()
    {
        Businesses = await BusinessService.GetAll();
    }
    private async Task HideMessageAsync()
    {
        await Task.Delay(3000);

        AppService.Message = null;

        await InvokeAsync(StateHasChanged);
    }
    public async Task OnApprove (int orderId)
    {
        await OrderService.ApproveOrder(orderId);
        Orders.RemoveAll(o => o.Id == orderId);
        OrdersFiltered.RemoveAll(o => o.Id == orderId);
        AppService.Message = "Comanda a fost aprobata cu succes!";
        StateHasChanged();
        _ = HideMessageAsync();
        StateHasChanged();
    }
    public async Task OnCancel(int orderId)
    {
        await OrderService.CancelOrder(orderId);
        Orders.RemoveAll(o => o.Id == orderId);
        OrdersFiltered.RemoveAll(o => o.Id == orderId);
        AppService.Message = "Comanda a fost anulată cu succes!";
        StateHasChanged();
        _ = HideMessageAsync();
        StateHasChanged();
    }
    protected override void OnParametersSet()
    {
        if (Orders != null)
            OrdersFiltered = Orders.ToList();
    }
    public void FilterOrders()
    {
        if (Orders is null)
            return;

        OrdersFiltered = Orders
            .Where(o =>
                (SelectedBusiness == 0 || o.BusinessId == SelectedBusiness)
                &&
                (string.IsNullOrEmpty(SelectedStatus)
                 || o.Status.ToString() == SelectedStatus)
            )
            .ToList();
    }
}
