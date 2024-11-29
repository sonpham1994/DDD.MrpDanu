using MinimalApi.LoggingDefinitions;
using Infrastructure.Persistence;
using Microsoft.Extensions.Options;
using CommonConstants = MinimalApi.Constants;

namespace MinimalApi.ValidationOptions;

//https://www.youtube.com/watch?v=jblRYDMTtvg
//https://code-maze.com/aspnet-configuration-options-validation/
//ValidatateOptions and how IServiceProvider with DI lifetime work: https://andrewlock.net/the-dangers-and-gotchas-of-using-scoped-services-when-configuring-options-with-options-builder/
//Add all ValidateOptions: https://andrewlock.net/adding-validation-to-strongly-typed-configuration-objects-using-flentvalidation/
//ValidateOption with .Net 8 using source generator: https://www.roundthecode.com/dotnet-tutorials/validating-appsettings-faster-dotnet-8
public sealed class DatabaseSettingsValidationOption(ILogger<DatabaseSettingsValidationOption> logger) : IValidateOptions<DatabaseSettings>
{
    private readonly ILogger _logger = logger;
    public ValidateOptionsResult Validate(string? name, DatabaseSettings settings)
    {
        var validationOptionType = new string(CommonConstants.Database.ConfigurationName);
        _logger.StartValidationOption(validationOptionType);
        
        if (settings.ConnectionString == string.Empty)
        {
            //we don't need to create char[] as representation of string because when code hit this, it means your app
            // will fail to start app, and it will shut down your app. After you reconfigure correctly, it will restart
            _logger.ValidateOptionWithError(validationOptionType, "ConnectionString is empty");
            return ValidateOptionsResult.Fail("ConnectionString is empty");
        }

        if (settings.MaxRetryCount == 0)
        {
            _logger.ValidateOptionWithError(validationOptionType, "MaxRetryCount equals to 0");
            return ValidateOptionsResult.Fail("MaxRetryCount equals to 0");
        }

        if (settings.MaxRetryDelay == 0)
        {
            _logger.ValidateOptionWithError(validationOptionType, "MaxRetryDelay equals to 0");
            return ValidateOptionsResult.Fail("MaxRetryDelay equals to 0");
        }

        if (settings.StandardExecutedDbCommandTime == 0)
        {
            _logger.ValidateOptionWithError(validationOptionType, "StandardExecutedDbCommandTime equals to 0");
            return ValidateOptionsResult.Fail("StandardExecutedDbCommandTime equals to 0");
        }

        var connections = settings.ConnectionString.Split(';');
        foreach (var connection in connections)
        {
            if (connection.StartsWith(Constants.ServerDatabaseKey, StringComparison.OrdinalIgnoreCase))
            {
                var serverConfig = connection.Split('=');
                var server = serverConfig[1];
                var serverInfos = server.Split(',');
                var serverName = serverInfos[0];
                var port = serverInfos[1];
                if (string.IsNullOrEmpty(serverName) || string.IsNullOrWhiteSpace(serverName))
                {
                    _logger.ValidateOptionWithError(validationOptionType, "Server is empty");
                    return ValidateOptionsResult.Fail("Server is empty");
                }

            }
            else if (connection.StartsWith(CommonConstants.Database.DatabaseString, StringComparison.OrdinalIgnoreCase)
                || connection.StartsWith(Constants.CatalogDatabaseKey, StringComparison.OrdinalIgnoreCase)
                || connection.StartsWith(Constants.InitialCatalogDatabaseKey, StringComparison.OrdinalIgnoreCase))
            {
                var databaseInfo = connection.Split('=');
                var databaseName = databaseInfo[1];
                if (string.IsNullOrEmpty(databaseName) || string.IsNullOrWhiteSpace(databaseName))
                {
                    _logger.ValidateOptionWithError(validationOptionType, "Database name is empty");
                    return ValidateOptionsResult.Fail("Database name is empty");
                }

            }
            else if (connection.StartsWith(Constants.UserIdDatabaseKey, StringComparison.OrdinalIgnoreCase))
            {
                var userId = connection.Split('=');
                var userIdName = userId[1];
                if (string.IsNullOrEmpty(userIdName) || string.IsNullOrWhiteSpace(userIdName))
                {
                    _logger.ValidateOptionWithError(validationOptionType, "User Id is empty");
                    return ValidateOptionsResult.Fail("User Id is empty");
                }

            }
            else if (connection.StartsWith(Constants.PasswordDatabaseKey, StringComparison.OrdinalIgnoreCase))
            {
                var password = connection.Split('=');
                var passwordInfo = password[1];
                if (string.IsNullOrEmpty(passwordInfo) || string.IsNullOrWhiteSpace(passwordInfo))
                {
                    _logger.ValidateOptionWithError(validationOptionType, "Password is empty");
                    return ValidateOptionsResult.Fail("Password is empty");
                }
            }
        }

        _logger.CompletedValidationOption(validationOptionType);
        return ValidateOptionsResult.Success;
    }
}
