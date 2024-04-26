using Api.LoggingDefinitions;
using Api.Settings;
using Microsoft.Extensions.Options;

namespace Api.ValidationOptions;

public sealed class SerilogSettingsValidationOption(IWebHostEnvironment environment, ILogger<SerilogSettingsValidationOption> logger) : IValidateOptions<SerilogSettings>
{
    private readonly IWebHostEnvironment _environment = environment;
    private readonly ILogger _logger = logger;

    public ValidateOptionsResult Validate(string? name, SerilogSettings options)
    {
        if (_environment.IsProduction())
        {
            string validationOptionType = nameof(SerilogSettings);

            _logger.StartValidationOption(validationOptionType);
            if (options.WriteTo.Count == 0)
            {
                _logger.ValidateOptionWithError(validationOptionType, "WriteTo is empty");
                return ValidateOptionsResult.Fail("WriteTo is empty");
            }

            if (options.WriteTo[0].Name.Equals("ApplicationInsights", StringComparison.OrdinalIgnoreCase))
            {
                if (options.WriteTo[0].Args is null)
                {
                    _logger.ValidateOptionWithError(validationOptionType, "Args is null");
                    return ValidateOptionsResult.Fail("Args is empty");
                }

                if (string.IsNullOrEmpty(options.WriteTo[0].Args.ConnectionString))
                {
                    _logger.ValidateOptionWithError(validationOptionType, "ConnectionString is empty");
                    return ValidateOptionsResult.Fail("ConnectionString is empty");
                }
            }
            _logger.CompletedValidationOption(validationOptionType);
            return ValidateOptionsResult.Success;
        }

        return ValidateOptionsResult.Success;
    }
}