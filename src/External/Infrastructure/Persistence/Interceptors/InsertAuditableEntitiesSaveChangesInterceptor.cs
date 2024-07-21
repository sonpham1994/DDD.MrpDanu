using Domain.SharedKernel.Base;
using Infrastructure.Persistence.Externals;
using Infrastructure.Persistence.Writes;
using Infrastructure.Persistence.AuditTrail;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Persistence.Interceptors;

internal sealed class InsertAuditableEntitiesSaveChangesInterceptor(
    ExternalDbContext externalDbContext, 
    AuditTrailDomainEvents auditTrailDomainEvents,
    IDomainEventDispatcher domainEventDispatcher)
    : SaveChangesInterceptor
{
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is AppDbContext appDbContext)
        {
            //externalDbContext.SetAuditTables(appDbContext);
            var entityAuditsChanged = appDbContext.GetEntityAuditsChanged();
            foreach (var entityAuditChanged in entityAuditsChanged)
            {
                var entity = entityAuditChanged.Entity;
                var state = entityAuditChanged.GetStateAuditEntity().Value!;
                if (entity is IAuditTable auditTable)
                {
                    AuditTrailDomainEventRaising.RaiseAuditTrailDomainEvents(auditTable, auditTrailDomainEvents, state);
                }
            }
        }

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }
    
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        if (eventData.Context is AppDbContext appDbContext)
        {
            //externalDbContext.SetAuditTables(appDbContext);
            var entityAuditsChanged = appDbContext.GetEntityAuditsChanged();
            foreach (var entityAuditChanged in entityAuditsChanged)
            {
                var entity = entityAuditChanged.Entity; 
                var state = entityAuditChanged.GetStateAuditEntity().Value!;
                if (entity is IAuditTable auditTable)
                {
                    AuditTrailDomainEventRaising.RaiseAuditTrailDomainEvents(auditTable, auditTrailDomainEvents, state);
                }
            }
        }

        return base.SavingChanges(eventData, result);
    }

    public override async ValueTask<int> SavedChangesAsync(
        SaveChangesCompletedEventData eventData,
        int result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is AppDbContext)
        {
            if (result == 0)
            {
                externalDbContext.ClearAuditTables();
                auditTrailDomainEvents.ClearDomainEvents();
            }
            else
            {
                // externalDbContext is implemented at transactionBehaviour if result > 0. Maybe we will move it to here
                await domainEventDispatcher.DispatchAndClearEvents(auditTrailDomainEvents);
            }
        }

        return await base.SavedChangesAsync(eventData, result, cancellationToken);
    }
    
    public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
    {
        if (eventData.Context is AppDbContext)
        {
            if (result == 0)
            {
                externalDbContext.ClearAuditTables();
                auditTrailDomainEvents.ClearDomainEvents();
            }
            else
            {
                // externalDbContext is implemented at transactionBehaviour if result > 0. Maybe we will move it to here
                domainEventDispatcher.DispatchAndClearEvents(auditTrailDomainEvents).GetAwaiter().GetResult();
            }
        }

        return base.SavedChanges(eventData, result);
    }
}