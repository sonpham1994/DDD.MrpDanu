using System.Data;
using Domain.MaterialManagement.MaterialAggregate;
using Domain.MaterialManagement.TransactionalPartnerAggregate;
using Domain.ProductManagement;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Infrastructure.EventDispatchers;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.Design;
using Domain.SharedKernel.Base;
using Application.Interfaces;
using Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore.Storage;
using Infrastructure.Persistence.Externals;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;

namespace Infrastructure.Persistence.Write;

internal sealed class AppDbContext : DbContext, IUnitOfWork
{
    private readonly EventDispatcher _eventDispatcher;
    private readonly DatabaseSettings _databaseSettings;
    private readonly bool _isProduction;
    private readonly LoggingDbCommandInterceptor _loggingDbCommandInterceptor;
    private readonly IInterceptor[] _interceptors;
    private IDbContextTransaction? _currentTransaction;

    public bool HasActiveTransaction => _currentTransaction != null;

    public DbSet<Material> Materials => Set<Material>();
    public DbSet<TransactionalPartner> TransactionalPartners => Set<TransactionalPartner>();
    public DbSet<Product> Products => Set<Product>();
    
    internal AppDbContext(IOptions<DatabaseSettings> databaseSettings
        , bool isProduction
        , EventDispatcher eventDispatcher
        , LoggingDbCommandInterceptor loggingDbCommandInterceptor
        , params IInterceptor[] interceptors)
    {
        _databaseSettings = databaseSettings.Value;
        _isProduction = isProduction;
        _eventDispatcher = eventDispatcher;
        _loggingDbCommandInterceptor = loggingDbCommandInterceptor;
        _interceptors = interceptors;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlServer(_databaseSettings.ConnectionString, msSqlOptions =>
            {
                msSqlOptions.EnableRetryOnFailure(maxRetryCount:_databaseSettings.MaxRetryCount, maxRetryDelay: TimeSpan.FromSeconds(_databaseSettings.MaxRetryDelay),
                    errorNumbersToAdd: null);
            })
            .UseLazyLoadingProxies()
            .AddInterceptors(_interceptors);

        if (!_isProduction)
        {
            optionsBuilder
                .EnableSensitiveDataLogging()
                //Interceptor: https://learn.microsoft.com/en-us/ef/core/logging-events-diagnostics/interceptors
                //https://devblogs.microsoft.com/dotnet/announcing-ef7-preview7-entity-framework/
                .AddInterceptors(_loggingDbCommandInterceptor);
            
        }
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        List<AggregateRoot> entities = ChangeTracker
            .Entries()
            .Where(x => x.Entity is AggregateRoot aggregateRoot && aggregateRoot.DomainEvents.Count > 0)
            .Select(x => (AggregateRoot)x.Entity)
            .ToList();

        int result = await base.SaveChangesAsync(cancellationToken);

        foreach (AggregateRoot entity in entities)
        {
            _eventDispatcher.Dispatch(entity.DomainEvents);
            entity.ClearDomainEvents();
        }

        return result;
    }
    
    public async Task<IDbContextTransaction?> BeginTransactionAsync()
    {
        if (_currentTransaction != null) return null;

        _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

        return _currentTransaction;
    }

    public async Task CommitTransactionAsync(IDbContextTransaction transaction)
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
}

internal class AppDbContextDesignFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        string connectionString = "Server=LAPTOP-IHQC4RF9,1433;Database=DDD.MrpDanu;User Id=sa;Password=Vsa*12345#;TrustServerCertificate=true";
        //string connectionString = "Server=localhost,1433;Database=DDD.MrpDanu;User Id=sa;Password=reallyStrongPwd123;TrustServerCertificate=true";
        ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
        {
        });
        IOptions<DatabaseSettings> options = Options.Create(new DatabaseSettings
        {
            ConnectionString = connectionString
        });
        var log = new LoggingDbCommandInterceptor(loggerFactory.CreateLogger<LoggingDbCommandInterceptor>(), options);
        var externalDbContext = new ExternalDbContext(options, false, log);
        var insertAuditable = new InsertAuditableEntitiesSaveChangesInterceptor(externalDbContext);
        return new AppDbContext(
            options,
            false,
            new EventDispatcher(),
            log,
            insertAuditable);
    }
}
