using Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Data;

namespace Web.HostedServices;

public sealed class MrpDanuHostedService : IHostedService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public MrpDanuHostedService(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _serviceScopeFactory.ApplyMigrationsAsync();
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
