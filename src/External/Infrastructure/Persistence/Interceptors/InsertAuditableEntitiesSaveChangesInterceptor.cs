using Infrastructure.Persistence.Externals;
using Infrastructure.Persistence.Writes;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Persistence.Interceptors;

internal sealed class InsertAuditableEntitiesSaveChangesInterceptor(ExternalDbContext externalDbContext)
    : SaveChangesInterceptor
{
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is AppDbContext appDbContext)
        {
            externalDbContext.SetAuditTables(appDbContext);
        }

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }
    
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        if (eventData.Context is AppDbContext appDbContext)
        {
            externalDbContext.SetAuditTables(appDbContext);
        }

        return base.SavingChanges(eventData, result);
    }

    public override async ValueTask<int> SavedChangesAsync(
        SaveChangesCompletedEventData eventData,
        int result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is AppDbContext && result == 0)
        {
            externalDbContext.ClearAuditTables();
        }

        return await base.SavedChangesAsync(eventData, result, cancellationToken);
    }
    
    public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
    {
        if (eventData.Context is AppDbContext && result == 0)
        {
            externalDbContext.ClearAuditTables();
        }

        return base.SavedChanges(eventData, result);
    }
}