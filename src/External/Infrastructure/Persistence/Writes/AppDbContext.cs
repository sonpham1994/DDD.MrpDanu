using System.Data;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Application.Interfaces;
using Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;
using Domain.SupplyAndProductionManagement.ProductionPlanning;

namespace Infrastructure.Persistence.Writes;

internal sealed class AppDbContext(
    DbContextOptions<AppDbContext> options,
    IOptions<DatabaseSettings> databaseSettings,
    EnumerationSaveChangesInterceptor enumerationSaveChangesInterceptor,
    InsertAuditableEntitiesSaveChangesInterceptor insertAuditableEntitiesSaveChangesInterceptor,
    PerformanceDetectionCommandInterceptor performanceDetectionCommandInterceptor)
    : DbContext(options), IUnitOfWork, IAppDbConnection
{
    private readonly DatabaseSettings _databaseSettings = databaseSettings.Value;
    private readonly IInterceptor[] _interceptors = [
        enumerationSaveChangesInterceptor, 
        insertAuditableEntitiesSaveChangesInterceptor, 
        performanceDetectionCommandInterceptor];
    private IDbContextTransaction? _currentTransaction;

    public bool HasActiveTransaction => _currentTransaction != null;

    public DbSet<Material> Materials => Set<Material>();
    public DbSet<TransactionalPartner> TransactionalPartners => Set<TransactionalPartner>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<BoM> BoMs => Set<BoM>();

    //TODO: we can override ChangeTracker api, need to somehow override this implementation for reducing boxing. Maybe apply Source Generator
    //public override ChangeTracker ChangeTracker { get; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //base.OnConfiguring(optionsBuilder);
        optionsBuilder
            .UseSqlServer(_databaseSettings.ConnectionString, msSqlOptions =>
            {
                msSqlOptions.EnableRetryOnFailure(maxRetryCount: _databaseSettings.MaxRetryCount, maxRetryDelay: TimeSpan.FromSeconds(_databaseSettings.MaxRetryDelay),
                    errorNumbersToAdd: null);

                //https://devblogs.microsoft.com/dotnet/announcing-ef8-preview-4/ Older versions of SQL Server for OPENJSON function in SQL 2019 or later
                msSqlOptions.UseCompatibilityLevel(160);
                msSqlOptions.MigrationsHistoryTable("AppDbContextMigrationsHistory");
            })
            // Disable lazy-loading for entities are not virtual: https://learn.microsoft.com/en-gb/ef/core/what-is-new/ef-core-8.0/whatsnew - Opt-out of lazy-loading for specific navigations
            .UseLazyLoadingProxies(b => b.IgnoreNonVirtualNavigations())
            //Interceptor: https://learn.microsoft.com/en-us/ef/core/logging-events-diagnostics/interceptors
            //https://devblogs.microsoft.com/dotnet/announcing-ef7-preview7-entity-framework/
            .AddInterceptors(_interceptors);

        if (_databaseSettings.EnableLog)
        {
            optionsBuilder
                .EnableSensitiveDataLogging(true)
                .EnableDetailedErrors(true);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        //apply dbInterceptor to retrieve this domain events for the 3rd handler in TransactionalBehaviour.cs
        // List<AggregateRoot> entities = ChangeTracker
        //     .Entries()
        //     .Where(x => x.Entity is AggregateRoot aggregateRoot && aggregateRoot.DomainEvents.Count > 0)
        //     .Select(x => (AggregateRoot)x.Entity)
        //     .ToList();

        int result = await base.SaveChangesAsync(cancellationToken);

        // foreach (AggregateRoot entity in entities)
        // {
        //     _eventDispatcher.Dispatch(entity.DomainEvents);
        //     entity.ClearDomainEvents();
        // }

        return result;
    }

    public async Task<IDbContextTransaction?> BeginTransactionAsync()
    {
        if (_currentTransaction != null) return null;

        _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

        return _currentTransaction;
    }

    public async Task CommitTransactionAsync(IDbContextTransaction? transaction)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

        try
        {
            await transaction.CommitAsync();
        }
        catch
        {
            RollbackTransaction();
            throw;
        }
        finally
        {
            DisposeTransaction();
        }
    }

    public void RollbackTransaction()
    {
        try
        {
            _currentTransaction?.Rollback();
        }
        finally
        {
            DisposeTransaction();
        }
    }

    private void DisposeTransaction()
    {
        if (_currentTransaction != null)
        {
            _currentTransaction.Dispose();
            _currentTransaction = null;
        }
    }

    public bool CanConnect()
        => Database.CanConnect();
}

//internal class AppDbContextDesignFactory : IDesignTimeDbContextFactory<AppDbContext>
//{
//    public AppDbContextDesignFactory()
//    {
        
//    }
//    public AppDbContext CreateDbContext(string[] args)
//    {
//        string connectionString = "Server=localhost,1433;Database=DDD.MrpDanu;User Id=sa;Password=reallyStrongPwd123;TrustServerCertificate=true";
//        ////string connectionString = "Server=son-quang-pham-0131,1433;Database=DDD.MrpDanu;User Id=sa;Password=Vsa*12345#;TrustServerCertificate=true";
//        ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
//        {
//        });
//        IOptions<DatabaseSettings> options = Options.Create(new DatabaseSettings
//        {
//            ConnectionString = connectionString
//        });
//        var log = new LoggingDbCommandInterceptor(loggerFactory.CreateLogger<LoggingDbCommandInterceptor>(), options);
//        var externalDbContext = new ExternalDbContext(options, false, log);
//        var insertAuditable = new InsertAuditableEntitiesSaveChangesInterceptor(externalDbContext);
//        return new AppDbContext(
//            options,
//            false,
//            log,
//            insertAuditable);
//    }
//}
