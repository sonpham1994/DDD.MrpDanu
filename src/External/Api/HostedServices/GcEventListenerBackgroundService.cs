using Api.Constants;
using Api.EventListeners;
using Microsoft.FeatureManagement;

namespace Api.HostedServices;

// to know when the Full GC event be triggered. When the Full GC is triggered, it will clean up memory across Generations
// from Generation 0 to 2. If may be there are a lot of LOH so this is why the full GC is triggered frequently.
// that's why we need this class to monitor the frequent full GC event.
//may be we don't need FullGCEventBackgroundService, we use GC.CollectionCount in RequestLoggingHandler to know when the 
// garbage collection generation 2 perform
public sealed class GcEventListenerBackgroundService(ILogger<GcEventListener> logger, IFeatureManager featureManager) 
    : IHostedService, IDisposable
{
    // for BackgroundService
    //private readonly PeriodicTimer _timer = new(TimeSpan.FromMilliseconds(500));
    // protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    // {
    //     // using Feature Flag when you want to enable this
    //     //Feature flag: https://www.linkedin.com/feed/update/urn:li:activity:7138990332951588865?utm_source=share&utm_medium=member_ios
    //     //Apply feature flag for GCLoggingHandler, if the environment want to check triggered GC, enable the log through feature flag. https://www.milanjovanovic.tech/blog/feature-flags-in-dotnet-and-how-i-use-them-for-ab-testing?utm_source=LinkedIn&utm_medium=social&utm_campaign=19.02.2024
    //     // https://www.youtube.com/watch?v=QVEUgIC7Wpo
    //     
    //     while (await _timer.WaitForNextTickAsync(stoppingToken)
    //            && !stoppingToken.IsCancellationRequested)
    //     {
    //         if (await featureManager.IsEnabledAsync(FeatureFlags.GcEventListener))
    //         {
    //             _gcEventListener = new GcEventListener(logger);
    //         }
    //         // // Check for a notification of an approaching collection.
    //         // GCNotificationStatus s = GC.WaitForFullGCApproach();
    //         // if (s == GCNotificationStatus.Succeeded)
    //         // {
    //         //     _logger.Warning("Full GC approaching!, Time generated: {@TimeGenerated}", DateTime.UtcNow);
    //         // }
    //         //
    //         // // Check for a notification of a completed collection.
    //         // s = GC.WaitForFullGCComplete();
    //         // if (s == GCNotificationStatus.Succeeded)
    //         // {
    //         //     _logger.Warning("Full GC completed!, Time generated: {@TimeGenerated}", DateTime.UtcNow);
    //         // }
    //     }
    // }

    private readonly PeriodicTimer _timer = new(TimeSpan.FromSeconds(10));
    private GcEventListener? _gcEventListener;
    
    public async Task StartAsync(CancellationToken stoppingToken)
    {
        while (await _timer.WaitForNextTickAsync(stoppingToken)
               && !stoppingToken.IsCancellationRequested)
        {
            if (await featureManager.IsEnabledAsync(FeatureFlags.GcEventListener))
            {
                if (_gcEventListener is null)
                {
                    _gcEventListener = new GcEventListener(logger);
                    logger.LogInformation("GCEventListener started.");
                }
            }
            else
            {
                if (_gcEventListener is not null)
                {
                    _gcEventListener.Dispose();
                    _gcEventListener = null;
                    logger.LogInformation("GCEventListener started.");
                }
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _gcEventListener?.Dispose();
        return Task.CompletedTask;
    }
    
    public void Dispose()
    {
        _timer.Dispose();
        _gcEventListener?.Dispose();
    }
}