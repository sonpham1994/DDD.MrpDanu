using System.Data;
using System.Net.Http.Json;
using Infrastructure.Persistence;
using Infrastructure.PersistenceMongoDb;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MinimalApi.ApiResponses;
using Testcontainers.MongoDb;
using Testcontainers.MsSql;

namespace Tests.Integration;

public class EndpointFactory : WebApplicationFactory<ProgramMinimalApi>, IAsyncLifetime
{
    private readonly MsSqlContainer _sqlDbContainer =
        new MsSqlBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
            .Build();
    
    private readonly MongoDbContainer _mongoDbContainer =
        new MongoDbBuilder()
            .WithImage("mongo:latest")
            .Build();

    public const string Host = "http://localhost";

    public static async Task<ApiResponse?> ReadFromJsonAsync(HttpResponseMessage response)
    {
        return await response.Content.ReadFromJsonAsync<ApiResponse>();
    }
    
    public static async Task<ApiResponse<TResponse>?> ReadFromJsonAsync<TResponse>(HttpResponseMessage response)
    {
        return await response.Content.ReadFromJsonAsync<ApiResponse<TResponse>>();
    }
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // Don't log when run test.
        builder.ConfigureLogging(logging =>
        {
            logging.ClearProviders();
        });

        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll<IHostedService>();
            services.RemoveAll(typeof(IDbConnection));
            services.RemoveAll(typeof(IOptions<DatabaseSettings>));
            services.RemoveAll(typeof(IOptions<MongoDbSettings>));

            services.AddScoped<IDbConnection>(_ => new SqlConnection(_sqlDbContainer.GetConnectionString()));
            
            // Create a DatabaseSettings instance with init setters
            var databaseSettings = new DatabaseSettings
            {
                ConnectionString = _sqlDbContainer.GetConnectionString(),
                MaxRetryCount = 10,
                MaxRetryDelay = 10,
                StandardExecutedDbCommandTime = 5,
                EnableLog = false
            };

            // Register the instance as IOptions<DatabaseSettings>
            services.AddSingleton<IOptions<DatabaseSettings>>(Options.Create(databaseSettings));
        
            var mongoDbSettings = new MongoDbSettings()
            {
                ConnectionString = _mongoDbContainer.GetConnectionString(),
            };
            
            // Register the instance as IOptions<DatabaseSettings>
            services.AddSingleton<IOptions<MongoDbSettings>>(Options.Create(mongoDbSettings));
        });
    }
    
    public async Task InitializeAsync()
    {
        await _sqlDbContainer.StartAsync();
        await _mongoDbContainer.StartAsync();
    }

    public new async Task DisposeAsync()
    {
        await _sqlDbContainer.DisposeAsync();
        await _mongoDbContainer.DisposeAsync();
    }
}