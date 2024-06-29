using Domain.SharedKernel.Base;
using Microsoft.Extensions.Logging;

namespace Application.LoggingDefinitions;

internal static class HandlerLoggingInterceptorLoggingDefinition
{
    private static readonly Action<ILogger, string, string, DateTime, Exception?> StartHandlerLoggingDefinition =
        LoggerMessage.Define<string, string, DateTime>(LogLevel.Information, 0,
            "HandlerLogging - Handler: {Handler} start: RequestName: {RequestName} - Time: {@DateTimeUtc}");

    private static readonly Action<ILogger, string, string, DateTime, double, Exception?> CompletedHandlerLoggingDefinition =
            LoggerMessage.Define<string, string, DateTime, double>(LogLevel.Information, 0,
                "HandlerLogging - Handler: {Handler} completed: RequestName: {RequestName} - Time: {@DateTimeUtc} in {ElapsedMs}ms");

    private static readonly Action<ILogger, string, string, DateTime, double, Exception?> CompletedLongHandlerLoggingDefinition =
            LoggerMessage.Define<string, string, DateTime, double>(LogLevel.Warning, 0,
                "HandlerLogging - Handler: {Handler} completed in a long time: RequestName: {RequestName} - Time: {@DateTimeUtc} in {ElapsedMs}ms");

    private static readonly Action<ILogger, string, string, DomainError, DateTime, double, Exception?> HandleFailureLoggingDefinition =
            LoggerMessage.Define<string, string, DomainError, DateTime, double>(LogLevel.Error, 0,
                "HandlerLogging - Handler: {Handler} handled with error: RequestName: {RequestName} - Error: {@DomainError} - Time: {@DateTimeUtc} in {ElapsedMs}ms");


    public static void StartHandler(this ILogger logger, string handlerName, string requestTypeName)
    {
        StartHandlerLoggingDefinition(logger, handlerName, requestTypeName, DateTime.UtcNow, null);
    }
    
    public static void CompletedHandler(this ILogger logger, string handlerName, string requestTypeName, double totalMilliseconds)
    {
        if (totalMilliseconds <= 2000)
            CompletedHandlerLoggingDefinition(logger, handlerName, requestTypeName, DateTime.UtcNow, totalMilliseconds, null);
        else
            CompletedLongHandlerLoggingDefinition(logger, handlerName, requestTypeName, DateTime.UtcNow, totalMilliseconds, null);
    }
    
    public static void HandleFailure(this ILogger logger, string handlerName, string requestTypeName, in DomainError error, double totalMilliseconds)
    {
        HandleFailureLoggingDefinition(logger, handlerName, requestTypeName, error, DateTime.UtcNow, totalMilliseconds, null);
    }
}