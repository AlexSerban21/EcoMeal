using EcoMeal.Site.Models;

namespace EcoMeal.Site.Services;

public class CityService
{
    private readonly HttpClient _http;
    public CityService(HttpClient http)
    {
        _http = http;
    }
    public async Task<List<CityModel>> GetAll()
    {
        var cities = await _http.GetFromJsonAsync<List<CityModel>>($"/api/City/GetAll");
        return cities ?? new List<CityModel>();
    }
}
