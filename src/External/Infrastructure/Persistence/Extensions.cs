using Domain.SupplyChainManagement.TransactionalPartnerAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Persistence;

internal static class Extensions
{
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

    public static IReadOnlyList<byte> GetSupplierTypeIds()
    {
        return TransactionalPartnerType.GetSupplierTypes().ToArray().Select(x => x.Id).ToList();
    }
}