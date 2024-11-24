using Infrastructure;

namespace MinimalApi.HostedServices;

//wait for .NET 8 to use HostedService as background task which is concurrently.
//https://www.youtube.com/watch?v=XA_3CZmD9y0&ab_channel=NickChapsas
public sealed class MigrationsHostedService(
    ILogger<MigrationsHostedService> logger, 
    IServiceScopeFactory serviceScopeFactory) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Start Apply Migration");
        await serviceScopeFactory.ApplyMigrationsAsync();
        logger.LogInformation("End Apply Migration");
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
