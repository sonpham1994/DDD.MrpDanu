using SharedKernel;
using Microsoft.Extensions.Logging;

namespace Application.LoggingDefinitions;

internal static class ValidatorInterceptorLoggingDefinition
{
    private const string MessageLoggingDefinition = SharedLoggingDefinition.LoggingMessage + " - Command Validation's {CommandType}";
    
    private static readonly Action<ILogger, string, string, DateTime, Exception?> StartValidationLoggingDefinition =
        LoggerMessage.Define<string, string, DateTime>(LogLevel.Information, 0,
            MessageLoggingDefinition + " start - Time: {@DateTimeUtc}");
    
    private static readonly Action<ILogger, string, string, DateTime, Exception?> CompletedValidationLoggingDefinition =
        LoggerMessage.Define<string, string, DateTime>(LogLevel.Information, 0,
            MessageLoggingDefinition + " completed  - Time: {@DateTimeUtc}");
    
    private static readonly Action<ILogger, string, string, IReadOnlyList<DomainError>, DateTime, Exception?> ValidateFailureLoggingDefinition =
        LoggerMessage.Define<string, string, IReadOnlyList<DomainError>, DateTime>(LogLevel.Warning, 0,
            MessageLoggingDefinition + " failed - Errors: {@ValidationErrors} - Time: {@DateTimeUtc}");
    
    public static void StartValidation(this ILogger logger, string commandTypeName)
    {
        StartValidationLoggingDefinition(logger, LoggingTypes.ValidatorInterceptor, commandTypeName, DateTime.UtcNow, null);
    }
    
    public static void CompletedValidation(this ILogger logger, string commandTypeName)
    {
        CompletedValidationLoggingDefinition(logger, LoggingTypes.ValidatorInterceptor, commandTypeName, DateTime.UtcNow, null);
    }

    public static void ValidateFailure(this ILogger logger, string commandTypeName, IReadOnlyList<DomainError> errors)
    {
        ValidateFailureLoggingDefinition(logger, LoggingTypes.ValidatorInterceptor, commandTypeName, errors, DateTime.UtcNow, null);
    }
}