using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace FakeStoreApi.Integrations.Repositories.Base;

internal abstract class BaseApiHttpHelper<T> where T : class
{
    protected readonly HttpClient client;
    protected readonly string baseUrl;

    protected BaseApiHttpHelper(string baseUrl)
    {
        this.baseUrl = baseUrl;
        client = new HttpClient();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    protected async Task<IEnumerable<T>> GetAllAsync()
    {
        var response = await client.GetAsync(baseUrl);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<IEnumerable<T>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
    }

    protected async Task<T?> GetByIdAsync(int id)
    {
        var response = await client.GetAsync($"{baseUrl}/{id}");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        return string.IsNullOrWhiteSpace(json) ? null :
            JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }

    protected async Task<T> PostAsync(T entity)
    {
        var json = JsonSerializer.Serialize(entity);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync(baseUrl, content);
        response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
    }

    protected async Task<T> PutAsync(int id, T entity)
    {
        var json = JsonSerializer.Serialize(entity);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PutAsync($"{baseUrl}/{id}", content);
        response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
    }

    protected async Task<bool> DeleteAsync(int id)
    {
        var response = await client.DeleteAsync($"{baseUrl}/{id}");
        return response.IsSuccessStatusCode;
    }
}
