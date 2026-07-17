using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.Extensions.Logging;

namespace EcoMeal.Site.Services;

public class AuthenticationHeaderHandler : DelegatingHandler
{
    private readonly ProtectedLocalStorage _localStorage;
    private readonly ILogger<AuthenticationHeaderHandler> _logger;

    public AuthenticationHeaderHandler(
        ProtectedLocalStorage localStorage,
        ILogger<AuthenticationHeaderHandler> logger)
    {
        _localStorage = localStorage;
        _logger = logger;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        string? token = null;

        try
        {
            var result = await _localStorage.GetAsync<string>("authToken");

            if (result.Success)
            {
                token = result.Value;
            }
        }
        catch (Exception ex)
        {
            _logger.LogDebug(
                ex,
                "Tokenul nu este disponibil încă. Request-ul continuă fără token.");
        }

        // Dacă tokenul există, îl atașăm
        if (!string.IsNullOrWhiteSpace(token))
        {
            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }

        // IMPORTANT:
        // Request-ul continuă indiferent dacă tokenul există sau nu
        return await base.SendAsync(request, cancellationToken);
    }
}