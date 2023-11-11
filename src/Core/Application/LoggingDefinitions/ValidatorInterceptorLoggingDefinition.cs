using Application.Helpers;
using Domain.SharedKernel.Base;
using Microsoft.Extensions.Logging;

namespace Application.LoggingDefinitions;

internal static class ValidatorInterceptorLoggingDefinition
{
    private static readonly Action<ILogger, string, string, DateTime, Exception?> StartValidationLoggingDefinition =
        LoggerMessage.Define<string, string, DateTime>(LogLevel.Information, 0,
            "----- TraceId: {TraceId} - Validating command: {CommandType} - Time: {@DateTimeUtc}");
    
    private static readonly Action<ILogger, string, string, DateTime, Exception?> CompletedValidationLoggingDefinition =
        LoggerMessage.Define<string, string, DateTime>(LogLevel.Information, 0,
            "----- TraceId: {TraceId} - Completed validation command: {CommandType} - Time: {@DateTimeUtc}");
    
    private static readonly Action<ILogger, string, string, IReadOnlyList<DomainError>, DateTime, Exception?> ValidateFailureLoggingDefinition =
        LoggerMessage.Define<string, string, IReadOnlyList<DomainError>, DateTime>(LogLevel.Error, 0,
            "----- TraceId: {TraceId} - Validate failure: {CommandType} - Errors: {@ValidationErrors} - Time: {@DateTimeUtc}");
    
    public static void StartValidation(this ILogger logger, string commandTypeName)
    {
        string traceId = Helper.GetTraceId();
        StartValidationLoggingDefinition(logger, traceId, commandTypeName, DateTime.UtcNow, null);
    }
    
    public static void CompletedValidation(this ILogger logger, string commandTypeName)
    {
        string traceId = Helper.GetTraceId();
        CompletedValidationLoggingDefinition(logger, traceId, commandTypeName, DateTime.UtcNow, null);
    }

    public static void ValidateFailure(this ILogger logger, string commandTypeName, IReadOnlyList<DomainError> errors)
    {
        string traceId = Helper.GetTraceId();
        ValidateFailureLoggingDefinition(logger, traceId, commandTypeName, errors, DateTime.UtcNow, null);
    }
}