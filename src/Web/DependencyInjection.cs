using Application;
using Web.Middlewares;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Infrastructure.Persistence;

namespace Web;

public static class DependencyInjection
{
    public static IServiceCollection AddWeb(this IServiceCollection services)
    {
        services.AddCustomMiddlewares()
            .DisableDefaultModelValidation()
            .AddValidators()
            .AddDatabaseSettings();

        return services;
    }

    private static IServiceCollection AddCustomMiddlewares(this IServiceCollection services)
    {
        services.AddScoped<GlobalExceptionHandlingMiddleware>();

        return services;
    }
    
    private static IServiceCollection DisableDefaultModelValidation(this IServiceCollection services)
    {
        ServiceDescriptor? serviceDescriptor = services.FirstOrDefault(s => s.ServiceType == typeof(IObjectModelValidator));
        if (serviceDescriptor != null)
        {
            services.Remove(serviceDescriptor);
            services.Add(new ServiceDescriptor(typeof(IObjectModelValidator), _ => new NullObjectModelValidator(), ServiceLifetime.Singleton));
        }
        
        return services;
    }

    private static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(ApplicationAssembly.Instance, ServiceLifetime.Scoped, null, true);

        ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Stop;

        return services;
    }

    private static IServiceCollection AddDatabaseSettings(this IServiceCollection services)
    {
        //https://www.youtube.com/watch?v=qRruEdjNVNE
        services.AddOptions<DatabaseSettings>()
            .BindConfiguration(nameof(DatabaseSettings))
            .Validate(settings =>
            {
                if (settings.ConnectionString == string.Empty)
                    return false;
                if (settings.MaxRetryCount == 0)
                    return false;
                if (settings.MaxRetryDelay == 0)
                    return false;
                if (settings.StandardExecutedDbCommandTime == 0)
                    return false;

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
                            return false;
                    }
                    else if (connection.StartsWith("Database", StringComparison.OrdinalIgnoreCase) || connection.StartsWith("Catalog", StringComparison.OrdinalIgnoreCase))
                    {
                        var database = connection.Split('=');
                        var databaseName = database[1];
                        if (string.IsNullOrEmpty(databaseName) || string.IsNullOrWhiteSpace(databaseName))
                            return false;
                    }
                    else if (connection.StartsWith("User Id", StringComparison.OrdinalIgnoreCase))
                    {
                        var userId = connection.Split('=');
                        var userIdName = userId[1];
                        if (string.IsNullOrEmpty(userIdName) || string.IsNullOrWhiteSpace(userIdName))
                            return false;
                    }
                    else if (connection.StartsWith("Password", StringComparison.OrdinalIgnoreCase))
                    {
                        var password = connection.Split('=');
                        var passwordInfo = password[1];
                        if (string.IsNullOrEmpty(passwordInfo) || string.IsNullOrWhiteSpace(passwordInfo))
                            return false;
                    }
                }

                return true;
            })
            .ValidateOnStart();

        //services.Configure<DatabaseSettings>(configuration.GetSection(nameof(DatabaseSettings)), null);

        return services;
    }
}