using System.Net.Http.Json;
using Infrastructure.Persistence.Writes;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using MinimalApi.ApiResponses;

namespace Tests.Integration;

public partial class SharedTests(EndpointFactory endpointFactory) : IAsyncLifetime
{
    protected readonly HttpClient HttpClient = endpointFactory.CreateClient();
    protected IServiceScope ServiceScope { get; private set; }
    private AppDbContext _appDbContext { get; set; }

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

    protected DatabaseFacade GetDatabaseFacade()
    {
        return _appDbContext.Database;
    }
    
    public Task InitializeAsync()
    {
        CreatedIds.Clear();
        ServiceScope = endpointFactory.Services.CreateScope();
        _appDbContext = ServiceScope.ServiceProvider.GetRequiredService<AppDbContext>();
        
        return Task.CompletedTask;
    }

    public async Task DisposeAsync()
    {
        foreach (var createdId in CreatedIds)
        {
            await HttpClient.DeleteAsync($"{BaseEndpoint}/{createdId}");
        }
        
        ServiceScope.Dispose();
        await _appDbContext.DisposeAsync();
    }
}