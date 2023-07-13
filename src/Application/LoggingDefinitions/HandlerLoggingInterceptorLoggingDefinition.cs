using Application.Helpers;
using Domain.SharedKernel.Base;
using Microsoft.Extensions.Logging;

namespace Application.LoggingDefinitions;

internal static class HandlerLoggingInterceptorLoggingDefinition
{
    private static readonly Action<ILogger, string, string, string, DateTime, Exception?> StartHandlerLoggingDefinition =
        LoggerMessage.Define<string, string, string, DateTime>(LogLevel.Information, 0,
            "----- TraceId: {TraceId} - Handler: {Handler} start: RequestName: {RequestName} - Time: {@DateTimeUtc}");

    private static readonly Action<ILogger, string, string, string, DateTime, Exception?> CompletedHandlerLoggingDefinition =
            LoggerMessage.Define<string, string, string, DateTime>(LogLevel.Information, 0,
                "----- TraceId: {TraceId} - Handler: {Handler} completed: RequestName: {RequestName} - Time: {@DateTimeUtc}");

    private static readonly Action<ILogger, string, string, string, DomainError, DateTime, Exception?> HandleFailureLoggingDefinition =
            LoggerMessage.Define<string, string, string, DomainError, DateTime>(LogLevel.Error, 0,
                "----- TraceId: {TraceId} - Handler: {Handler} handled with error: RequestName: {RequestName} - Error: {@DomainError} - Time: {@DateTimeUtc}");


    public static void StartHandler(this ILogger logger, string handlerName, string requestTypeName)
    {
        string traceId = Helper.GetTraceId();
        StartHandlerLoggingDefinition(logger, traceId, handlerName, requestTypeName, DateTime.UtcNow, null);
    }
    
    public static void CompletedHandler(this ILogger logger, string handlerName, string requestTypeName)
    {
        string traceId = Helper.GetTraceId();
        CompletedHandlerLoggingDefinition(logger, traceId, handlerName, requestTypeName, DateTime.UtcNow, null);
    }
    
    public static void HandleFailure(this ILogger logger, string handlerName, string requestTypeName, DomainError error)
    {
        string traceId = Helper.GetTraceId();
        HandleFailureLoggingDefinition(logger, traceId, handlerName, requestTypeName, error, DateTime.UtcNow, null);
    }
}