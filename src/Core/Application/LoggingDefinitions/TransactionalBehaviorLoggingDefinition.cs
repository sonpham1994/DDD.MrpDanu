using Application.Helpers;
using Microsoft.Extensions.Logging;

namespace Application.LoggingDefinitions;

internal static class TransactionalBehaviorLoggingDefinition
{
    private static readonly Action<ILogger, string?, string, DateTime, Exception?> StartTransactionalBehaviorLoggingDefinition =
       LoggerMessage.Define<string?, string, DateTime>(LogLevel.Information, 0,
           "----- TraceId: {TraceId} - Transaction start: RequestName: {RequestName} - Time: {@DateTimeUtc}");

    private static readonly Action<ILogger, string?, string, DateTime, Exception?> CompletedTransactionalBehaviorLoggingDefinition =
       LoggerMessage.Define<string?, string, DateTime>(LogLevel.Information, 0,
           "----- TraceId: {TraceId} - Transaction completed: RequestName: {RequestName} - Time: {@DateTimeUtc}");

    public static void StartTransactionalBehavior(this ILogger logger, string requestName)
    {
        var traceId = Helper.GetTraceId();
        StartTransactionalBehaviorLoggingDefinition(logger, traceId, requestName, DateTime.UtcNow, null);
    }

    public static void CompletedTransactionalBehavior(this ILogger logger, string requestName)
    {
        var traceId = Helper.GetTraceId();
        CompletedTransactionalBehaviorLoggingDefinition(logger, traceId, requestName, DateTime.UtcNow, null);
    }
}
