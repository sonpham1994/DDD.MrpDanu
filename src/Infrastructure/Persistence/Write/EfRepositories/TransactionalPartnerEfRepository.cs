using System.Reflection;
using Application.Interfaces.Repositories;
using Domain.MaterialManagement.TransactionalPartnerAggregate;
using Domain.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Write.EfRepositories;

internal sealed class TransactionalPartnerEfRepository : BaseEfRepository<TransactionalPartner>, ITransactionalPartnerRepository
{
    public TransactionalPartnerEfRepository(AppDbContext context) : base(context)
    {
    }

    public async ValueTask<TransactionalPartner?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        TransactionalPartner? transactionalPartner = null;
        if (id == Guid.Empty)
            return transactionalPartner;

        transactionalPartner = await base.GetByIdAsync(id, cancellationToken);
       
        SetValueForEnumerationData(transactionalPartner);
        
        return transactionalPartner;
    }
    
    public async ValueTask<IReadOnlyList<TransactionalPartner>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
    {
        IReadOnlyList<TransactionalPartner> transactionalPartners = Array.Empty<TransactionalPartner>();
        var transactionalPartnerIds = ids.Where(x => x != Guid.Empty).Distinct().ToList();
        
        if (!transactionalPartnerIds.Any())
            return transactionalPartners;

        transactionalPartners = await dbSet.Where(x => transactionalPartnerIds.Contains(x.Id)).ToListAsync(cancellationToken);

        foreach(var transactionalPartner in transactionalPartners)
        {
            //reduce N+1 problem for enumeration data type
            SetValueForEnumerationData(transactionalPartner);
        }
        
        return transactionalPartners;
    }
    
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await context.Database.ExecuteSqlAsync($"DELETE TransactionalPartner WHERE Id = {id}", cancellationToken);
    }

    private void SetValueForEnumerationData(TransactionalPartner? transactionalPartner)
    {
        if (transactionalPartner is null)
            return;
        
        /*
        * if you want to reduce round trip from backend to database, you may use reflection for enumeration data type, due to the
        fact that the enumeration data type store own data in memory, hence we don't need to make a call to database, we may
        only the data that we store in memory. In fact, EF Core also use reflection to bind data to your entity. By doing this
        we reduce the round-trip but the use of reflection remains intact. And another benefit is that, we reduce the memory
        usage when we retrieve the enumeration data type from database, which increase additional enumeration object. We just
        use the enumeration object that store in database.
        Please check EnumerationLoadingBenchmark in Benchmark.Infrastructure
        */
        
        transactionalPartner.BindingEnumeration<TransactionalPartner, TransactionalPartnerType>(ShadowProperties.TransactionalPartnerTypeId, nameof(TransactionalPartner.TransactionalPartnerType), context);
        transactionalPartner.BindingEnumeration<TransactionalPartner, CurrencyType>(ShadowProperties.CurrencyTypeId, nameof(TransactionalPartner.CurrencyType), context);
        transactionalPartner.BindingEnumeration<TransactionalPartner, LocationType>(ShadowProperties.LocationTypeId, nameof(TransactionalPartner.LocationType), context);
        transactionalPartner.TaxNo.BindingEnumeration<TaxNo, Country>(ShadowProperties.CountryId, nameof(TransactionalPartner.TaxNo.Country), context);
    }
}