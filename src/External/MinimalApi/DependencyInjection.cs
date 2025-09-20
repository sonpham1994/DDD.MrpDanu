using MinimalApi.EventListeners;
using Application;
using MinimalApi.Middlewares;
using FluentValidation;
using Infrastructure.Persistence;
using MinimalApi.HostedServices;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using MinimalApi.ValidationOptions;
using MinimalApi.Settings;
using Infrastructure.PersistenceMongoDb;
using Microsoft.FeatureManagement;
using MinimalApi.Constants;

namespace MinimalApi;

public static class DependencyInjection
{
    public static IServiceCollection AddMinimalApi(this IServiceCollection services, bool isProduction, ConfigurationManager configuration)
    {
        services.AddCustomMiddlewares()
            .AddValidators()
            .AddOptions(configuration)
            .AddHostedService<GcEventListenerBackgroundService>()
            .AddFeatureManagement(configuration.GetSection(FeatureFlags.ConfigurationName));

        //if (!isProduction)
        //{
            //services.AddHostedService<MigrationsHostedService>();
        //}

        services.AddSingleton<GcEventListener>();
        return services;
    }

    private static IServiceCollection AddCustomMiddlewares(this IServiceCollection services)
    {
        services.AddScoped<LoggingMiddleware>();
        services.AddScoped<GlobalExceptionHandlingMiddleware>();

        return services;
    }
    
    private static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(ApplicationAssembly.Instance, ServiceLifetime.Scoped, null, true);

        ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Stop;

        return services;
    }

    private static IServiceCollection AddOptions(this IServiceCollection services, ConfigurationManager configuration)
    {
        // I prefer using implementing class with IValidateOption<> in order to use DI, e.g, ILogger
        //https://www.youtube.com/watch?v=qRruEdjNVNE
        //services.AddOptions<DatabaseSettings>()
        //    .BindConfiguration(nameof(DatabaseSettings))
        //    .Validate(settings =>
        //    {
        //        if (settings.ConnectionString == string.Empty)
        //            return false;
        //        if (settings.MaxRetryCount == 0)
        //            return false;
        //        if (settings.MaxRetryDelay == 0)
        //            return false;
        //        if (settings.StandardExecutedDbCommandTime == 0)
        //            return false;

        //        var connections = settings.ConnectionString.Split(';');
        //        foreach (var connection in connections)
        //        {
        //            if (connection.StartsWith("Server", StringComparison.OrdinalIgnoreCase))
        //            {
        //                var serverConfig = connection.Split('=');
        //                var server = serverConfig[1];
        //                var serverInfos = server.Split(',');
        //                var serverName = serverInfos[0];
        //                var port = serverInfos[1];
        //                if (string.IsNullOrEmpty(serverName) || string.IsNullOrWhiteSpace(serverName))
        //                {

        //                    return false;
        //                }

        //            }
        //            else if (connection.StartsWith("Database", StringComparison.OrdinalIgnoreCase) || connection.StartsWith("Catalog", StringComparison.OrdinalIgnoreCase))
        //            {
        //                var database = connection.Split('=');
        //                var databaseName = database[1];
        //                if (string.IsNullOrEmpty(databaseName) || string.IsNullOrWhiteSpace(databaseName))
        //                    return false;
        //            }
        //            else if (connection.StartsWith("User Id", StringComparison.OrdinalIgnoreCase))
        //            {
        //                var userId = connection.Split('=');
        //                var userIdName = userId[1];
        //                if (string.IsNullOrEmpty(userIdName) || string.IsNullOrWhiteSpace(userIdName))
        //                    return false;
        //            }
        //            else if (connection.StartsWith("Password", StringComparison.OrdinalIgnoreCase))
        //            {
        //                var password = connection.Split('=');
        //                var passwordInfo = password[1];
        //                if (string.IsNullOrEmpty(passwordInfo) || string.IsNullOrWhiteSpace(passwordInfo))
        //                    return false;
        //            }
        //        }

        //        return true;
        //    })
        //    .ValidateOnStart();

        // I prefer using this approach due to be able to use DI, e.g, ILogger. The problem with this approach is that
        // it registers DI as Singleton. Because we just need to validate it at once, hence registering as singleton
        // can allocate memory on Generation 2 that doesn't really need.
        // this will run whenever the settings is changed (for ex DatabaseSettings), the validator will run again,
        // for case if something change while running, the app will not shut down.
        // it will be suitable for connection changes frequently, but not ideal for connection never change.
        services.TryAddSingleton<IValidateOptions<SqlDatabaseSettings>, SqlDatabaseSettingsValidationOption>();
        services.TryAddSingleton<IValidateOptions<SerilogSettings>, SerilogSettingsValidationOption>();
        services.TryAddSingleton<IValidateOptions<MongoDbSettings>, MongoDbSettingsValidationOption>();
        

        // When first accessing these options, it will validate
        //services.Configure<DatabaseSettings>(configuration.GetSection(nameof(DatabaseSettings)));
        //services.Configure<SerilogSettings>(configuration.GetSection("Serilog"));

        //Validate when running the project on
        services.AddOptions<SqlDatabaseSettings>().BindConfiguration(SqlDatabaseConstants.ConfigurationName).ValidateOnStart();
        services.AddOptions<SerilogSettings>().BindConfiguration(SerilogConstants.ConfigurationName).ValidateOnStart();
        services.AddOptions<MongoDbSettings>().BindConfiguration(MongoDbConstants.ConfigurationName).ValidateOnStart();

        return services;
    }
}