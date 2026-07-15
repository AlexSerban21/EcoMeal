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
        var response = await _http.PostAsJsonAsync ("api/order", new {PackageId = packageId});
        return response.IsSuccessStatusCode;
    }
    public async Task<List<OrderGetModel>> GetMyOrdersAsync()
    {
        try
        {
        var orders = await _http.GetFromJsonAsync<List<OrderGetModel>>("api/order/my");
            return orders ?? new List<OrderGetModel>();

        }
        catch
        {
            return new List<OrderGetModel>();
        }
/*        var request = new HttpRequestMessage(HttpMethod.Get, "api/order/my");
       *//* await AddAuthHeaderAsync(request);*//*

        var response = await _http.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var orders = await response.Content.ReadFromJsonAsync<List<OrderGetModel>>();*/
    }

    private async Task AddAuthHeaderAsync(HttpRequestMessage request)
    { 

        if (!string.IsNullOrEmpty(_authService.Token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _authService.Token);
        }
    }

}
