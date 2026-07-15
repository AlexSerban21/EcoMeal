namespace EcoMeal.Site.Services;

public static class HttpClientServiceExtensions
{
    public static IServiceCollection AddEcoMealApiClient(this IServiceCollection services, string baseAddress)
    {
        services.AddScoped(sp => new HttpClient(
            new AuthenticationHeaderHandler(
                sp.GetRequiredService<Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage.ProtectedLocalStorage>(),
                sp.GetRequiredService<ILoggerFactory>().CreateLogger<AuthenticationHeaderHandler>())
            {
                InnerHandler = new HttpClientHandler()
            })
        {
            BaseAddress = new Uri(baseAddress)
        });

        return services;
    }
}
