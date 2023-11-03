using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public static class Extensions
{
    //improve performance and reduce memory allocation instead of using .ToString() for enum
    public static string ToEnumString(EntityState entityState)
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
}