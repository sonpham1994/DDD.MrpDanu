using Microsoft.Extensions.Logging;
using SharedKernel;

namespace Application.LoggingDefinitions;

internal static class TransactionalBehaviorLoggingDefinition
{
    private const string MessageLogging = LoggingDefinition.Prefix + " - Transactional operation's {RequestName}";
    
    private static readonly Action<ILogger, string, string, DateTime, Exception?> StartTransactionalBehaviorLoggingDefinition =
       LoggerMessage.Define<string, string, DateTime>(LogLevel.Information, 0,
           MessageLogging + LoggingDefinition.Start + LoggingDefinition.Suffix);

    private static readonly Action<ILogger, string, string, DateTime, Exception?> CompletedTransactionalBehaviorLoggingDefinition =
       LoggerMessage.Define<string, string, DateTime>(LogLevel.Information, 0,
           MessageLogging + LoggingDefinition.Completed + LoggingDefinition.Suffix);

    public static void StartTransactionalBehavior(this ILogger logger, string requestName)
    {
        StartTransactionalBehaviorLoggingDefinition(logger, LoggingTypes.TransactionalBehavior, requestName, DateTime.UtcNow, null);
    }

    public static void CompletedTransactionalBehavior(this ILogger logger, string requestName)
    {
        CompletedTransactionalBehaviorLoggingDefinition(logger, LoggingTypes.TransactionalBehavior, requestName, DateTime.UtcNow, null);
    }
}
