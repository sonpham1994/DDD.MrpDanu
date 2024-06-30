namespace Api.LoggingDefinitions;

public static class ValidationOptionLoggingDefinition
{
    private static readonly Action<ILogger, string, string, DateTime, Exception?> _startValidationOption =
       LoggerMessage.Define<string, string, DateTime>(LogLevel.Information, 0,
           "LoggingType: {LoggingType} - ValidationOptionType: {ValidationOptionType} start, Time: {@DateTimeUtc}");

    private static readonly Action<ILogger, string, string, DateTime, Exception?> _completedValidationOption =
       LoggerMessage.Define<string, string, DateTime>(LogLevel.Information, 0,
           "LoggingType: {LoggingType} - ValidationOptionType: {ValidationOptionType} completed, Time: {@DateTimeUtc}");

    private static readonly Action<ILogger, string, string, string, DateTime, Exception?> _validateOptionWithError =
        LoggerMessage.Define<string, string, string, DateTime>(LogLevel.Error, 0,
            "LoggingType: {LoggingType} - ValidationOptionType: {ValidationOptionType} failed with error: {ErrorValidationOptionTypeMessage}, Time: {@DateTimeUtc}");

    public static void StartValidationOption(this ILogger logger, string validationOptionType)
    {
        _startValidationOption(logger, LoggingTypes.ValidationOption, validationOptionType, DateTime.UtcNow, null);
    }

    public static void CompletedValidationOption(this ILogger logger, string validationOptionType)
    {
        _completedValidationOption(logger, LoggingTypes.ValidationOption, validationOptionType, DateTime.UtcNow, null);
    }

    public static void ValidateOptionWithError(this ILogger logger, string validationOptionType, string errorMessage)
    {
        _validateOptionWithError(logger, LoggingTypes.ValidationOption, validationOptionType, errorMessage, DateTime.UtcNow, null);
    }
}