using Application.LoggingDefinitions;

namespace MinimalApi.LoggingDefinitions;

public static class ValidationOptionLoggingDefinition
{
    private const string MessageLoggingDefinition = SharedLoggingDefinition.Prefix + " - ValidationOptionType: {ValidationOptionType}";
    
    //These format cannot be cleaned up event if using char[]. Hence we dont' need to create char array for this case.
    // But we can use Console.Error.WriteLine, it can be cleaned up with GC.Collect(2)
    // and but because it will run at once, and if this log runs, the system will fail and shutdown, so we don't need
    // to take care of this
    //Please check TestGcForString project
    private static readonly Action<ILogger, string, string, DateTime, Exception?> _startValidationOption =
       LoggerMessage.Define<string, string, DateTime>(LogLevel.Information, 0,
           MessageLoggingDefinition + SharedLoggingDefinition.Start + SharedLoggingDefinition.Suffix);

    private static readonly Action<ILogger, string, string, DateTime, Exception?> _completedValidationOption =
       LoggerMessage.Define<string, string, DateTime>(LogLevel.Information, 0,
           MessageLoggingDefinition + SharedLoggingDefinition.Completed + SharedLoggingDefinition.Suffix);

    private static readonly Action<ILogger, string, string, string, DateTime, Exception?> _validateOptionWithError =
        LoggerMessage.Define<string, string, string, DateTime>(LogLevel.Error, 0,
            MessageLoggingDefinition + SharedLoggingDefinition.FailedWithError + SharedLoggingDefinition.Suffix);

    public static void StartValidationOption(this ILogger logger, string validationOptionType)
    {
        _startValidationOption(logger, LoggingTypes.ValidationOption, validationOptionType, DateTime.UtcNow, null);
    }

    public static void CompletedValidationOption(this ILogger logger, string validationOptionType)
    {
        //logger.LogWarning("GC LoggingType: " + GC.GetGeneration(LoggingTypes.ValidationOption));
        _completedValidationOption(logger, LoggingTypes.ValidationOption, validationOptionType, DateTime.UtcNow, null);
    }

    public static void ValidateOptionWithError(this ILogger logger, string validationOptionType, string errorMessage)
    {
        //logger.LogWarning("GC LoggingType: " + GC.GetGeneration(LoggingTypes.ValidationOption));
        _validateOptionWithError(logger, LoggingTypes.ValidationOption, validationOptionType, errorMessage, DateTime.UtcNow, null);
    }
}