using Infrastructure;

namespace Web.HostedServices;

//wait for .NET 8 to use HostedService as background task which is concurrently.
//https://www.youtube.com/watch?v=XA_3CZmD9y0&ab_channel=NickChapsas
public sealed class MigrationsHostedService : IHostedService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public MigrationsHostedService(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _serviceScopeFactory.ApplyMigrationsAsync();
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
