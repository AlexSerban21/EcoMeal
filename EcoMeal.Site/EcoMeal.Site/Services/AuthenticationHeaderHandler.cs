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

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        try
        {
            var tokenResult = await _localStorage.GetAsync<string>("authToken");
            if (tokenResult.Success && !string.IsNullOrEmpty(tokenResult.Value))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokenResult.Value);
                _logger.LogDebug("Attached Bearer token to outgoing request.");
            }
            else
            {
                _logger.LogDebug("No auth token available. Request sent without authorization.");
            }
        }
        catch (InvalidOperationException)
        {
            // Usually happens if JS interop is called during prerendering (which is disabled, but good to catch anyway)
            _logger.LogDebug("Failed to read token from local storage (likely prerendering phase). Request sent without authorization.");
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
