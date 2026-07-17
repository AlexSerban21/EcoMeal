using EcoMeal.Site.Components;
using EcoMeal.Site.Services;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddTransient<AuthenticationHeaderHandler>();

var apiClientBuilder = builder.Services.AddHttpClient("EcoMealApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7206/");
}).AddHttpMessageHandler<AuthenticationHeaderHandler>();

// In development the backend uses the ASP.NET self-signed dev certificate.
// Accept it for the server-to-server API calls so the Authorization header is not
// dropped by an http->https redirect (which strips auth headers on cross-scheme redirects).
if (builder.Environment.IsDevelopment())
{
    apiClientBuilder.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    });
}


builder.Services.AddScoped(sp =>
    sp.GetRequiredService<IHttpClientFactory>().CreateClient("EcoMealApi"));

builder.Services.AddEcoMealApiClient("https://localhost:7206/");
builder.Services.AddScoped<BusinessService>();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<PackageService>();
builder.Services.AddScoped<BusinessTypeService>();
builder.Services.AddScoped<PackageTypeService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<FavouriteBusinessService>();
builder.Services.AddScoped<CityService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<RatingService>();
builder.Services.AddScoped<AppService>();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
