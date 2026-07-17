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
    public required OrderService OrderService { get; set; }
    [Inject]
    public required BusinessService BusinessService { get; set; }
    private List<BusinessModel> Businesses { get; set; } = new List<BusinessModel>();
    public int SelectedBusiness { get; set; } = 0;
    private List<OrderGetModel> OrdersFiltered { get; set; } = new List<OrderGetModel>();
    protected override async Task OnInitializedAsync()
    {
        Businesses = await BusinessService.GetAll();
    }
    public async Task OnApprove (int orderId)
    {
        await OrderService.ApproveOrder(orderId);
        Orders.RemoveAll(o => o.Id == orderId);
        StateHasChanged();
    }
    public async Task OnCancel(int orderId)
    {
        await OrderService.CancelOrder(orderId);
        Orders.RemoveAll(o => o.Id == orderId);
        StateHasChanged();
    }
    protected override void OnParametersSet()
    {
        if (Orders != null)
            OrdersFiltered = Orders.ToList();
    }
    public void FilterOrders ()
    {
        OrdersFiltered = Orders.Where(o => o.BusinessId == SelectedBusiness || SelectedBusiness == 0).ToList();
    }
}
