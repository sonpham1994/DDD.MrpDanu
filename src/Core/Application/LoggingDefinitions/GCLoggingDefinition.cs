using Microsoft.Extensions.Logging;

namespace Application.LoggingDefinitions;

internal static class GcLoggingDefinition
{
    private static readonly Action<ILogger, string, string, int, int, DateTime, Exception?> StartGcLoggingDefinition =
        LoggerMessage.Define<string, string, int, int, DateTime>(LogLevel.Debug, 0,
            "GcLogging - Handler: {Handler} start: RequestName: {RequestName} - at Generation {Generation} has occured with {GCCollectionTimes} times - Time: {@DateTimeUtc}");
    private static readonly Action<ILogger, string, string, int, int, DateTime, Exception?> StartGc2LoggingDefinition =
        LoggerMessage.Define<string, string, int, int, DateTime>(LogLevel.Critical, 0,
            "GcLogging - Handler: {Handler} start: RequestName: {RequestName} - at Generation {Generation} has occured with {GCCollectionTimes} times - Time: {@DateTimeUtc}");
    
    public static void GcGenerationLogging(this ILogger logger, 
        string handlerName, 
        string requestTypeName, 
        int previousGen0Count, 
        int previousGen1Count, 
        int previousGen2Count)
    {
        var currentGen0Count = GC.CollectionCount(0);
        var currentGen1Count = GC.CollectionCount(1);
        var currentGen2Count = GC.CollectionCount(2);

        if (currentGen0Count > previousGen0Count)
        {
            StartGcLoggingDefinition(logger, handlerName, requestTypeName, 0, currentGen0Count,
                DateTime.UtcNow, null);
        }
        
        if (currentGen1Count > previousGen1Count)
        {
            StartGcLoggingDefinition(logger, handlerName, requestTypeName, 1, currentGen0Count,
                DateTime.UtcNow, null);
        }

        if (currentGen2Count > previousGen2Count)
        {
            StartGc2LoggingDefinition(logger, handlerName, requestTypeName, 2, currentGen0Count,
                DateTime.UtcNow, null);
        }
    }
}