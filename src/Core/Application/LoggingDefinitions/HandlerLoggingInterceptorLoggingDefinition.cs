using SharedKernel;
using Microsoft.Extensions.Logging;

namespace Application.LoggingDefinitions;

internal static class HandlerLoggingInterceptorLoggingDefinition
{
    private const string MessageLogging = SharedLoggingDefinition.Prefix + " - Handler: {Handler} - RequestName: {RequestName}";
    
    private static readonly Action<ILogger, string, string, string, DateTime, Exception?> StartHandlerLoggingDefinition =
        LoggerMessage.Define<string, string, string, DateTime>(LogLevel.Information, 0,
            MessageLogging + SharedLoggingDefinition.Start + SharedLoggingDefinition.Suffix);

    private static readonly Action<ILogger, string, string, string, double, DateTime, Exception?> CompletedHandlerLoggingDefinition =
            LoggerMessage.Define<string, string, string, double, DateTime>(LogLevel.Information, 0,
                MessageLogging + SharedLoggingDefinition.Completed + SharedLoggingDefinition.SuffixWithElapsedSecond);

    private static readonly Action<ILogger, string, string, string, double, DateTime, Exception?> CompletedLongHandlerLoggingDefinition =
            LoggerMessage.Define<string, string, string, double, DateTime>(LogLevel.Warning, 0,
                MessageLogging + SharedLoggingDefinition.Completed + " in a long time" + SharedLoggingDefinition.SuffixWithElapsedSecond);

    private static readonly Action<ILogger, string, string, string, DomainError, double, DateTime, Exception?> HandleFailureLoggingDefinition =
            LoggerMessage.Define<string, string, string, DomainError, double, DateTime>(LogLevel.Warning, 0,
                MessageLogging + SharedLoggingDefinition.FailedWithDomainError + SharedLoggingDefinition.SuffixWithElapsedSecond);

    public static void StartHandler(this ILogger logger, string handlerName, string requestTypeName)
    {
        StartHandlerLoggingDefinition(logger, LoggingTypes.Handler, handlerName, requestTypeName, DateTime.UtcNow, null);
    }
    
    public static void CompletedHandler<TResponse>(
        this ILogger logger, 
        string handlerName, 
        string requestName, 
        TResponse response,
        double totalMilliseconds)
        where TResponse : IResult
    {
        if (!response.IsSuccess)
        {
            HandleFailureLoggingDefinition(logger, LoggingTypes.Handler, handlerName, requestName, response.Error, totalMilliseconds, DateTime.UtcNow, null);
        }
        else if (totalMilliseconds <= 2000)
        {
            CompletedHandlerLoggingDefinition(logger, LoggingTypes.Handler, handlerName, requestName, totalMilliseconds, DateTime.UtcNow, null);
        }
        else
        {
            CompletedLongHandlerLoggingDefinition(logger, LoggingTypes.Handler, handlerName, requestName, totalMilliseconds, DateTime.UtcNow, null);
        }
    }
}