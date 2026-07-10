using EcoMeal.Site.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcoMeal.Site.Services;

public class BusinessTypeService
{
    private readonly HttpClient _http;
    public BusinessTypeService(HttpClient http)
    {
        _http = http;
    }
    public async Task<List<BusinessTypeModel>> GetAll()
    {
        var BusinessTypes = await _http.GetFromJsonAsync<List<BusinessTypeModel>>("/api/BusinessType/GetAll");
        return BusinessTypes ?? new List<BusinessTypeModel>();
    }
}
