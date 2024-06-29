using Domain.SharedKernel.Base;
using Microsoft.Extensions.Logging;

namespace Application.LoggingDefinitions;

internal static class ValidatorInterceptorLoggingDefinition
{
    private static readonly Action<ILogger, string, DateTime, Exception?> StartValidationLoggingDefinition =
        LoggerMessage.Define<string, DateTime>(LogLevel.Information, 0,
            "ValidatorInterceptorLogging - Validating command: {CommandType} - Time: {@DateTimeUtc}");
    
    private static readonly Action<ILogger, string, DateTime, Exception?> CompletedValidationLoggingDefinition =
        LoggerMessage.Define<string, DateTime>(LogLevel.Information, 0,
            "ValidatorInterceptorLogging - Completed validation command: {CommandType} - Time: {@DateTimeUtc}");
    
    private static readonly Action<ILogger, string, IReadOnlyList<DomainError>, DateTime, Exception?> ValidateFailureLoggingDefinition =
        LoggerMessage.Define<string, IReadOnlyList<DomainError>, DateTime>(LogLevel.Error, 0,
            "ValidatorInterceptorLogging - Validate failure: {CommandType} - Errors: {@ValidationErrors} - Time: {@DateTimeUtc}");
    
    public static void StartValidation(this ILogger logger, string commandTypeName)
    {
        StartValidationLoggingDefinition(logger, commandTypeName, DateTime.UtcNow, null);
    }
    
    public static void CompletedValidation(this ILogger logger, string commandTypeName)
    {
        CompletedValidationLoggingDefinition(logger, commandTypeName, DateTime.UtcNow, null);
    }

    public static void ValidateFailure(this ILogger logger, string commandTypeName, IReadOnlyList<DomainError> errors)
    {
        ValidateFailureLoggingDefinition(logger, commandTypeName, errors, DateTime.UtcNow, null);
    }
}