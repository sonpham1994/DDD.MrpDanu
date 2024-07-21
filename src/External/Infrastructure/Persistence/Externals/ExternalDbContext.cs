using Infrastructure.Persistence.Externals.AuditTables;
using Infrastructure.Persistence.Interceptors;
using Infrastructure.Persistence.Writes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure.Persistence.Externals;

internal sealed class ExternalDbContext(
    DbContextOptions<ExternalDbContext> options,
    IOptions<DatabaseSettings> databaseSettings,
    EnumerationSaveChangesInterceptor enumerationSaveChangesInterceptor,
    PerformanceDetectionCommandInterceptor performanceDetectionCommandInterceptor)
    : DbContext(options)
{
    private readonly DatabaseSettings _databaseSettings = databaseSettings.Value;
    private readonly IInterceptor[] _interceptors = [enumerationSaveChangesInterceptor, performanceDetectionCommandInterceptor];
    private List<AuditTable> _auditTables = [];

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

        if (_databaseSettings.EnableLog)
        {
            optionsBuilder
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }
    }

    public IReadOnlyList<AuditTable> GetAuditTables() => _auditTables;

    public void SetAuditTables(AppDbContext appDbContext)
    {
        _auditTables = appDbContext.GetEntityAuditsChanged()
            .Select(x => x.CreateAuditTable())
            .ToList();
    }

    public void ClearAuditTables()
    {
        _auditTables.Clear();
    }
}

//internal class ExternalDbContextDesignFactory : IDesignTimeDbContextFactory<ExternalDbContext>
//{
//    public ExternalDbContext CreateDbContext(string[] args)
//    {
//        string connectionString = "Server=LAPTOP-IHQC4RF9,1433;Database=DDD.MrpDanu;User Id=sa;Password=Vsa*12345#;TrustServerCertificate=true";
//        //string connectionString = "Server=localhost,1433;Database=DDD.MrpDanu;User Id=sa;Password=reallyStrongPwd123;TrustServerCertificate=true";
//        ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
//        {
//        });
//        IOptions<DatabaseSettings> options = Options.Create(new DatabaseSettings
//        {
//            ConnectionString = connectionString
//        });
//        return new ExternalDbContext(
//            options,
//            false,
//            new LoggingDbCommandInterceptor(loggerFactory.CreateLogger<LoggingDbCommandInterceptor>(), options));
//    }
//}