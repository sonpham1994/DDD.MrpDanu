using MinimalApi.LoggingDefinitions;
using Infrastructure.PersistenceMongoDb;
using Microsoft.Extensions.Options;

namespace MinimalApi.ValidationOptions;

public sealed class MongoDbSettingsValidationOption(ILogger<DatabaseSettingsValidationOption> logger)
    : IValidateOptions<MongoDbSettings>
{
    public ValidateOptionsResult Validate(string? name, MongoDbSettings settings)
    {
        var validationOptionType = MinimalApi.Constants.MongoDb.ConfigurationName;
        logger.StartValidationOption(validationOptionType);
        
        if (settings.ConnectionString == string.Empty)
        {
            //we don't need to create char[] as representation of string because when code hit this, it means your app
            // will fail to start app, and it will shut down your app. After you reconfigure correctly, it will restart
            // if you change configuration, this will re-run
            logger.ValidateOptionWithError(validationOptionType, "ConnectionString is empty");
            return ValidateOptionsResult.Fail("ConnectionString is empty");
        }
        
        logger.CompletedValidationOption(validationOptionType);
        return ValidateOptionsResult.Success;
    }
}