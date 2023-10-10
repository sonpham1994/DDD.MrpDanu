using Domain;
using Domain.SharedKernel.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Persistence.Interceptors;

internal sealed class EnumerationSaveChangesInterceptor : SaveChangesInterceptor
{
    private static readonly Type[] EnumerationTypes = GetEnumerationTypes();
    
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var dbContext = eventData.Context;
        var enumerationEntries = dbContext!.ChangeTracker.Entries()
            .Where(x => EnumerationTypes.Contains(x.Entity.GetType()) && x.State != EntityState.Unchanged)
            .ToList();

        foreach (var enumerationEntry in enumerationEntries)
        {
            enumerationEntry.State = EntityState.Unchanged;
        }

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }
    
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        var dbContext = eventData.Context;
        var enumerationEntries = dbContext!.ChangeTracker.Entries()
            .Where(x => EnumerationTypes.Contains(x.Entity.GetType()) && x.State != EntityState.Unchanged)
            .ToList();

        foreach (var enumerationEntry in enumerationEntries)
        {
            enumerationEntry.State = EntityState.Unchanged;
        }

        return base.SavingChanges(eventData, result);
    }
    
    
    private static Type[] GetEnumerationTypes()
    {
        Func<Type, bool> enumerationPredicate = x => x.BaseType is not null
                        && x.BaseType.IsGenericType
                        && x.BaseType.GetGenericTypeDefinition() == typeof(Enumeration<>);
        
        
        var enumerationTypes = DomainAssembly.Instance.GetTypes().Where(enumerationPredicate).ToArray();
        var enumerationInfrastructureTypes = InfrastructureAssembly.Instance.GetTypes().Where(enumerationPredicate).ToArray();

        var types = new Type[enumerationTypes.Length + enumerationInfrastructureTypes.Length];
        enumerationTypes.CopyTo(types, 0);
        enumerationInfrastructureTypes.CopyTo(types, enumerationTypes.Length);

        return types;
    }
}