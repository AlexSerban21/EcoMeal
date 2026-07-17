using EcoMeal.Site.Models;
using EcoMeal.Site.Services;
using Microsoft.AspNetCore.Components;

namespace EcoMeal.Site.Components.BusinessList;
public partial class BusinessList
{
    [Inject]
    public required AppService AppService { get; set; }

    [Inject]
    public required NavigationManager NavigationManager { get; set; }
    [Inject]
    public required BusinessTypeService BusinessTypeService { get; set; }
    [Inject]
    public required BusinessService BusinessService { get; set; }
    [Inject]
    public required CityService CityService { get; set; }
    private List<BusinessTypeModel> BusinessTypes { get; set; } = new List<BusinessTypeModel>();
    [Parameter]
    public List<BusinessModel>? Businesses { get; set; }
    [Parameter]
    public bool ShowAddBusinessButton { get; set; }
    private List<BusinessModel>? FilteredBusinesses { get; set; }
    private List<CityModel> Cities { get; set; } = new List<CityModel>();
    private int SelectedBusinessTypeId { get; set; } = 0;
    private int SelectedBusinessCity { get; set; } = 0;
    private async Task HideMessageAsync()
    {
        await Task.Delay(3000);

        AppService.Message = null;

        await InvokeAsync(StateHasChanged);
    }
    protected override async Task OnInitializedAsync()
    {
        BusinessTypes = await BusinessTypeService.GetAll();
        FilteredBusinesses = Businesses.ToList();
        Cities = await CityService.GetAll();
        StateHasChanged();
    }

    protected override void OnParametersSet()
    {
        FilteredBusinesses = Businesses.ToList();
    }
    public async Task FilterBusinesses()
    {
        FilteredBusinesses = Businesses.Where(b => SelectedBusinessTypeId == 0 || b.BusinessTypeId == SelectedBusinessTypeId)
            .Where(b => SelectedBusinessCity == 0 || b.CityId == SelectedBusinessCity).Select(b => b).ToList();
    }

    public void NavigateToAddBusiness()
    {
        NavigationManager.NavigateTo(uri: $"addBusiness");
    }
    public async Task DeleteBusiness(int id)
    {
        await BusinessService.Delete(id);
        if (Businesses != null)
        {
            Businesses.RemoveAll (b => b.Id == id);
            FilteredBusinesses.RemoveAll(b => b.Id == id);
            AppService.Message = "Restaurantul a fost șters cu succes!";
            StateHasChanged();
            _ = HideMessageAsync();
            StateHasChanged();
        }
    }
}
