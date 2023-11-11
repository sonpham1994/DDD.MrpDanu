using Infrastructure.Persistence.Externals;
using Infrastructure.Persistence.Write;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Persistence.Interceptors;

internal sealed class InsertAuditableEntitiesSaveChangesInterceptor : SaveChangesInterceptor
{
    private readonly ExternalDbContext _externalDbContext;
    
    public InsertAuditableEntitiesSaveChangesInterceptor(ExternalDbContext externalDbContext)
    {
        _externalDbContext = externalDbContext;
    }
    
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is AppDbContext appDbContext)
        {
            _externalDbContext.SetAuditTables(appDbContext);
        }

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }
    
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        if (eventData.Context is AppDbContext appDbContext)
        {
            _externalDbContext.SetAuditTables(appDbContext);
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
            _externalDbContext.ClearAuditTables();
        }

        return await base.SavedChangesAsync(eventData, result, cancellationToken);
    }
    
    public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
    {
        if (eventData.Context is AppDbContext && result == 0)
        {
            _externalDbContext.ClearAuditTables();
        }

        return base.SavedChanges(eventData, result);
    }
}