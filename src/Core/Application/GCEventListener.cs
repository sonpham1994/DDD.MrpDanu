using System.Diagnostics.Tracing;
using Microsoft.Extensions.Logging;

namespace Application;

internal sealed class GCEventListener : EventListener
{
    private readonly ILogger<GCEventListener> _logger;

    public GCEventListener(ILogger<GCEventListener> logger)
    {
        _logger = logger;
    }
    protected override void OnEventSourceCreated(EventSource eventSource)
    {
        if (eventSource.Name.Equals("System.Runtime"))
        {
            EnableEvents(eventSource, EventLevel.Informational, (EventKeywords)(-1));
        }
    }

    protected override void OnEventWritten(EventWrittenEventArgs eventData)
    {
        // Check if the event is related to GC
        if (!string.IsNullOrEmpty(eventData.EventName) && eventData.EventName.Contains("GC"))
        {
            _logger.LogWarning("GC Event: {GCEventName}, Message: {Message}, TimeStamp: {@TimeStamp}, Time generated: {@TimeGenerated}", eventData.EventName, eventData.Message, eventData.TimeStamp, DateTime.UtcNow);
        }
    }
}