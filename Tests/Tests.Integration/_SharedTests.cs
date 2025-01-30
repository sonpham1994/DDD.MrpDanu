using System.Net.Http.Json;
using MinimalApi.ApiResponses;

namespace Tests.Integration;

public partial class SharedTests(HttpClient httpClient) : IAsyncLifetime
{
    protected readonly HttpClient HttpClient = httpClient;
    public const string Host = "http://localhost";
    protected virtual string BaseEndpoint => string.Empty;
    protected virtual string Url => string.Empty;
    protected HashSet<Guid> CreatedIds { get; } = new();
    
    public static async Task<ApiResponse?> ReadFromJsonAsync(HttpResponseMessage response)
    {
        return await response.Content.ReadFromJsonAsync<ApiResponse>();
    }
    
    public static async Task<ApiResponse<TResponse>?> ReadFromJsonAsync<TResponse>(HttpResponseMessage response)
    {
        return await response.Content.ReadFromJsonAsync<ApiResponse<TResponse>>();
    }
    
    public Task InitializeAsync()
    {
        CreatedIds.Clear();
        return Task.CompletedTask;
    }

    public async Task DisposeAsync()
    {
        foreach (var createdId in CreatedIds)
        {
            await HttpClient.DeleteAsync($"{BaseEndpoint}/{createdId}");
        }
    }
}