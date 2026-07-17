using EcoMeal.Site.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace EcoMeal.Site.Services;

public class FavouriteBusinessService
{
    HttpClient _http;
    public FavouriteBusinessService (HttpClient http)
    {
        _http = http;
    }
    public async Task<bool> Check (int id)
    {
        return await _http.GetFromJsonAsync<bool>($"/api/FavouriteBusiness/Check/{id}");
    }
    public async Task Add(int id)
    {
        var response = await _http.PostAsJsonAsync<object?>($"/api/FavouriteBusiness/Add/{id}", null);
    }
    public async Task Delete(int id)
    {
        await _http.DeleteAsync($"/api/FavouriteBusiness/Delete/{id}");
    }
    public async Task<List<BusinessModel>> GetAll()
    {
        var FavouriteBusinesses =  await _http.GetFromJsonAsync<List<BusinessModel>>($"/api/FavouriteBusiness/Get");
        return FavouriteBusinesses ?? new List<BusinessModel>();
    }
}
