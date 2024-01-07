using Application.Helpers;
using Microsoft.Extensions.Logging;

namespace Application.LoggingDefinitions;

internal static class GCLoggingDefinition
{
    private static readonly Action<ILogger, string, string, string, int, int, DateTime, Exception?> StartGCLoggingDefinition =
        LoggerMessage.Define<string, string, string, int, int, DateTime>(LogLevel.Debug, 0,
            "----- TraceId: {TraceId} - Handler: {Handler} start: RequestName: {RequestName} - at Generation {Generation} has occured with {GCCollectionTimes} times - Time: {@DateTimeUtc}");
    private static readonly Action<ILogger, string, string, string, int, int, DateTime, Exception?> StartGC2LoggingDefinition =
        LoggerMessage.Define<string, string, string, int, int, DateTime>(LogLevel.Critical, 0,
            "----- TraceId: {TraceId} - Handler: {Handler} start: RequestName: {RequestName} - at Generation {Generation} has occured with {GCCollectionTimes} times - Time: {@DateTimeUtc}");
    
    public static void GCGenerationLogging(this ILogger logger, 
        string handlerName, 
        string requestTypeName, 
        int previousGen0Count, 
        int previousGen1Count, 
        int previousGen2Count)
    {
        string traceId = Helper.GetTraceId();
        var currentGen0Count = GC.CollectionCount(0);
        var currentGen1Count = GC.CollectionCount(1);
        var currentGen2Count = GC.CollectionCount(2);

        if (currentGen0Count > previousGen0Count)
        {
            StartGCLoggingDefinition(logger, traceId, handlerName, requestTypeName, 0, currentGen0Count,
                DateTime.UtcNow, null);
        }
        
        if (currentGen1Count > previousGen1Count)
        {
            StartGCLoggingDefinition(logger, traceId, handlerName, requestTypeName, 1, currentGen0Count,
                DateTime.UtcNow, null);
        }

        if (currentGen2Count > previousGen2Count)
        {
            StartGC2LoggingDefinition(logger, traceId, handlerName, requestTypeName, 2, currentGen0Count,
                DateTime.UtcNow, null);
        }
    }
}