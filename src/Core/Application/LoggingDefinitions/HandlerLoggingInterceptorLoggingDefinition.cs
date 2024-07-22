using SharedKernel;
using Microsoft.Extensions.Logging;

namespace Application.LoggingDefinitions;

internal static class HandlerLoggingInterceptorLoggingDefinition
{
    private static readonly Action<ILogger, string, string, string, DateTime, Exception?> StartHandlerLoggingDefinition =
        LoggerMessage.Define<string, string, string, DateTime>(LogLevel.Information, 0,
            "LoggingType: {LoggingType} - HandlerName: {HandlerName} start: RequestName: {RequestName} - Time: {@DateTimeUtc}");

    private static readonly Action<ILogger, string, string, string, DateTime, double, Exception?> CompletedHandlerLoggingDefinition =
            LoggerMessage.Define<string, string, string, DateTime, double>(LogLevel.Information, 0,
                "LoggingType: {LoggingType} - HandlerName: {HandlerName} completed: RequestName: {RequestName} - Time: {@DateTimeUtc} in {ElapsedMs}ms");

    private static readonly Action<ILogger, string, string, string, DateTime, double, Exception?> CompletedLongHandlerLoggingDefinition =
            LoggerMessage.Define<string, string, string, DateTime, double>(LogLevel.Warning, 0,
                "LoggingType: {LoggingType} - HandlerName: {HandlerName} completed in a long time: RequestName: {RequestName} - Time: {@DateTimeUtc} in {ElapsedMs}ms");

    private static readonly Action<ILogger, string, string, string, DomainError, DateTime, double, Exception?> HandleFailureLoggingDefinition =
            LoggerMessage.Define<string, string, string, DomainError, DateTime, double>(LogLevel.Warning, 0,
                "LoggingType: {LoggingType} - HandlerName: {HandlerName} handled with error: RequestName: {RequestName} - Error: {@DomainError} - Time: {@DateTimeUtc} in {ElapsedMs}ms");


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