using Microsoft.Extensions.Logging;

namespace Infrastructure.Persistence.LoggingDefinitions;

internal static class PerformanceDetectionCommandInterceptorLoggingDefinition
{
    // private static readonly Action<ILogger, string?, string, double, ParameterDbCommand[], DateTime, string, Exception?> StartLogDbCommandLoggingDefinition =
    //    LoggerMessage.Define<string?, string, double, ParameterDbCommand[], DateTime, string>
    //     (
    //        LogLevel.Information,
    //        0,
    //       @"----- TraceId: {TraceId}, {MethodName} - Executed DbCommand ({ExecutedDbCommandTime}) milliseconds, Parameters={@Parameters}, Time={@DateTimeUtcNow}
    //       Command: {CommandText})"
    //     );
    //
    // private static readonly Action<ILogger, string?, string, double, ParameterDbCommand[], DateTime, string, Exception?> StartLogLongDbCommandLoggingDefinition =
    //   LoggerMessage.Define<string?, string, double, ParameterDbCommand[], DateTime, string>
    //    (
    //       LogLevel.Warning,
    //       0,
    //      @"----- TraceId: {TraceId}, {MethodName} - Executed DbCommand ({ExecutedDbCommandTime}) milliseconds, Parameters={@Parameters}, Time={@DateTimeUtcNow}
    //      Command: {CommandText})"
    //    );
    // public static void StartLogDbCommand(this ILogger logger, 
    //     string methodName, 
    //     double durationInMilliSeconds, 
    //     ParameterDbCommand[] parameters, 
    //     string commandText)
    // {
    //     var traceId = Helper.GetTraceId();
    //     StartLogDbCommandLoggingDefinition(logger, traceId, methodName, durationInMilliSeconds, parameters, DateTime.UtcNow, commandText, null);
    // }
    //
    // public static void StartLogLongDbCommand(this ILogger logger,
    //     string methodName,
    //     double durationInMilliSeconds,
    //     ParameterDbCommand[] parameters,
    //     string commandText)
    // {
    //     var traceId = Helper.GetTraceId();
    //     StartLogLongDbCommandLoggingDefinition(logger, traceId, methodName, durationInMilliSeconds, parameters, DateTime.UtcNow, commandText, null);
    // }
    
    private static readonly Action<ILogger, EventId, string, double, DateTime, Exception?> StartLogDbCommandLoggingDefinition =
        LoggerMessage.Define<EventId, string, double, DateTime>
        (
            LogLevel.Information,
            0,
            @"PerformanceDetectionCommandInterceptorLogging - EventId: {@EventId} - {MethodName} - Executed DbCommand ({ExecutedDbCommandTime}) milliseconds, Time={@DateTimeUtcNow}"
        );

    private static readonly Action<ILogger, EventId, string, double,  DateTime, Exception?> StartLogLongDbCommandLoggingDefinition =
        LoggerMessage.Define<EventId, string, double, DateTime>
        (
            LogLevel.Warning,
            0,
            @"PerformanceDetectionCommandInterceptorLogging - EventId: {@EventId} - {MethodName} - Executed DbCommand ({ExecutedDbCommandTime}) milliseconds, Time={@DateTimeUtcNow}"
        );

    public static void StartLogDbCommand(this ILogger logger, 
        string methodName, 
        double durationInMilliSeconds,
        EventId eventId)
    {
        StartLogDbCommandLoggingDefinition(logger, eventId, methodName, durationInMilliSeconds, DateTime.UtcNow, null);
    }

    public static void StartLogLongDbCommand(this ILogger logger,
        string methodName,
        double durationInMilliSeconds,
        EventId eventId)
    {
        StartLogLongDbCommandLoggingDefinition(logger, eventId, methodName, durationInMilliSeconds, DateTime.UtcNow, null);
    }
}
