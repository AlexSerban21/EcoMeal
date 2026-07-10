using EcoMeal.Site.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcoMeal.Site.Services;

public class BusinessService
{
    private readonly HttpClient _http;
    public BusinessService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<BusinessModel>> GetAll()
    {
        var businesses = await _http.GetFromJsonAsync<List<BusinessModel>>("/api/Business/GetAll");
        return businesses ?? new List<BusinessModel>();
    }
    public async Task<BusinessModel?> GetOneById(int id)
    {
        var business = await _http.GetFromJsonAsync<BusinessModel>($"/api/Business/GetOneById/{id}");

        return business;
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
