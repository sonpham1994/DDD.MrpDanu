using Application.Helpers;
using Domain.SharedKernel.Base;
using Microsoft.Extensions.Logging;

namespace Application.LoggingDefinitions;

internal static class HandlerLoggingInterceptorLoggingDefinition
{
    private static readonly Action<ILogger, string, string, string, DateTime, Exception?> StartHandlerLoggingDefinition =
        LoggerMessage.Define<string, string, string, DateTime>(LogLevel.Information, 0,
            "----- TraceId: {TraceId} - Handler: {Handler} start: RequestName: {RequestName} - Time: {@DateTimeUtc}");

    private static readonly Action<ILogger, string, string, string, DateTime, double, Exception?> CompletedHandlerLoggingDefinition =
            LoggerMessage.Define<string, string, string, DateTime, double>(LogLevel.Information, 0,
                "----- TraceId: {TraceId} - Handler: {Handler} completed: RequestName: {RequestName} - Time: {@DateTimeUtc} in {ElapsedMs}ms");

    private static readonly Action<ILogger, string, string, string, DateTime, double, Exception?> CompletedLongHandlerLoggingDefinition =
            LoggerMessage.Define<string, string, string, DateTime, double>(LogLevel.Warning, 0,
                "----- TraceId: {TraceId} - Handler: {Handler} completed in a long time: RequestName: {RequestName} - Time: {@DateTimeUtc} in {ElapsedMs}ms");

    private static readonly Action<ILogger, string, string, string, DomainError, DateTime, double, Exception?> HandleFailureLoggingDefinition =
            LoggerMessage.Define<string, string, string, DomainError, DateTime, double>(LogLevel.Error, 0,
                "----- TraceId: {TraceId} - Handler: {Handler} handled with error: RequestName: {RequestName} - Error: {@DomainError} - Time: {@DateTimeUtc} in {ElapsedMs}ms");


    public static void StartHandler(this ILogger logger, string handlerName, string requestTypeName)
    {
        string traceId = Helper.GetTraceId();
        StartHandlerLoggingDefinition(logger, traceId, handlerName, requestTypeName, DateTime.UtcNow, null);
    }
    
    public static void CompletedHandler(this ILogger logger, string handlerName, string requestTypeName, double milliseconds)
    {
        string traceId = Helper.GetTraceId();
        if (milliseconds <= 2000)
            CompletedHandlerLoggingDefinition(logger, traceId, handlerName, requestTypeName, DateTime.UtcNow, milliseconds, null);
        else
            CompletedLongHandlerLoggingDefinition(logger, traceId, handlerName, requestTypeName, DateTime.UtcNow, milliseconds, null);
    }
    
    public static void HandleFailure(this ILogger logger, string handlerName, string requestTypeName, in DomainError error, double milliseconds)
    {
        string traceId = Helper.GetTraceId();
        HandleFailureLoggingDefinition(logger, traceId, handlerName, requestTypeName, error, DateTime.UtcNow, milliseconds, null);
    }
}