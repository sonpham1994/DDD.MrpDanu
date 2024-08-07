using Domain.AuditTrail.SharedKernel;
using SharedKernel;
using Infrastructure.Errors;
using Infrastructure.Persistence.Externals.AuditTables;
using Infrastructure.Persistence.Writes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Persistence;

internal static class Extensions
{
    public static IReadOnlyList<EntityEntry> GetEntityAuditsChanged(this AppDbContext appDbContext)
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

        return appDbContext.ChangeTracker
            .Entries()
            // maybe need cache this delegation func for improving performance: pls check Benchmark.Infrastructure/CacheDelegateFunction
            // for Infor: https://kevingosse.medium.com/performance-best-practices-in-c-b85a47bdd93a
            .Where(x => x.Entity.IsEntityAudit()
                        && (x.State.IsModifiedEntityState()
                            || (x.State == EntityState.Unchanged && x.IsInternalEntityModified()))
                    )
            /*
             *  we need to create a list of audit tables this, not in GetAuditTables method. Because after saving
             * is made, the entity state will reset. For example Added would be Unchanged, Deleted would be Detached
             */
            .ToList();
    }
    
    public static Result<StateAuditEntity?> GetStateAuditEntity(this EntityEntry entity)
    {
        var entityState = entity.State;
        var state = entityState switch
        {
            EntityState.Added => StateAuditEntity.Added,
            EntityState.Modified => StateAuditEntity.Modified,
            EntityState.Deleted => StateAuditEntity.Deleted,
            _ => StateAuditEntity.None
        };

        if (state == StateAuditEntity.None)
        {
            if (entity.IsInternalEntityModified())
            {
                state = StateAuditEntity.Modified;
            }
            else
            {
                return InfrastructureDomainErrors.AuditData.InvalidStateAuditData(entityState.ToEnumString());
            }
        }

        return state;
    }
    
    //improve performance and reduce memory allocation instead of using .ToString() for enum
    //https://www.youtube.com/watch?v=BoE5Y6Xkm6w&t=449s&ab_channel=NickChapsas
    public static string ToEnumString(this EntityState entityState)
    {
        return entityState switch
        {
            EntityState.Added => nameof(EntityState.Added),
            EntityState.Deleted => nameof(EntityState.Deleted),
            EntityState.Detached => nameof(EntityState.Detached),
            EntityState.Modified => nameof(EntityState.Modified),
            EntityState.Unchanged => nameof(EntityState.Unchanged),
            _ => string.Empty
        };
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