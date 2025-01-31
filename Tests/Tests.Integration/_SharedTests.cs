using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using MinimalApi.ApiResponses;

namespace Tests.Integration;

public partial class SharedTests(EndpointFactory endpointFactory) : IAsyncLifetime
{
    //protected readonly IServiceProvider ServiceProvider = endpointFactory.ServiceProvider.CreateScope().ServiceProvider;
    
    protected readonly HttpClient HttpClient = endpointFactory.CreateClient();
    
    protected const string Host = "http://localhost";
    protected virtual string BaseEndpoint => string.Empty;
    protected virtual string Url => string.Empty;
    protected HashSet<Guid> CreatedIds { get; } = new();

    protected static async Task<ApiResponse?> ReadFromJsonAsync(HttpResponseMessage response)
    {
        return await response.Content.ReadFromJsonAsync<ApiResponse>();
    }

    protected static async Task<ApiResponse<TResponse>?> ReadFromJsonAsync<TResponse>(HttpResponseMessage response)
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