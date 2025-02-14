using SharedKernel;
using Microsoft.Extensions.Logging;

namespace Application.LoggingDefinitions;

internal static class HandlerLoggingInterceptorLoggingDefinition
{
    private const string MessageLogging = SharedLoggingDefinition.Prefix + " - Handler: {Handler} - RequestName: {RequestName}";
    private const string SuffixMessage = SharedLoggingDefinition.Suffix + " in {ElapsedMs}ms";
    
    private static readonly Action<ILogger, string, string, string, DateTime, Exception?> StartHandlerLoggingDefinition =
        LoggerMessage.Define<string, string, string, DateTime>(LogLevel.Information, 0,
            MessageLogging + SharedLoggingDefinition.Start + SharedLoggingDefinition.Suffix);

    private static readonly Action<ILogger, string, string, string, DateTime, double, Exception?> CompletedHandlerLoggingDefinition =
            LoggerMessage.Define<string, string, string, DateTime, double>(LogLevel.Information, 0,
                MessageLogging + SharedLoggingDefinition.Completed + SuffixMessage);

    private static readonly Action<ILogger, string, string, string, DateTime, double, Exception?> CompletedLongHandlerLoggingDefinition =
            LoggerMessage.Define<string, string, string, DateTime, double>(LogLevel.Warning, 0,
                MessageLogging + SharedLoggingDefinition.Completed + " in a long time" + SuffixMessage);

    private static readonly Action<ILogger, string, string, string, DomainError, DateTime, double, Exception?> HandleFailureLoggingDefinition =
            LoggerMessage.Define<string, string, string, DomainError, DateTime, double>(LogLevel.Warning, 0,
                MessageLogging + SharedLoggingDefinition.FailedWithDomainError + SuffixMessage);

    public static void StartHandler(this ILogger logger, string handlerName, string requestTypeName)
    {
        StartHandlerLoggingDefinition(logger, LoggingTypes.Handler, handlerName, requestTypeName, DateTime.UtcNow, null);
    }
    
    public static void CompletedHandler<TResponse>(
        this ILogger logger, 
        string handlerName, 
        string requestTypeName, 
        TResponse response,
        double totalMilliseconds)
        where TResponse : IResult
    {
        if (!response.IsSuccess)
        {
            HandleFailureLoggingDefinition(logger, LoggingTypes.Handler, handlerName, requestTypeName, response.Error, DateTime.UtcNow, totalMilliseconds, null);
        }
        else if (totalMilliseconds <= 2000)
        {
            CompletedHandlerLoggingDefinition(logger, LoggingTypes.Handler, handlerName, requestTypeName, DateTime.UtcNow, totalMilliseconds, null);
        }
        else
        {
            CompletedLongHandlerLoggingDefinition(logger, LoggingTypes.Handler, handlerName, requestTypeName, DateTime.UtcNow, totalMilliseconds, null);
        }
    }
}