using EcoMeal.Site.Models;
using System.Net.Http.Headers;

namespace EcoMeal.Site.Services;

public class OrderService
{
    private readonly HttpClient _http;
    private readonly AuthService _authService;
    public OrderService (HttpClient http, AuthService authservice)
    {
        _http = http;
        _authService = authservice;
    }
    public async Task<bool> PlaceOrderAsync(int packageId)
    {
        var OrderAddModel = new OrderAddModel
        {
            PackageId = packageId,
            DateTime = DateTime.Now
        };
        var response = await _http.PostAsJsonAsync ("api/order/placeOrder", OrderAddModel);
        return response.IsSuccessStatusCode;
    }
    public async Task<List<OrderGetModel>> GetMyOrdersAsync()
    {
        var orders = await _http.GetFromJsonAsync<List<OrderGetModel>>("api/order/myOrders");
        return orders ?? new List<OrderGetModel>();
    }
    public async Task<List<OrderGetModel>> GetWaitingOrders()
    {
        var orders = await _http.GetFromJsonAsync<List<OrderGetModel>>("api/order/waitingOrders");
        return orders ?? new List<OrderGetModel>();
    }
    public async Task ApproveOrder(int orderId)
    {
        await _http.PutAsJsonAsync("api/order/approveOrder", orderId);
    }
    public async Task CancelOrder(int orderId)
    {
        await _http.DeleteAsync($"api/order/cancelOrder/{orderId}");
    }
}
