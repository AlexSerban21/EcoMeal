using EcoMeal.Site.Models;
using EcoMeal.Site.Services;
using Microsoft.AspNetCore.Components;
using System.Net.NetworkInformation;

namespace EcoMeal.Site.Components.Pages;

public partial class BusinessDetails
{
    [Parameter]
    public int Id { get; set; }
    [Inject]
    public required AppService AppService { get; set; }

    [Inject]
    public required BusinessService BusinessService { get; set; }
    [Inject]
    public required PackageService PackageService { get; set; }
    [Inject]
    public required PackageTypeService PackageTypeService { get; set; }
    [Inject]
    public required RatingService RatingService { get; set; }

    private BusinessModel? Business;
    private List<PackageModel>? Packages;
    private List<PackageTypesModel>? PackageTypes;
    private int SelectedPackageTypeId = 0;
    private int MaxPrice = 0, BusinessRating = 0;

    protected override async Task OnInitializedAsync()
    {
        if (!string.IsNullOrEmpty(AppService.Message))
        {
            await Task.Delay(3000);

            AppService.Message = null;

            await InvokeAsync(StateHasChanged);
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            Business = await BusinessService.GetOneById(Id);
            Packages = await PackageService.GetFromBusinessId(Id, 0, 0);
            PackageTypes = await PackageTypeService.GetAll();
            BusinessRating = await RatingService.GetBusinessRating(Id);
            StateHasChanged();
        }
    }
    public async Task FilterPackages ()
    {
        Packages = await PackageService.GetFromBusinessId(Id, SelectedPackageTypeId, MaxPrice);
        StateHasChanged();
    }
    public void NavigateToAddPackage()
    {
        NavigationManager.NavigateTo(uri: $"business/{Id}/addPackage");
    }
    public async void GetRating (int rating)
    {
        BusinessRating = rating;
        await RatingService.AddRating(Id, rating);
        Business = await BusinessService.GetOneById(Id);
        StateHasChanged();
    }
}
