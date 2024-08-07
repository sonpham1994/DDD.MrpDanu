using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;

namespace Benchmark.Infrastructure.CacheEntityAuditChanged;

public class AppDbContextCached : DbContext
{
    private readonly string _connectionString = "Server=localhost,1433;Database=TestCompanyEntity;User Id=sa;Password=reallyStrongPwd123;TrustServerCertificate=true";
    public DbSet<Test1> Test1 { get; set; }
    public DbSet<Test2> Test2 { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
        optionsBuilder
            .UseSqlServer(_connectionString, msSqlOptions =>
            {
                msSqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null);
            });
    }
}

public static class Ext
{
    private static readonly Func<EntityEntry, bool> _getEntityAuditsChanged = new(IsEntityAuditsChanged);
    private static bool IsEntityAuditsChanged(EntityEntry x)
    {
        return x.Entity.IsEntityAudit()
               && (x.State.IsModifiedEntityState()
                   || (x.State == EntityState.Unchanged && x.IsInternalEntityModified()));
    }
    
    public static bool IsEntityAudit(this object obj)
    {
        var interfaceType = typeof(IAuditTable);
        var result = interfaceType.IsAssignableFrom(obj.GetType());
        return result;
    }

    // private static HashSet<Type> CreateAuditTables()
    // {
    //     var interfaceType = typeof(IAuditTable);
    //     var types = typeof(AppDbContextCached).Assembly.GetTypes()
    //         .Where(t => interfaceType.IsAssignableFrom(t) && t.IsClass)
    //         .ToHashSet();
    //
    //     return types;
    // }
    
    public static IReadOnlyList<EntityEntry> GetEntityAuditsChangedCahced(this AppDbContextCached appDbContext)
    {
        return appDbContext.ChangeTracker
            .Entries()
            .Where(_getEntityAuditsChanged)
            /*
             *  we need to create a list of audit tables this, not in GetAuditTables method. Because after saving
             * is made, the entity state will reset. For example Added would be Unchanged, Deleted would be Detached
             */
            .ToList();
    }
    
    public static IReadOnlyList<EntityEntry> GetEntityAuditsChangedWithoutCached(this AppDbContextCached appDbContext)
    {
        return appDbContext.ChangeTracker
            .Entries()
            .Where(x=>x.Entity.IsEntityAudit()
                   && (x.State.IsModifiedEntityState()
                       || (x.State == EntityState.Unchanged && x.IsInternalEntityModified())))
            /*
             *  we need to create a list of audit tables this, not in GetAuditTables method. Because after saving
             * is made, the entity state will reset. For example Added would be Unchanged, Deleted would be Detached
             */
            .ToList();
    }
    
    public static bool IsModifiedEntityState(this EntityState entityState)
    {
        return entityState is EntityState.Added or EntityState.Modified or EntityState.Deleted;
    }

    public static bool IsInternalEntityModified(this EntityEntry entity)
    {
        // for internal entities are object
        // for ValueObject with Owned Entity Type. The problem here is that whenever we reassign
        // the owned entity type, it always is a modified state, although the value of this remains unchanged
        // Let's wait the ComplexType from .NET 8 to check whether is it addressed or not.
        return entity.References.Any(j => j.IsModified)
               // for internal entities which are collections
               || entity.Collections.Any(j => j.IsModified);
    }
}

public class Test1 : IAuditTable
{
    public int Id { get; set; }
}

public class Test2
{
    public int Id { get; set; }
}

public interface IAuditTable;