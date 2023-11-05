using Domain.Extensions;
using Infrastructure.Persistence.Externals.AuditTables;
using Infrastructure.Persistence.Externals.AuditTables.Factories;
using Infrastructure.Persistence.Interceptors;
using Infrastructure.Persistence.Write;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure.Persistence.Externals;

internal sealed class ExternalDbContext : DbContext
{
    private readonly DatabaseSettings _databaseSettings;
    private readonly bool _isProduction;
    private readonly LoggingDbCommandInterceptor _loggingDbCommandInterceptor;
    private readonly IInterceptor[] _interceptors;
    private List<AuditTable> _auditTables;

    internal ExternalDbContext(IOptions<DatabaseSettings> databaseSettings
        , bool isProduction
        , LoggingDbCommandInterceptor loggingDbCommandInterceptor
        , params IInterceptor[] interceptors)
    {
        _databaseSettings = databaseSettings.Value;
        _loggingDbCommandInterceptor = loggingDbCommandInterceptor;
        _isProduction = isProduction;
        _interceptors = interceptors;
    }

    public DbSet<AuditTable> AuditTables => Set<AuditTable>();
    public DbSet<StateAuditTable> StateAuditTables => Set<StateAuditTable>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlServer(_databaseSettings.ConnectionString, msSqlOptions =>
            {
                msSqlOptions.EnableRetryOnFailure(maxRetryCount:_databaseSettings.MaxRetryCount, maxRetryDelay: TimeSpan.FromSeconds(_databaseSettings.MaxRetryDelay),
                    errorNumbersToAdd: null);
            })
            .AddInterceptors(_interceptors);

        if (!_isProduction)
        {
            optionsBuilder
                .EnableSensitiveDataLogging()
                .AddInterceptors(_loggingDbCommandInterceptor);
        }
    }

    public IReadOnlyList<AuditTable> GetAuditTables() => _auditTables;

    public void SetAuditTables(AppDbContext appDbContext)
    {
        //due to the fact that EF Core cannot mark owned entity type as Modified state, so we remove the condition
        //of x.State != EntityState.Modified and get all data with implementing IAuditTable. For example modifying
        //Surcharge or Price in MaterialCostManagement
        //https://stackoverflow.com/questions/46872428/owned-type-property-not-persisting-for-a-modified-entity-in-ef-core?fbclid=IwAR3wWOXGo6d_A_ETasDDvJ12i1rJ2rRY_LnGurAZuR5Avht_gAa6qmi2MZ8
        //Because EF core treats owned entity type as a splitting table, so it works as navigation properties.
        //When we update owned entity type, depending on FK (which is shadow property Id) of owned entity type
        //  - Added: If your owned entity type is detached mode (or shadow property Id is empty)
        //  - Deleted: If the navigation property to the owned entity "is set to null", then this removes the reference to it from the parent entity, so there is no reference to it from the parent EntityEntry.
        //https://github.com/dotnet/efcore/issues/16513
        //https://github.com/dotnet/efcore/issues/27848
        //https://github.com/dotnet/efcore/pull/21259
        //This is behaviour is of navigation properties, as well as owned entity type
        //So if don't change anything and call savechange, they always mark as IsModified due to Shadow PropertyId is empty
        //we can work around by checking if they are different, take a look at SetMaterialCost in MaterialCostManagement,
        //  for example. We can take a full advantage of Interceptor from DbContext, after saving changes completed,
        //  if the result from SaveChanges equals to 0, it means it don't modify anything

        /* if you want to take advantage of set ModifiedDate by Interceptors, you can face the same problem with this if you modify 
         *  the owned entity type https://github.com/dotnet/efcore/issues/29848
         */

        /*
         * Some scenarios can work around for Value Object when working with EF Core from Julie Lerman
         * https://learn.microsoft.com/en-us/archive/msdn-magazine/2018/april/data-points-ef-core-2-owned-entities-and-temporary-work-arounds
         */
        /*
         * another problem with this approach is that, now it record data as Domain Model, these data just reserve for tracing and reading purpose,
         *  so maybe we need to write pure entity for that read operation. Or we can use Domain model for checking whether our domain model will be
         *  invalid state or not, but this situation is rarely to happen (I think so).
         * And also, these data persist with LazyLoader coming from Proxy - Lazy Loading
         */
        
        /*we don't directly convert EntityEntry to AuditTable, because we're using lazy loading, and if we convert
         * EntityEntry to AuditTable here, we load the Entities from db by using lazy load in Serialize method in
         * AuditTable Factory. If we have modified data, it's ok, but we don't have anything to change them, we make an
         * surplus for loading data, for example:
         * var a = appDbContext.ChangeTracker
             .Entries()
            .Where(x => AuditTableFactory.Entities.Any(j => j == x.Entity.GetUnproxiedType()!)
                         && (x.State != EntityState.Unchanged
                             || (x.State == EntityState.Unchanged // for ValueObject with Owned Entity Type
                                 && x.References
                                     .Any(j => j.IsModified))))
             .Select(AuditTableFactory.Create) ---> make a further loading here due to Lazy loading
             .ToList();
         */
        _auditTables = appDbContext.ChangeTracker
            .Entries()
            .Where(x => AuditTableFactory.Entities.Any(j => j == x.Entity.GetUnproxiedType()!)
                        && (x.State.IsModifiedEntityState()
                            || (x.State == EntityState.Unchanged && x.IsInternalEntityModified()))
                    )
            /*
             *  we need to create a list of audit tables this, not in GetAuditTables method. Because after saving
             * is made, the entity state will reset. For example Added would be Unchanged, Deleted would be Detached
             */
            .Select(AuditTableFactory.Create) 
            .ToList();
    }

    public void ClearAuditTables()
    {
        _auditTables.Clear();
    }
}

internal class ExternalDbContextDesignFactory : IDesignTimeDbContextFactory<ExternalDbContext>
{
    public ExternalDbContext CreateDbContext(string[] args)
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
        return new ExternalDbContext(
            options,
            false,
            new LoggingDbCommandInterceptor(loggerFactory.CreateLogger<LoggingDbCommandInterceptor>(), options));
    }
}