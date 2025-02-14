using Microsoft.Extensions.Logging;

namespace Application.LoggingDefinitions;

internal static class TransactionalBehaviorLoggingDefinition
{
    private const string MessageLogging = SharedLoggingDefinition.Prefix + " - Transactional operation's {RequestName}";
    
    private static readonly Action<ILogger, string, string, DateTime, Exception?> StartTransactionalBehaviorLoggingDefinition =
       LoggerMessage.Define<string, string, DateTime>(LogLevel.Information, 0,
           MessageLogging + SharedLoggingDefinition.Start + SharedLoggingDefinition.Suffix);

    private static readonly Action<ILogger, string, string, DateTime, Exception?> CompletedTransactionalBehaviorLoggingDefinition =
       LoggerMessage.Define<string, string, DateTime>(LogLevel.Information, 0,
           MessageLogging + SharedLoggingDefinition.Completed + SharedLoggingDefinition.Suffix);

    public static void StartTransactionalBehavior(this ILogger logger, string requestName)
    {
        StartTransactionalBehaviorLoggingDefinition(logger, LoggingTypes.TransactionalBehavior, requestName, DateTime.UtcNow, null);
    }

    public static void CompletedTransactionalBehavior(this ILogger logger, string requestName)
    {
        CompletedTransactionalBehaviorLoggingDefinition(logger, LoggingTypes.TransactionalBehavior, requestName, DateTime.UtcNow, null);
    }
}
