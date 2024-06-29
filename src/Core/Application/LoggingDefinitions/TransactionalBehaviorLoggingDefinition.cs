using Microsoft.Extensions.Logging;

namespace Application.LoggingDefinitions;

internal static class TransactionalBehaviorLoggingDefinition
{
    private static readonly Action<ILogger, string, DateTime, Exception?> StartTransactionalBehaviorLoggingDefinition =
       LoggerMessage.Define<string, DateTime>(LogLevel.Information, 0,
           "TransactionalBehaviorLogging - Transaction start: RequestName: {RequestName} - Time: {@DateTimeUtc}");

    private static readonly Action<ILogger, string, DateTime, Exception?> CompletedTransactionalBehaviorLoggingDefinition =
       LoggerMessage.Define<string, DateTime>(LogLevel.Information, 0,
           "TransactionalBehaviorLogging - Transaction completed: RequestName: {RequestName} - Time: {@DateTimeUtc}");

    public static void StartTransactionalBehavior(this ILogger logger, string requestName)
    {
        StartTransactionalBehaviorLoggingDefinition(logger, requestName, DateTime.UtcNow, null);
    }

    public static void CompletedTransactionalBehavior(this ILogger logger, string requestName)
    {
        CompletedTransactionalBehaviorLoggingDefinition(logger, requestName, DateTime.UtcNow, null);
    }
}
