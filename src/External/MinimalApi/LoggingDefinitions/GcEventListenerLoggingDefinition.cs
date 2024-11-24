namespace MinimalApi.LoggingDefinitions;

public static class GcEventListenerLoggingDefinition
{
    private static readonly Action<ILogger, string, string, IDictionary<string, object>, DateTime, Exception?> GcEventOccurrence =
        LoggerMessage.Define<string, string, IDictionary<string, object>, DateTime>(LogLevel.Warning, 0,
            "LoggingType: {LoggingType} - GC Event: {GCEventName}, GcData: {@GcData}, Time generated: {@TimeGenerated}");

    private static readonly Action<ILogger, string, string?, int, int, DateTime, Exception?> GcEventOccurrenceAtGeneration =
        LoggerMessage.Define<string, string?, int, int, DateTime>(LogLevel.Warning, 0,
    "LoggingType: {LoggingType} - GC Event: {GCEventName} at Generation {GenerationNumber} has occured with {GCCollectionTimes} times, Time generated: {@TimeGenerated}");
        
    public static void LogGcEvent(this ILogger logger, string eventName, IDictionary<string, object> eventPayload)
    {
        GcEventOccurrence(logger, LoggingTypes.GcEventListener, eventName, eventPayload, DateTime.UtcNow, null);
    }
    
    public static void LogGcEventOccurenceAtGeneration(
        this ILogger logger, 
        string? eventName, 
        int generationNumber, 
        int generationCollectionTimes,
        DateTime timeStamp)
    {
        GcEventOccurrenceAtGeneration(logger, LoggingTypes.GcEventListener, eventName, generationNumber, generationCollectionTimes, timeStamp, null);
    }
}