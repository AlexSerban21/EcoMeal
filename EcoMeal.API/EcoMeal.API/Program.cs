using EcoMeal.API.Application.Constants;
using EcoMeal.API.Entities;
using EcoMeal.API.Infrastructure;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// OpenAPI
builder.Services.AddOpenApi(options =>
{
    options.OpenApiVersion =
        Microsoft.OpenApi.OpenApiSpecVersion.OpenApi3_0;
});

// Database
builder.Services.AddDbContext<EcoMealDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration
            .GetConnectionString("DefaultConnection"));
});

// Identity API
builder.Services
    .AddIdentityApiEndpoints<User>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;

        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = true;
        options.Password.RequireLowercase = true;
    })
    .AddRoles<IdentityRole<int>>()
    .AddEntityFrameworkStores<EcoMealDbContext>();
builder.Services.Configure<BearerTokenOptions>(
    IdentityConstants.BearerScheme,
    options =>
    {
        options.BearerTokenExpiration =
            TimeSpan.FromHours(30);

        options.RefreshTokenExpiration =
            TimeSpan.FromDays(30);
    });

// Authorization
builder.Services.AddAuthorization();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorSite", policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:5195",
                "https://localhost:7083")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Development tools
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint(
            "/openapi/v1.json",
            "EcoMeal API");
    });
}

app.UseHttpsRedirection();

// CORS trebuie înainte de auth
app.UseCors("AllowBlazorSite");

// DEBUG: verifică dacă tokenul ajunge efectiv în API
app.Use(async (context, next) =>
{
    var authorization =
        context.Request.Headers.Authorization.ToString();

    Console.WriteLine(
        $"API Authorization Header: {authorization}");

    await next();
});

// Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

// Identity endpoints
app.MapIdentityApi<User>();

// API controllers
app.MapControllers();

// Seed roles
using (var scope = app.Services.CreateScope())
{
    var roleManager =
        scope.ServiceProvider
            .GetRequiredService<
                RoleManager<IdentityRole<int>>>();

    var roles = new[]
    {
        UserRoles.Admin,
        UserRoles.User
    };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(
                new IdentityRole<int>
                {
                    Name = role
                });
        }
    }
}

app.Run();