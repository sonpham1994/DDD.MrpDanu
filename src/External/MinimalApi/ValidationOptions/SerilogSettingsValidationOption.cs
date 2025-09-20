using MinimalApi.LoggingDefinitions;
using MinimalApi.Settings;
using Microsoft.Extensions.Options;
using CommonConstants = MinimalApi.Constants;

namespace MinimalApi.ValidationOptions;

public sealed class SerilogSettingsValidationOption(IWebHostEnvironment environment, ILogger<SerilogSettingsValidationOption> logger) : IValidateOptions<SerilogSettings>
{
    public ValidateOptionsResult Validate(string? name, SerilogSettings options)
    {
        if (environment.IsDevelopment())
        {
            string validationOptionType = CommonConstants.SerilogConstants.ConfigurationNameSettings;
            logger.StartValidationOption(validationOptionType);
            
            if (options.WriteTo.Count == 0)
            {
                //we don't need to create char[] as representation of string because when code hit this, it means your app
                // will fail to start app, and it will shut down your app. After you reconfigure correctly, it will restart
                // if you change configuration, this will re-run
                logger.ValidateOptionWithError(validationOptionType, "WriteTo is empty");
                return ValidateOptionsResult.Fail("WriteTo is empty");
            }

            if (options.WriteTo[0].Name.Equals(CommonConstants.SerilogConstants.ApplicationInsightsName, StringComparison.OrdinalIgnoreCase))
            {
                if (options.WriteTo[0]?.Args is null)
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