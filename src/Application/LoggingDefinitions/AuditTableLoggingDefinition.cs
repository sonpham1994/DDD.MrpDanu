using Application.Helpers;
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
    private static readonly Action<ILogger, string, DateTime, Exception?> StartLogAuditTableLoggingDefinition =
       LoggerMessage.Define<string, DateTime>(LogLevel.Information, 0,
           "----- TraceId: {TraceId} - Log AuditTable start: Time: {@DateTimeUtc}");

    private static readonly Action<ILogger, string, DateTime, double, Exception?> CompletedLogAuditTableLoggingDefinition =
       LoggerMessage.Define<string, DateTime, double>(LogLevel.Information, 0,
           "----- TraceId: {TraceId} - Log AuditTable completed: Time: {@DateTimeUtc} in {ElapsedMs}ms");

    private static readonly Action<ILogger, string, DateTime, double, Exception?> CompletedLongLogAuditTableLoggingDefinition =
        LoggerMessage.Define<string, DateTime, double>(LogLevel.Warning, 0,
            "----- TraceId: {TraceId} - Log AuditTable completed in a long time: Time: {@DateTimeUtc} in {ElapsedMs}ms");

    public static void StartLogAuditTable(this ILogger logger)
    {
        string traceId = Helper.GetTraceId();
        StartLogAuditTableLoggingDefinition(logger, traceId, DateTime.UtcNow, null);
    }

    public static void CompletedLogAuditTable(this ILogger logger, double totalMilliseconds)
    {
        string traceId = Helper.GetTraceId();
        if (totalMilliseconds < 2000)
            CompletedLogAuditTableLoggingDefinition(logger, traceId, DateTime.UtcNow, totalMilliseconds, null);
        else
            CompletedLongLogAuditTableLoggingDefinition(logger, traceId, DateTime.UtcNow, totalMilliseconds, null);
    }
}
