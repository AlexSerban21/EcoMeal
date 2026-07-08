using Microsoft.AspNetCore.Components;

namespace EcoMeal.Site.Components.Pages;

public partial class Home
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }
    public void NavigateToAddBusiness()
    {
        NavigationManager.NavigateTo(uri: $"addBusiness");
    }
}
