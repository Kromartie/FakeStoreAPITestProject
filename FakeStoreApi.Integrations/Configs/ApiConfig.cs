namespace FakeStoreApi.Integrations.Configs;

// Holds settings for the Fake Store API
internal class ApiConfig
{
    // Section name in appsettings.json
    public const string API = "Api";

    // Base URL for the Fake Store API
    public string FakeStoreApiUrl { get; set; } = string.Empty;
}
