using Microsoft.Extensions.Logging;

namespace Application.LoggingDefinitions;

internal static class TransactionalBehaviorLoggingDefinition
{
    private const string MessageLoggingDefinition = SharedLoggingDefinition.LoggingMessage + " - Transactional operation's {RequestName}";
    
    private static readonly Action<ILogger, string, string, DateTime, Exception?> StartTransactionalBehaviorLoggingDefinition =
       LoggerMessage.Define<string, string, DateTime>(LogLevel.Information, 0,
           MessageLoggingDefinition + " start - Time: {@DateTimeUtc}");

    private static readonly Action<ILogger, string, string, DateTime, Exception?> CompletedTransactionalBehaviorLoggingDefinition =
       LoggerMessage.Define<string, string, DateTime>(LogLevel.Information, 0,
           MessageLoggingDefinition + " completed - Time: {@DateTimeUtc}");

    public static void StartTransactionalBehavior(this ILogger logger, string requestName)
    {
        StartTransactionalBehaviorLoggingDefinition(logger, LoggingTypes.TransactionalBehavior, requestName, DateTime.UtcNow, null);
    }

    public static void CompletedTransactionalBehavior(this ILogger logger, string requestName)
    {
        CompletedTransactionalBehaviorLoggingDefinition(logger, LoggingTypes.TransactionalBehavior, requestName, DateTime.UtcNow, null);
    }
}
