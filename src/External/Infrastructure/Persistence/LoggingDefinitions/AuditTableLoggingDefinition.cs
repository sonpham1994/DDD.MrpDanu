using Microsoft.Extensions.Logging;
using SharedKernel;

namespace Infrastructure.Persistence.LoggingDefinitions;

/*
 * LoggingDefinition: Application vs Infrastructure
 *  if we put LoggingDefinition of Infrastructure in Application. When we modify LoggingDefinition of Infrastructure in Application, 
 *  application layer force to recompile code. But this maybe rare because we often add LoggingDefinition just once or twice, so we 
 *  can accept putting Logging Definition in Application. However, you cant put LoggingDefinition of Infrastructure in Infrastructure
 *  to achieve Separation of Concenrs
 */
internal static class AuditTableLoggingDefinition
{
    private const string MessageLogging = LoggingDefinition.Prefix + " - Insertion AuditTable";
    private const string SuffixMessage = LoggingDefinition.Suffix + " in {ElapsedMs}ms";
    
    private static readonly Action<ILogger, string, DateTime, Exception?> StartLogAuditTableLoggingDefinition =
       LoggerMessage.Define<string, DateTime>(LogLevel.Information, 0,
           MessageLogging + LoggingDefinition.Start + LoggingDefinition.Suffix);

    private static readonly Action<ILogger, string, double, DateTime, Exception?> CompletedLogAuditTableLoggingDefinition =
       LoggerMessage.Define<string, double, DateTime>(LogLevel.Information, 0,
           MessageLogging + LoggingDefinition.Completed + LoggingDefinition.SuffixWithElapsedSecond);

    private static readonly Action<ILogger, string, double, DateTime, Exception?> CompletedLongLogAuditTableLoggingDefinition =
        LoggerMessage.Define<string, double, DateTime>(LogLevel.Warning, 0,
            MessageLogging + LoggingDefinition.Completed + " in a long time" + LoggingDefinition.SuffixWithElapsedSecond);

    public static void StartLogAuditTable(this ILogger logger)
    {
        StartLogAuditTableLoggingDefinition(logger, LoggingTypes.AuditTable, DateTime.UtcNow, null);
    }

    public static void CompletedLogAuditTable(this ILogger logger, double totalMilliseconds)
    {
        if (totalMilliseconds < 2000)
            CompletedLogAuditTableLoggingDefinition(logger, LoggingTypes.AuditTable, totalMilliseconds, DateTime.UtcNow, null);
        else
            CompletedLongLogAuditTableLoggingDefinition(logger, LoggingTypes.AuditTable, totalMilliseconds, DateTime.UtcNow, null);
    }
}
