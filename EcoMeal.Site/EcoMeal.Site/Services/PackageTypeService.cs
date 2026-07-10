using EcoMeal.Site.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcoMeal.Site.Services;

public class PackageTypeService
{
    private readonly HttpClient _http;
    public PackageTypeService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<PackageTypesModel>> GetAll()
    {
        var PackageTypes = await _http.GetFromJsonAsync<List<PackageTypesModel>>("/api/PackageType/GetAll");
        return PackageTypes ?? new List<PackageTypesModel>();
    }
}
