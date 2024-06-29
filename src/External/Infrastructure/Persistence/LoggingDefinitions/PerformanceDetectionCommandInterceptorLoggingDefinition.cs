using Microsoft.EntityFrameworkCore.Diagnostics;
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
    
    private static readonly Action<ILogger, string, EventId, string, double,  DateTime, string, Exception?> StartLogLongDbCommandLoggingDefinition =
        LoggerMessage.Define<string, EventId, string, double, DateTime, string>
        (
            LogLevel.Warning,
            0,
            """
            LoggingType: {LoggingType} - EventId: {@EventId} - {MethodName} - Executed DbCommand ({ExecutedDbCommandTime}) milliseconds in a long time, Time={@DateTimeUtcNow}
            CommandText: {CommandText}
            """
        );

    public static void StartLogLongDbCommand(this ILogger logger,
        string methodName,
        double durationInMilliSeconds,
        CommandExecutedEventData commandExecutedEventData)
    {
        StartLogLongDbCommandLoggingDefinition(
            logger,
            LoggingTypes.PerformanceDetectionCommandInterceptor,
            commandExecutedEventData.EventId, 
            methodName, 
            durationInMilliSeconds, 
            DateTime.UtcNow, 
            commandExecutedEventData.Command.CommandText, 
            null);
    }
}
