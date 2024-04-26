namespace Api.LoggingDefinitions;

public static class ValidationOptionLoggingDefinition
{
    private static readonly Action<ILogger, string, DateTime, Exception?> _startValidationOption =
       LoggerMessage.Define<string, DateTime>(LogLevel.Information, 0,
           "Start ValidationOption: {ValidationOptionType}, Time: {@DateTimeUtc}");

    private static readonly Action<ILogger, string, DateTime, Exception?> _completedValidationOption =
       LoggerMessage.Define<string, DateTime>(LogLevel.Information, 0,
           "Completed ValidationOption: {ValidationOptionType}, Time: {@DateTimeUtc}");

    private static readonly Action<ILogger, string, string, DateTime, Exception?> _validateOptionWithError =
        LoggerMessage.Define<string, string, DateTime>(LogLevel.Error, 0,
            "Validation Option with error '{ErrorValidationOptionType}': {ErrorValidationOptionTypeMessage}, Time: {@DateTimeUtc}");

    public static void StartValidationOption(this ILogger logger, string validationOptionType)
    {
        _startValidationOption(logger, validationOptionType, DateTime.UtcNow, null);
    }

    public static void CompletedValidationOption(this ILogger logger, string validationOptionType)
    {
        _completedValidationOption(logger, validationOptionType, DateTime.UtcNow, null);
    }

    public static void ValidateOptionWithError(this ILogger logger, string validationOptionType, string errorMessage)
    {
        _validateOptionWithError(logger, validationOptionType, errorMessage, DateTime.UtcNow, null);
    }
}