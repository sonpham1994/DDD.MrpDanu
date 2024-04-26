using Infrastructure;

namespace Api.HostedServices;

//wait for .NET 8 to use HostedService as background task which is concurrently.
//https://www.youtube.com/watch?v=XA_3CZmD9y0&ab_channel=NickChapsas
public sealed class MigrationsHostedService(ILogger<MigrationsHostedService> logger, IServiceScopeFactory serviceScopeFactory) : IHostedService
{
    private readonly ILogger<MigrationsHostedService> _logger = logger;
    private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;


    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Start Apply Migration");
        await _serviceScopeFactory.ApplyMigrationsAsync();
        _logger.LogInformation("End Apply Migration");
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
