using Microsoft.Extensions.DependencyInjection;
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
using Infrastructure.Persistence.SupplyAndProductionManagement.SupplyChainManagement.Materials.Writes;
using Application.Interfaces.Write;
using Application.Interfaces.Reads;
using Application.Interfaces.Writes.AuditTrailWrite;
using Application.Interfaces.Writes.MaterialWrite;
using Infrastructure.Persistence.Writes;
using Application.Interfaces.Writes.TransactionalPartnerWrite;
using Domain.AuditTrail.SharedKernel;
using Domain.AuditTrail.SupplyChainManagement;
using SharedKernel;
using Infrastructure.Persistence.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartners.Reads;
using Infrastructure.Persistence.SupplyAndProductionManagement.SupplyChainManagement.Materials.Reads;
using Infrastructure.Persistence.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartners.Writes;
using Infrastructure.Persistence.SupplyAndProductionManagement.ProductPlanning.Writes;
using Infrastructure.Persistence.AuditTrail;
using Infrastructure.PersistenceMongoDb;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

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
            .AddWritesDi()
            .AddReadsDi()
            .AddTransactions()
            .AddAuditTrail();

        return services;
    }
    
    private static IServiceCollection AddDbContext(this IServiceCollection services, bool isProduction)
    {
        //services.AddScoped(sp =>
        //{
        //    var databaseSettings = sp.GetRequiredService<IOptions<DatabaseSettings>>();
        //    var loggingDbCommandInterceptor = sp.GetRequiredService<LoggingDbCommandInterceptor>();
        //    var enumerationSaveChangesInterceptor = sp.GetRequiredService<EnumerationSaveChangesInterceptor>();

        //    return new ExternalDbContext(databaseSettings,
        //        isProduction,
        //        loggingDbCommandInterceptor,
        //        enumerationSaveChangesInterceptor);
        //});
        //services.AddScoped(sp =>
        //{
        //    var databaseSettings = sp.GetRequiredService<IOptions<DatabaseSettings>>();
        //    var loggingDbCommandInterceptor = sp.GetRequiredService<LoggingDbCommandInterceptor>();
        //    var insertAuditableEntitiesInterceptor = sp.GetRequiredService<InsertAuditableEntitiesSaveChangesInterceptor>();
        //    var enumerationSaveChangesInterceptor = sp.GetRequiredService<EnumerationSaveChangesInterceptor>();

        //    return new AppDbContext(databaseSettings,
        //        isProduction,
        //        loggingDbCommandInterceptor,
        //        enumerationSaveChangesInterceptor, insertAuditableEntitiesInterceptor);
        //});

        services.AddDbContext<ExternalDbContext>();
        services.AddDbContext<AppDbContext>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<AppDbContext>());
        services.AddScoped<IAppDbConnection>(sp => sp.GetRequiredService<AppDbContext>());

        return services;
    }

    private static IServiceCollection AddWritesDi(this IServiceCollection services)
    {
        services.AddScoped<IMaterialRepository, MaterialEfRepository>();
        services.AddScoped<IMaterialReadonlyRepository, MaterialEfReadonlyRepository>();

        services.AddScoped<ITransactionalPartnerRepository, TransactionalPartnerEfRepository>();
        services.AddScoped<ITransactionalPartnerReadonlyRepository, TransactionalPartnerEfReadonlyRepository>();

        services.AddScoped<IProductRepository, ProductEfRepository>();

        return services;
    }

    private static IServiceCollection AddEventDispatcher(this IServiceCollection services)
    {
        services.AddScoped<EventDispatcher>();

        return services;
    }

    private static IServiceCollection AddReadsDi(this IServiceCollection services)
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
        services.TryAddSingleton<PerformanceDetectionCommandInterceptor>();
        services.TryAddSingleton<EnumerationSaveChangesInterceptor>();

        //due to inject ExternalDbContext, we cannot use Singleton
        services.TryAddScoped<InsertAuditableEntitiesSaveChangesInterceptor>();

        return services;
    }

    private static IServiceCollection AddAuditTrail(this IServiceCollection services)
    {
        // Add services to the container.
        BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
        BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));
        services.AddTransient<AuditTrailDomainEvents>();
        services.AddScoped<IDomainEventDispatcher, MediatRDomainEventDispatcher>();

        services.AddSingleton(sp =>
        {
            var mongoDbSettings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
            // var settings = new MongoClientSettings
            // {
            //     Servers = new[]
            //     {
            //         new MongoServerAddress("mongo-node1", 27017),
            //         new MongoServerAddress("mongo-node2", 27017),
            //         new MongoServerAddress("mongo-node3", 27017)
            //     },
            //     DirectConnection = 
            //     ReplicaSetName = "rs0"
            // };
            //var client = new MongoClient(settings);
            var mongoClient = new MongoClient(mongoDbSettings.ConnectionString);
            return mongoClient.GetDatabase("dddMrpDanu");
        });

        services.AddMongoRepository<MaterialAudit>("materialAudit");
        
        return services;
    }
    
    public static IServiceCollection AddMongoRepository<T>(this IServiceCollection services, string collectionName)
        where T : IAuditEntity
    {
        services.AddSingleton<IAuditTableWriteRepository<AuditEntity<T>>>(serviceProvider =>
        {
            var database = serviceProvider.GetRequiredService<IMongoDatabase>();
            return new AuditTableWriteMongoRepository<AuditEntity<T>>(database, collectionName);
        });

        return services;
    }
    
    
}
