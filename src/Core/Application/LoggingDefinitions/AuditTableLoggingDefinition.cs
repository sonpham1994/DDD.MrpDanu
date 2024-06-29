using Microsoft.Extensions.Logging;

namespace Application.LoggingDefinitions;

/*
 * LoggingDefinition: Application vs Infrastructure
 *  if we put LoggingDefinition of Infrastructure in Application. When we modify LoggingDefinition of Infrastructure in Application, 
 *  application layer force to recompile code. But this maybe rare because we often add LoggingDefinition just once or twice, so we 
 *  can accept putting Logging Definition in Application. However, you cant put LoggingDefinition of Infrastructure in Infrastructure
 *  to achieve Separation of Concenrs
 */
internal static class AuditTableLoggingDefinition
{
    private static readonly Action<ILogger, DateTime, Exception?> StartLogAuditTableLoggingDefinition =
       LoggerMessage.Define<DateTime>(LogLevel.Information, 0,
           "AuditTableLogging - Log AuditTable start: Time: {@DateTimeUtc}");

    private static readonly Action<ILogger, DateTime, double, Exception?> CompletedLogAuditTableLoggingDefinition =
       LoggerMessage.Define<DateTime, double>(LogLevel.Information, 0,
           "AuditTableLogging - Log AuditTable completed: Time: {@DateTimeUtc} in {ElapsedMs}ms");

    private static readonly Action<ILogger, DateTime, double, Exception?> CompletedLongLogAuditTableLoggingDefinition =
        LoggerMessage.Define<DateTime, double>(LogLevel.Warning, 0,
            "AuditTableLogging - Log AuditTable completed in a long time: Time: {@DateTimeUtc} in {ElapsedMs}ms");

    public static void StartLogAuditTable(this ILogger logger)
    {
        StartLogAuditTableLoggingDefinition(logger, DateTime.UtcNow, null);
    }

    public static void CompletedLogAuditTable(this ILogger logger, double totalMilliseconds)
    {
        if (totalMilliseconds < 2000)
            CompletedLogAuditTableLoggingDefinition(logger, DateTime.UtcNow, totalMilliseconds, null);
        else
            CompletedLongLogAuditTableLoggingDefinition(logger, DateTime.UtcNow, totalMilliseconds, null);
    }
}
