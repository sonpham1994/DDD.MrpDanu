namespace Api.HostedServices;

// to know when the Full GC event be triggered. When the Full GC is triggered, it will clean up memory across Generations
// from Generation 0 to 2. If may be there are a lot of LOH so this is why the full GC is triggered frequently.
// that's why we need this class to monitor the frequent full GC event.
//may be we don't need FullGCEventBackgroundService, we use GC.CollectionCount in RequestLoggingHandler to know when the 
// garbage collection generation 2 perform
public class FullGCEventBackgroundService : BackgroundService
{
    private readonly PeriodicTimer _timer;
    private readonly Serilog.ILogger _logger;
    
    public FullGCEventBackgroundService(Serilog.ILogger logger)
    {
        _logger = logger;
        _timer = new(TimeSpan.FromMilliseconds(500));
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (await _timer.WaitForNextTickAsync(stoppingToken)
               && !stoppingToken.IsCancellationRequested)
        {
            // Check for a notification of an approaching collection.
            GCNotificationStatus s = GC.WaitForFullGCApproach();
            if (s == GCNotificationStatus.Succeeded)
            {
                _logger.Warning("Full GC approaching!, Time generated: {@TimeGenerated}", DateTime.UtcNow);
            }

            // Check for a notification of a completed collection.
            s = GC.WaitForFullGCComplete();
            if (s == GCNotificationStatus.Succeeded)
            {
                _logger.Warning("Full GC completed!, Time generated: {@TimeGenerated}", DateTime.UtcNow);
            }
        }
    }
}