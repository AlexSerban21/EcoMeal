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
    public async Task<List<BusinessModel>> GetAllSync()
    {
        var businesses = await _http.GetFromJsonAsync<List<BusinessModel>>("/api/business");
        return businesses ?? new List<BusinessModel>();
    }
    public async Task<bool> DeleteAsync(int id)
    {
        var response = await _http.DeleteAsync($"/api/business/{id}");
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeletePackage(int id)
    {
        var response = await _http.DeleteAsync($"/api/business/package/{id}");
        return response.IsSuccessStatusCode;
    }

    public async Task<BusinessModel?> GetOneById(int id)
    {
        var business = await _http.GetFromJsonAsync<BusinessModel>($"/api/business/{id}");

        return business;
    }

    public async Task<List<PackageTypesModel>> GetPackageTypes ()
    {
        var PackageTypes = await _http.GetFromJsonAsync<List<PackageTypesModel>>("/api/Business/packageTypes");
        return PackageTypes ?? new List<PackageTypesModel>();
    }

    public async Task<List<PackageModel>> GetPackagesFromBusinessId(int id)
    {
        var Packages = await _http.GetFromJsonAsync<List<PackageModel>>($"/api/Business/{id}/packages");
        return Packages ?? new List<PackageModel>();
    }
    public async Task<PackageModel> GetPackageById(int id)
    {
        var Package = await _http.GetFromJsonAsync<PackageModel>($"/api/Business/getPackage/{id}");
        return Package;
    }

    public async Task<List<BusinessTypeModel>> GetBusinessTypes()
    {
        var BusinessTypes = await _http.GetFromJsonAsync<List<BusinessTypeModel>>("/api/Business/businessTypes");
        return BusinessTypes ?? new List<BusinessTypeModel>();
    }

    public async Task AddPackageToBusiness(int businessId, PackageAddModel package)
    {
        await _http.PostAsJsonAsync($"/api/business/{businessId}/addPackage", package);
    }

    public async Task AddBusiness (BusinessAddModel business)
    {
        await _http.PostAsJsonAsync<BusinessAddModel>("/api/Business/addBusiness", business);
    }

    public async Task UpdateBusiness(int businessId, BusinessAddModel business)
    {
        await _http.PutAsJsonAsync<BusinessAddModel>($"/api/Business/{businessId}/updateBusiness", business);
    }

    public async Task UpdatePackage(int PackageId, PackageAddModel package)
    {
        await _http.PutAsJsonAsync<PackageAddModel>($"/api/Business/{PackageId}/updatePackage", package);
    }
}
