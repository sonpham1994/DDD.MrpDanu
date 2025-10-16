using Microsoft.Extensions.Logging;
using SharedKernel;

namespace Application.LoggingDefinitions;

internal static class GcLoggingDefinition
{
    private const string MessageLogging = LoggingDefinition.Prefix + " - Handler: {Handler} start: RequestName: {RequestName}";
    
    private static readonly Action<ILogger, string, string, string, int, int, DateTime, Exception?> StartGcLoggingDefinition =
        LoggerMessage.Define<string, string, string, int, int, DateTime>(LogLevel.Debug, 0,
            MessageLogging + " - at Generation {Generation} has occured with {GCCollectionTimes} times" + LoggingDefinition.Suffix);
    private static readonly Action<ILogger, string, string, string, int, int, DateTime, Exception?> StartGc2LoggingDefinition =
        LoggerMessage.Define<string, string, string, int, int, DateTime>(LogLevel.Critical, 0,
            MessageLogging + " - at Generation {Generation} has occured with {GCCollectionTimes} times" + LoggingDefinition.Suffix);
    
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
            StartGcLoggingDefinition(logger, LoggingTypes.GarbageCollector, handlerName, requestTypeName, 0, currentGen0Count,
                DateTime.UtcNow, null);
        }
        
        if (currentGen1Count > previousGen1Count)
        {
            StartGcLoggingDefinition(logger, LoggingTypes.GarbageCollector, handlerName, requestTypeName, 1, currentGen0Count,
                DateTime.UtcNow, null);
        }

        if (currentGen2Count > previousGen2Count)
        {
            StartGc2LoggingDefinition(logger, LoggingTypes.GarbageCollector, handlerName, requestTypeName, 2, currentGen0Count,
                DateTime.UtcNow, null);
        }
    }
}