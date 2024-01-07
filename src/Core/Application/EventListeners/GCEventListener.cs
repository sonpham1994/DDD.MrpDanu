using System.Diagnostics.Tracing;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Logging;

namespace Application.EventListeners;

internal sealed class GCEventListener : EventListener
{
    private const string EventCounters = "EventCounters";
    private const string KeyGCName = "Name";
    private const string ValueGCName = "gc-";
    private static int PreviousGen0Count = 0;
    private static int PreviousGen1Count = 0;
    private static int PreviousGen2Count = 0;
    
    private readonly ILogger<GCEventListener> _logger;

    public GCEventListener(ILogger<GCEventListener> logger)
    {
        _logger = logger;
    }
    protected override void OnEventSourceCreated(EventSource eventSource)
    {
        EnableEvents(eventSource, EventLevel.LogAlways, EventKeywords.All);
    }

    protected override void OnEventWritten(EventWrittenEventArgs eventData)
    {
        if (_logger == null)
            return;
        
        // Check if the event is related to GC
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            // the EventListener uses Event Tracing for Windows (ETW), which provides detailed and comprehensive
            // event data. However, ETW is specific to Windows. Hence, in windows, this provides information being 
            // different from other platforms
            if (eventData.EventId == -1 
                && !string.IsNullOrEmpty(eventData.EventName) 
                && eventData.EventName == EventCounters
                && eventData.Payload[0] is IDictionary<string, object> eventPayload
                && eventPayload.TryGetValue(KeyGCName, out object gcNameObj)
                && gcNameObj is string gcName
                && gcName.Contains(ValueGCName))
            {
                /* the GcData has information like this:
                 [Name, gen-0-gc-count], [DisplayName, Gen 0 GC Count], [DisplayRateTimeScale, 00:01:00], [Increment, 2], [IntervalSec, 0.9916412], [Metadata, ], [Series, Interval=1000], [CounterType, Sum], [DisplayUnits, ]
                 [Name, gen-1-gc-count], [DisplayName, Gen 1 GC Count], [DisplayRateTimeScale, 00:01:00], [Increment, 0], [IntervalSec, 0.9916412], [Metadata, ], [Series, Interval=1000], [CounterType, Sum], [DisplayUnits, ]
                 [Name, gen-2-gc-count], [DisplayName, Gen 2 GC Count], [DisplayRateTimeScale, 00:01:00], [Increment, 0], [IntervalSec, 0.9916412], [Metadata, ], [Series, Interval=1000], [CounterType, Sum], [DisplayUnits, ]
                 [Name, gc-fragmentation], [DisplayName, GC Fragmentation], [Mean, 44.220805725151216], [StandardDeviation, 0], [Count, 1], [Min, 44.220805725151216], [Max, 44.220805725151216], [IntervalSec, 0.9860976], [Series, Interval=1000], [CounterType, Mean], [Metadata, ], [DisplayUnits, %]
                 
                 gc-heap-size: This indicates the total size of the managed heap. It's a measure of the amount of memory that is currently allocated by the application for managed objects.
                gc-fragmentation: This metric indicates how fragmented the managed heap is. Fragmentation occurs when there are small gaps between allocated objects. High fragmentation can lead to inefficient memory usage and may cause the application to require more memory than it's actually using for object data.
                gc-committed: This likely refers to the amount of memory that is committed for the GC heap. Committed memory is the physical memory for which space has been reserved on the disk paging file.
                 
                 Increment = 2 means there have been 2 garbage collection occurrences in Generation.
                 The interval itself (IntervalSec) is the time between each report or update of these counters. In your data, it's approximately 0.991 seconds.
                 [Mean, 44.220805725151216] (for gc-fragmentation): The average value of the metric, in this case, the average heap fragmentation percentage.
                 [StandardDeviation, 0] (for gc-fragmentation): Measures the variation or dispersion of the fragmentation values from the mean.
                 [Count, 1]: The number of samples collected for the metric.
                [Min, 44.220805725151216] and [Max, 44.220805725151216] (for gc-fragmentation): The minimum and maximum values recorded for the metric within the sampling interval.
                [DisplayUnits, %] (for gc-fragmentation): Indicates the unit of measurement for the metric, which is percentage in this case.
                 */
                
                _logger.LogWarning("GC Event: {GCEventName}, GcData: {@GcData}, Time generated: {@TimeGenerated}", gcName, eventPayload, eventData.TimeStamp);
            }
        }
        // this would be track whenever the GC performs, it means not only your handlers from your code, but also the
        // operations from the .NET. When a request comes in, .NET will do many tasks before reach your Controller or
        // your code. If you want to track the .NET operations whether they perform GC collection or not, uncomment this
        // else
        // {
        //     var currentGen0Count = GC.CollectionCount(0);
        //     var currentGen1Count = GC.CollectionCount(1);
        //     var currentGen2Count = GC.CollectionCount(2);
        //     
        //     if (currentGen0Count > PreviousGen0Count)
        //     {
        //         PreviousGen0Count = currentGen0Count;
        //         _logger.LogWarning("GC Event: {GCEventName} at Generation {GenerationNumber} has occured with {GCCollectionTimes} times, Time generated: {@TimeGenerated}", eventData.EventName, 0, currentGen0Count, eventData.TimeStamp);
        //     }
        //     if (currentGen1Count > PreviousGen1Count)
        //     {
        //         PreviousGen1Count = currentGen1Count;
        //         _logger.LogWarning("GC Event: {GCEventName} at Generation {GenerationNumber} has occured with {GCCollectionTimes} times, Time generated: {@TimeGenerated}", eventData.EventName, 1, currentGen1Count, eventData.TimeStamp);
        //     }
        //     if (currentGen2Count > PreviousGen2Count)
        //     {
        //         PreviousGen2Count = currentGen2Count;
        //         _logger.LogWarning("GC Event: {GCEventName} at Generation {GenerationNumber} has occured with {GCCollectionTimes} times, Time generated: {@TimeGenerated}", eventData.EventName, 2, currentGen2Count, eventData.TimeStamp);
        //     }
        // }
    }
}