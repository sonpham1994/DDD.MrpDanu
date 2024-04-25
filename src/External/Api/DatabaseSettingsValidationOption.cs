using Infrastructure.Persistence;
using Microsoft.Extensions.Options;

namespace Api;

//https://www.youtube.com/watch?v=jblRYDMTtvg
//https://code-maze.com/aspnet-configuration-options-validation/
//Add all ValidateOptions: https://andrewlock.net/adding-validation-to-strongly-typed-configuration-objects-using-flentvalidation/
public sealed class DatabaseSettingsValidationOption(ILogger<DatabaseSettingsValidationOption> logger) : IValidateOptions<DatabaseSettings>
{
    private readonly ILogger _logger = logger;
    public ValidateOptionsResult Validate(string? name, DatabaseSettings settings)
    {
        _logger.LogInformation("Start DatabaseSettingsValidationOption");
        if (settings.ConnectionString == string.Empty)
        {
            _logger.LogError("Validation Option with DatabaseSettings: ConnectionString is empty");
            return ValidateOptionsResult.Fail("ConnectionString is empty");
        }
            
        if (settings.MaxRetryCount == 0)
        {
            _logger.LogError("Validation Option with DatabaseSettings: MaxRetryCount equals to 0");
            return ValidateOptionsResult.Fail("MaxRetryCount equals to 0");
        }
           
        if (settings.MaxRetryDelay == 0)
        {
            _logger.LogError("Validation Option with DatabaseSettings: MaxRetryDelay equals to 0");
            return ValidateOptionsResult.Fail("MaxRetryDelay equals to 0");
        }
            
        if (settings.StandardExecutedDbCommandTime == 0)
        {
            _logger.LogError("Validation Option with DatabaseSettings: StandardExecutedDbCommandTime equals to 0");
            return ValidateOptionsResult.Fail("StandardExecutedDbCommandTime equals to 0");
        }
            

        var connections = settings.ConnectionString.Split(';');
        foreach (var connection in connections)
        {
            if (connection.StartsWith("Server", StringComparison.OrdinalIgnoreCase))
            {
                var serverConfig = connection.Split('=');
                var server = serverConfig[1];
                var serverInfos = server.Split(',');
                var serverName = serverInfos[0];
                var port = serverInfos[1];
                if (string.IsNullOrEmpty(serverName) || string.IsNullOrWhiteSpace(serverName))
                {
                    _logger.LogError("Validation Option with DatabaseSettings: Server is empty");
                    return ValidateOptionsResult.Fail("Server is empty");
                }

            }
            else if (connection.StartsWith("Database", StringComparison.OrdinalIgnoreCase) 
                || connection.StartsWith("Catalog", StringComparison.OrdinalIgnoreCase)
                || connection.StartsWith("Initial Catalog", StringComparison.OrdinalIgnoreCase))
            {
                var database = connection.Split('=');
                var databaseName = database[1];
                if (string.IsNullOrEmpty(databaseName) || string.IsNullOrWhiteSpace(databaseName))
                {
                    _logger.LogError("Validation Option with DatabaseSettings: Database name is empty");
                    return ValidateOptionsResult.Fail("Database name is empty");
                }
                    
            }
            else if (connection.StartsWith("User Id", StringComparison.OrdinalIgnoreCase))
            {
                var userId = connection.Split('=');
                var userIdName = userId[1];
                if (string.IsNullOrEmpty(userIdName) || string.IsNullOrWhiteSpace(userIdName))
                {
                    _logger.LogError("Validation Option with DatabaseSettings: User Id is empty");
                    return ValidateOptionsResult.Fail("User Id is empty");
                }
                    
            }
            else if (connection.StartsWith("Password", StringComparison.OrdinalIgnoreCase))
            {
                var password = connection.Split('=');
                var passwordInfo = password[1];
                if (string.IsNullOrEmpty(passwordInfo) || string.IsNullOrWhiteSpace(passwordInfo))
                {
                    _logger.LogError("Validation Option with DatabaseSettings: Password is empty");
                    return ValidateOptionsResult.Fail("Password is empty");
                }
            }
        }

        _logger.LogInformation("End DatabaseSettingsValidationOption");
        return ValidateOptionsResult.Success;
    }
}
