using MinimalApi.LoggingDefinitions;
using Infrastructure.Persistence;
using Microsoft.Extensions.Options;
using MinimalApi.Constants;

namespace MinimalApi.ValidationOptions;

//https://www.youtube.com/watch?v=jblRYDMTtvg
//https://code-maze.com/aspnet-configuration-options-validation/
//ValidatateOptions and how IServiceProvider with DI lifetime work: https://andrewlock.net/the-dangers-and-gotchas-of-using-scoped-services-when-configuring-options-with-options-builder/
//Add all ValidateOptions: https://andrewlock.net/adding-validation-to-strongly-typed-configuration-objects-using-flentvalidation/
//ValidateOption with .Net 8 using source generator: https://www.roundthecode.com/dotnet-tutorials/validating-appsettings-faster-dotnet-8
public sealed class SqlDatabaseSettingsValidationOption(ILogger<SqlDatabaseSettingsValidationOption> logger) : IValidateOptions<SqlDatabaseSettings>
{
    private readonly ILogger _logger = logger;
    public ValidateOptionsResult Validate(string? name, SqlDatabaseSettings settings)
    {
        var validationOptionType = SqlDatabaseConstants.ConfigurationName;
        _logger.StartValidationOption(validationOptionType);
        
        if (settings.ConnectionString == string.Empty)
        {
            //we don't need to create char[] as representation of string because when code hit this, it means your app
            // will fail to start app, and it will shut down your app. After you reconfigure correctly, it will restart
            // if you change configuration, this will re-run
            var message = SqlDatabaseConstants.ConnectionStringEmpty;
            _logger.ValidateOptionWithError(validationOptionType, message);
            return ValidateOptionsResult.Fail(message);
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
            if (connection.StartsWith(SqlDatabaseConstants.ServerDatabaseKey, StringComparison.OrdinalIgnoreCase))
            {
                var serverConfig = connection.Split('=');
                var server = serverConfig[1];
                var serverInfos = server.Split(',');
                var serverName = serverInfos[0];
                var port = serverInfos[1];
                if (string.IsNullOrEmpty(serverName) || string.IsNullOrWhiteSpace(serverName))
                {
                    var message = SqlDatabaseConstants.ServerNameEmpty;
                    _logger.ValidateOptionWithError(validationOptionType, message);
                    return ValidateOptionsResult.Fail(message);
                }

            }
            else if (connection.StartsWith(SqlDatabaseConstants.DatabaseStringKey, StringComparison.OrdinalIgnoreCase)
                || connection.StartsWith(SqlDatabaseConstants.CatalogDatabaseKey, StringComparison.OrdinalIgnoreCase)
                || connection.StartsWith(SqlDatabaseConstants.InitialCatalogDatabaseKey, StringComparison.OrdinalIgnoreCase))
            {
                var databaseInfo = connection.Split('=');
                var databaseName = databaseInfo[1];
                if (string.IsNullOrEmpty(databaseName) || string.IsNullOrWhiteSpace(databaseName))
                {
                    var message = SqlDatabaseConstants.DatabaseNameEmpty;
                    _logger.ValidateOptionWithError(validationOptionType, message);
                    return ValidateOptionsResult.Fail(message);
                }

            }
            else if (connection.StartsWith(SqlDatabaseConstants.UserIdKey, StringComparison.OrdinalIgnoreCase))
            {
                var userId = connection.Split('=');
                var userIdName = userId[1];
                if (string.IsNullOrEmpty(userIdName) || string.IsNullOrWhiteSpace(userIdName))
                {
                    var message = SqlDatabaseConstants.UserIdEmpty;
                    _logger.ValidateOptionWithError(validationOptionType, message);
                    return ValidateOptionsResult.Fail(message);
                }

            }
            else if (connection.StartsWith(SqlDatabaseConstants.PasswordKey, StringComparison.OrdinalIgnoreCase))
            {
                var password = connection.Split('=');
                var passwordInfo = password[1];
                if (string.IsNullOrEmpty(passwordInfo) || string.IsNullOrWhiteSpace(passwordInfo))
                {
                    var message = SqlDatabaseConstants.PasswordEmpty;
                    _logger.ValidateOptionWithError(validationOptionType, message);
                    return ValidateOptionsResult.Fail(message);
                }
            }
        }

        _logger.CompletedValidationOption(validationOptionType);
        return ValidateOptionsResult.Success;
    }
}
