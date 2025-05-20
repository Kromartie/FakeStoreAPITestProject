using FakeStoreApi.Integrations.Configs;
using FakeStoreApi.Integrations.Entities;
using FakeStoreApi.Integrations.Repositories.Base;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace FakeStoreApi.Integrations.Repositories;

// Helper for authentication API operations
internal class AuthApiHelper : BaseApiHttpHelper<LoginResponse>, IAuthApiHelper
{
    // Constructor sets up HttpClient and base URL from config
    public AuthApiHelper(HttpClient httpClient, IOptions<ApiConfig> config)
    {
        client = httpClient;
        baseUrl = $"{config.Value.FakeStoreApiUrl}/auth/login";
    }

    // Sends a login request and returns the response
    public async Task<LoginResponse?> LoginAsync(LoginRequest request)
    {
        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync(baseUrl, content);

        if (!response.IsSuccessStatusCode)
            return null;

        var jsonResponse = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<LoginResponse>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }
}