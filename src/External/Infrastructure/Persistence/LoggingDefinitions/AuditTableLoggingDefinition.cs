using Application.LoggingDefinitions;
using Microsoft.Extensions.Logging;

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
    private const string MessageLoggingDefinition = SharedLoggingDefinition.LoggingMessage + "  - Insertion AuditTable";
    
    private static readonly Action<ILogger, string, DateTime, Exception?> StartLogAuditTableLoggingDefinition =
       LoggerMessage.Define<string, DateTime>(LogLevel.Information, 0,
           MessageLoggingDefinition + " start: Time: {@DateTimeUtc}");

    private static readonly Action<ILogger, string, DateTime, double, Exception?> CompletedLogAuditTableLoggingDefinition =
       LoggerMessage.Define<string, DateTime, double>(LogLevel.Information, 0,
           MessageLoggingDefinition + " completed: Time: {@DateTimeUtc} in {ElapsedMs}ms");

    private static readonly Action<ILogger, string, DateTime, double, Exception?> CompletedLongLogAuditTableLoggingDefinition =
        LoggerMessage.Define<string, DateTime, double>(LogLevel.Warning, 0,
            MessageLoggingDefinition + " completed in a long time: Time: {@DateTimeUtc} in {ElapsedMs}ms");

    public static void StartLogAuditTable(this ILogger logger)
    {
        StartLogAuditTableLoggingDefinition(logger, LoggingTypes.AuditTable, DateTime.UtcNow, null);
    }

    public static void CompletedLogAuditTable(this ILogger logger, double totalMilliseconds)
    {
        if (totalMilliseconds < 2000)
            CompletedLogAuditTableLoggingDefinition(logger, LoggingTypes.AuditTable, DateTime.UtcNow, totalMilliseconds, null);
        else
            CompletedLongLogAuditTableLoggingDefinition(logger, LoggingTypes.AuditTable, DateTime.UtcNow, totalMilliseconds, null);
    }
}
