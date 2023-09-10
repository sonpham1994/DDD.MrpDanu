using Microsoft.Extensions.DependencyInjection;
using Application.Interfaces;
using Infrastructure.EventDispatchers;
using Microsoft.Data.SqlClient;
using System.Data;
using Application.Interfaces.Queries;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Externals;
using Infrastructure.Persistence.Read.Queries;
using Infrastructure.Persistence.Write;
using Infrastructure.Persistence.Write.EfRepositories;
using Infrastructure.Persistence.Interceptors;
using Microsoft.Extensions.Options;
using Infrastructure.Persistence.Externals.AuditTables.Services;
using Infrastructure.Persistence.Write.Mementos;
using Infrastructure.Persistence.Write.Mementos.Originators;

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
            .AddRepositories()
            .AddQueries()
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

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<MaterialEfRepository>();
        //IMaterialRepository and IUndoRepository use MaterialWithUndoRepository, so If we do the normal way,
        // we create 2 instances of MaterialWithUndoRepository. To achieve just one MaterialWithUndoRepository,
        // we add MaterialWithUndoRepository as scoped first and then IMaterialRepository and IUndoRepository
        // will reuse MaterialWithUndoRepository from service provider (service container)
        services.AddScoped<MaterialWithUndoRepository>(sp =>
            {
                var materialRepository = sp.GetRequiredService<MaterialEfRepository>();
                var dbConnection = sp.GetRequiredService<IDbConnection>();
                
                return new(materialRepository, dbConnection);
            });
        services.AddScoped<IMaterialRepository>(sp => sp.GetRequiredService<MaterialWithUndoRepository>());
        services.AddScoped<IUndoRepository>(sp => sp.GetRequiredService<MaterialWithUndoRepository>());
        
        services.AddScoped<ITransactionalPartnerRepository, TransactionalPartnerEfRepository>();
        services.AddScoped<IProductRepository, ProductEfRepository>();
        
        return services;
    }

    private static IServiceCollection AddEventDispatcher(this IServiceCollection services)
    {
        services.AddScoped<EventDispatcher>();

        return services;
    }

    private static IServiceCollection AddQueries(this IServiceCollection services)
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

    private static IServiceCollection AddDbInterceptors(this IServiceCollection services)
    {
        services.AddScoped<LoggingDbCommandInterceptor>();
        services.AddScoped<InsertAuditableEntitiesSaveChangesInterceptor>();
        services.AddScoped<EnumerationSaveChangesInterceptor>();

        return services;
    }
}
