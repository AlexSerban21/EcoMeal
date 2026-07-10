using EcoMeal.Site.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcoMeal.Site.Services;

public class PackageService
{
    private readonly HttpClient _http;
    public PackageService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<PackageModel>> GetFromBusinessId(int id)
    {
        var Packages = await _http.GetFromJsonAsync<List<PackageModel>>($"/api/Package/GetFromBusinessId/{id}");
        return Packages ?? new List<PackageModel>();
    }
    public async Task<PackageModel> GetOneById(int id)
    {
        var Package = await _http.GetFromJsonAsync<PackageModel>($"/api/Package/GetOneById/{id}");
        return Package;
    }

    public async Task<bool> Delete(int id)
    {
        var response = await _http.DeleteAsync($"/api/Package/Delete/{id}");
        return response.IsSuccessStatusCode;
    }
    
    public async Task AddToBusiness(int businessId, PackageAddModel package)
    {
        await _http.PostAsJsonAsync($"/api/Package/AddToBusiness/{businessId}", package);
    }

    public async Task Update(int PackageId, PackageAddModel package)
    {
        await _http.PutAsJsonAsync<PackageAddModel>($"/api/Package/Update/{PackageId}", package);
    }
}
