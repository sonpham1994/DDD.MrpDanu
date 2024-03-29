﻿using Microsoft.Extensions.DependencyInjection;
using Application.Interfaces;
using Infrastructure.EventDispatchers;
using Microsoft.Data.SqlClient;
using System.Data;
using Application.Interfaces.Services;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Externals;
using Infrastructure.Persistence.Interceptors;
using Microsoft.Extensions.Options;
using Infrastructure.Persistence.Externals.AuditTables.Services;
using Infrastructure.Persistence.Reads.MaterialQuery;
using Infrastructure.Persistence.Reads.TransactionalPartnerQuery;
using Infrastructure.Persistence.Writes.MaterialWrite;
using Application.Interfaces.Write;
using Application.Interfaces.Reads;
using Application.Interfaces.Writes.MaterialWrite;
using Infrastructure.Persistence.Writes;
using Application.Interfaces.Writes.TransactionalPartnerWrite;
using Infrastructure.Persistence.Writes.ProductWrite;
using Infrastructure.Persistence.Writes.TransactionalPartnerWrite;
using Microsoft.Extensions.Logging;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, bool isProduction)
    {
        services
            //.AddDatabaseSettings(configuration)
            .AddDbInterceptors()
            .AddEventDispatcher()
            .AddDbContext(isProduction)
            .AddWritesDI()
            .AddReadsDI()
            .AddTransactions();

        return services;
    }

    private static IServiceCollection AddDbContext(this IServiceCollection services, bool isProduction)
    {
        services.AddScoped(sp =>
        {
            var databaseSettings = sp.GetRequiredService<IOptions<DatabaseSettings>>();
            var loggingDbCommandInterceptor = sp.GetRequiredService<LoggingDbCommandInterceptor>();
            var enumerationSaveChangesInterceptor = sp.GetRequiredService<EnumerationSaveChangesInterceptor>();

            return new ExternalDbContext(databaseSettings,
                isProduction,
                loggingDbCommandInterceptor,
                enumerationSaveChangesInterceptor);
        });

        services.AddScoped(sp =>
        {
            var databaseSettings = sp.GetRequiredService<IOptions<DatabaseSettings>>();
            var eventDispatcher = sp.GetRequiredService<EventDispatcher>();
            var loggingDbCommandInterceptor = sp.GetRequiredService<LoggingDbCommandInterceptor>();
            var insertAuditableEntitiesInterceptor = sp.GetRequiredService<InsertAuditableEntitiesSaveChangesInterceptor>();
            var enumerationSaveChangesInterceptor = sp.GetRequiredService<EnumerationSaveChangesInterceptor>();

            return new AppDbContext(databaseSettings,
                isProduction,
                eventDispatcher,
                loggingDbCommandInterceptor,
                enumerationSaveChangesInterceptor, insertAuditableEntitiesInterceptor);
        });

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<AppDbContext>());

        return services;
    }

    private static IServiceCollection AddWritesDI(this IServiceCollection services)
    {
        services.AddScoped<IMaterialRepository, MaterialEfRepository>();
        services.AddScoped<IMaterialQueryForWrite, MaterialEFQueryForWrite>();

        services.AddScoped<ITransactionalPartnerRepository, TransactionalPartnerEfRepository>();
        services.AddScoped<ITransactionalPartnerQueryForWrite, TransactionalPartnerEFQueryForWrite>();

        services.AddScoped<IProductRepository, ProductEfRepository>();

        return services;
    }

    private static IServiceCollection AddEventDispatcher(this IServiceCollection services)
    {
        services.AddScoped<EventDispatcher>();

        return services;
    }

    private static IServiceCollection AddReadsDI(this IServiceCollection services)
    {
        //logging for dapper
        //https://stackoverflow.com/questions/18529965/is-there-any-way-to-trace-log-the-sql-using-dapper
        //https://www.erikthecoder.net/2019/07/06/automatically-log-all-sql-commands/
        services.AddScoped<IDbConnection>(sp =>
        {
            var applicationOptions = sp.GetRequiredService<IOptions<DatabaseSettings>>();
            return new SqlConnection(applicationOptions.Value.ConnectionString);
        });

        services.AddScoped<IMaterialQuery, MaterialDapperQuery>();
        services.AddScoped<ITransactionalPartnerQuery, TransactionalPartnerDapperQuery>();

        return services;
    }

    private static IServiceCollection AddTransactions(this IServiceCollection services)
    {
        services.AddScoped<ITransaction, DatabaseTransaction>();
        services.AddScoped<IAuditTableService, AuditTableService>();

        return services;
    }

    //private static IServiceCollection AddDatabaseSettings(this IServiceCollection services, IConfiguration configuration)
    //{
    //    //https://www.youtube.com/watch?v=qRruEdjNVNE
    //    services.AddOptions<DatabaseSettings>()
    //        .BindConfiguration(nameof(DatabaseSettings))
    //        .Validate(settings =>
    //        {
    //            if (settings.ConnectionString == string.Empty)
    //                return false;
    //            if (settings.MaxRetryCount == 0)
    //                return false;
    //            if (settings.MaxRetryDelay == 0)
    //                return false;
    //            if (settings.StandardExecutedDbCommandTime == 0)
    //                return false;

    //            var connections = settings.ConnectionString.Split(';');
    //            foreach (var connection in connections)
    //            {
    //                if (connection.StartsWith("Server", StringComparison.OrdinalIgnoreCase))
    //                {
    //                    var serverConfig = connection.Split('=');
    //                    var server = serverConfig[1];
    //                    var serverInfos = server.Split(',');
    //                    var serverName = serverInfos[0];
    //                    var port = serverInfos[1];
    //                    if (string.IsNullOrEmpty(serverName) || string.IsNullOrWhiteSpace(serverName))
    //                        return false;
    //                }
    //                else if (connection.StartsWith("Database", StringComparison.OrdinalIgnoreCase) || connection.StartsWith("Catalog", StringComparison.OrdinalIgnoreCase))
    //                {
    //                    var database = connection.Split('=');
    //                    var databaseName = database[1];
    //                    if (string.IsNullOrEmpty(databaseName) || string.IsNullOrWhiteSpace(databaseName))
    //                        return false;
    //                }
    //                else if (connection.StartsWith("User Id", StringComparison.OrdinalIgnoreCase))
    //                {
    //                    var userId = connection.Split('=');
    //                    var userIdName = userId[1];
    //                    if (string.IsNullOrEmpty(userIdName) || string.IsNullOrWhiteSpace(userIdName))
    //                        return false;
    //                }
    //                else if (connection.StartsWith("Password", StringComparison.OrdinalIgnoreCase))
    //                {
    //                    var password = connection.Split('=');
    //                    var passwordInfo = password[1];
    //                    if (string.IsNullOrEmpty(passwordInfo) || string.IsNullOrWhiteSpace(passwordInfo))
    //                        return false;
    //                }
    //            }

    //            return true;
    //        });

    //    services.Configure<DatabaseSettings>(configuration.GetSection(nameof(DatabaseSettings)), null);

    //    return services;
    //}

    private static IServiceCollection AddDbInterceptors(this IServiceCollection services)
    {
        services.AddScoped<LoggingDbCommandInterceptor>();
        services.AddScoped<InsertAuditableEntitiesSaveChangesInterceptor>();
        services.AddScoped<EnumerationSaveChangesInterceptor>();

        return services;
    }
}
