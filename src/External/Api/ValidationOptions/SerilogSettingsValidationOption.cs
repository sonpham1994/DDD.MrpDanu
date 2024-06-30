using Api.LoggingDefinitions;
using Api.Settings;
using Microsoft.Extensions.Options;

namespace Api.ValidationOptions;

public sealed class SerilogSettingsValidationOption(IWebHostEnvironment environment, ILogger<SerilogSettingsValidationOption> logger) : IValidateOptions<SerilogSettings>
{

    public ValidateOptionsResult Validate(string? name, SerilogSettings options)
    {
        if (environment.IsProduction())
        {
            string validationOptionType = nameof(SerilogSettings);

            logger.StartValidationOption(validationOptionType);
            if (options.WriteTo.Count == 0)
            {
                logger.ValidateOptionWithError(validationOptionType, "WriteTo is empty");
                return ValidateOptionsResult.Fail("WriteTo is empty");
            }

            if (options.WriteTo[0].Name.Equals("ApplicationInsights", StringComparison.OrdinalIgnoreCase))
            {
                if (options.WriteTo[0].Args is null)
                {
                    logger.ValidateOptionWithError(validationOptionType, "Args is null");
                    return ValidateOptionsResult.Fail("Args is empty");
                }

                if (string.IsNullOrEmpty(options.WriteTo[0].Args.ConnectionString))
                {
                    logger.ValidateOptionWithError(validationOptionType, "ConnectionString is empty");
                    return ValidateOptionsResult.Fail("ConnectionString is empty");
                }
            }
            logger.CompletedValidationOption(validationOptionType);
            return ValidateOptionsResult.Success;
        }

        return ValidateOptionsResult.Success;
    }
}