using EcoMeal.Site.Models;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace EcoMeal.Site.Services;

public class BusinessService
{
    private readonly HttpClient _http;
    private readonly AuthService _authService;
    private readonly ProtectedLocalStorage _protectedLocalStorage;
    public BusinessService(HttpClient http, AuthService authService, ProtectedLocalStorage protectedLocalStorage)
    {
        _http = http;
        _authService = authService;
        _protectedLocalStorage = protectedLocalStorage;
    }

    public async Task<List<BusinessModel>> GetAll()
    {
        var businesses = await _http.GetFromJsonAsync<List<BusinessModel>>($"/api/Business/GetAll");
        return businesses ?? new List<BusinessModel>();
    }
    public async Task<BusinessModel?> GetOneById(int id)
    {
        try
        {
            var business = await _http.GetFromJsonAsync<BusinessModel>($"/api/Business/GetOneById/{id}");
            return business;

        } catch
        {
            return null;
        }
    }

    public async Task<bool> Delete(int id)
    {
        var response = await _http.DeleteAsync($"/api/Business/Delete/{id}");
        return response.IsSuccessStatusCode;
    }

    public async Task Add (BusinessAddModel business)
    {
        await _http.PostAsJsonAsync<BusinessAddModel>("/api/Business/AddBusiness", business);
    }

    public async Task Update(int businessId, BusinessAddModel business)
    {
        await _http.PutAsJsonAsync<BusinessAddModel>($"/api/Business/UpdateBusiness/{businessId}", business);
    }

   
}
