using EcoMeal.Site.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcoMeal.Site.Services;

public class RatingService
{
    private readonly HttpClient _http;
    public RatingService(HttpClient http)
    {
        _http = http;
    }
    public async Task AddRating (int businessId, int value)
    {
        await _http.PostAsJsonAsync($"/api/Rating/AddRating", new
        {
            BusinessId = businessId,
            Value = value
        });
    }
}
