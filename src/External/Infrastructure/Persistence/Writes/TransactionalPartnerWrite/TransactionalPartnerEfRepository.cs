using Application.Interfaces.Writes.TransactionalPartnerWrite;
using Domain.Extensions;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate;
using Domain.SharedKernel.ValueObjects;
using Infrastructure.Persistence.Writes.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Writes.TransactionalPartnerWrite;

internal sealed class TransactionalPartnerEfRepository
    : BaseEfGuidStronglyTypedIdRepository<TransactionalPartner, TransactionalPartnerId>, ITransactionalPartnerRepository
{
    public TransactionalPartnerEfRepository(AppDbContext context) : base(context)
    {
    }

    public override async ValueTask<TransactionalPartner?> GetByIdAsync(TransactionalPartnerId id, CancellationToken cancellationToken)
    {
        TransactionalPartner? transactionalPartner = null;
        if (id.IsEmpty())
            return transactionalPartner;

        transactionalPartner = await base.GetByIdAsync(id, cancellationToken);

        SetValueForEnumerationData(transactionalPartner);

        return transactionalPartner;
    }

    public async Task DeleteAsync(TransactionalPartnerId id, CancellationToken cancellationToken)
    {
        await context.Database.ExecuteSqlAsync($"DELETE TransactionalPartner WHERE Id = {id.Value}", cancellationToken);
    }

    private void SetValueForEnumerationData(TransactionalPartner? transactionalPartner)
    {
        if (transactionalPartner is null)
            return;

        /*
        * if you want to reduce round trip from backend to database, you may use reflection for enumeration data type, due to the
        fact that the enumeration data type store own data in memory, hence we don't need to make a call to database, we may
        only need the data that we store in memory. In fact, EF Core also use reflection to bind data to your entity. 
        By doing this, we gain some benefits:
            - We reduce the round-trip but the use of reflection remains intact
            - We reduce the memory usage when we retrieve the enumeration data type from database, which increase additional 
            enumeration object, we just use the enumeration object that store in database.
            - The binding member of enumeration don't use reflection. For example, the MaterialType has Id, Name properties and 
            we avoid binding those properties using reflection, but the binding MaterialType to Entity like Material remain intact. 
        Please check EnumerationLoadingBenchmark in Benchmark.Infrastructure
        */

        // transactionalPartner.BindingEnumeration<TransactionalPartnerType>(ShadowProperties.TransactionalPartnerTypeId, nameof(TransactionalPartner.TransactionalPartnerType), context);
        // transactionalPartner.BindingEnumeration<TransactionalPartnerType>(ShadowProperties.TransactionalPartnerTypeId, nameof(TransactionalPartner.TransactionalPartnerType), context);
        // transactionalPartner.BindingEnumeration<CurrencyType>(ShadowProperties.CurrencyTypeId, nameof(TransactionalPartner.CurrencyType), context);
        // transactionalPartner.BindingEnumeration<LocationType>(ShadowProperties.LocationTypeId, nameof(TransactionalPartner.LocationType), context);
        transactionalPartner.TaxNo.BindingEnumeration<Country>(ShadowProperties.CountryId, nameof(TransactionalPartner.TaxNo.Country), context);
        transactionalPartner.Address.BindingEnumeration<Country>(ShadowProperties.CountryId, nameof(TransactionalPartner.Address.Country), context);

        transactionalPartner
            .BindingEnumeration(transactionalPartner.TransactionalPartnerType, ShadowProperties.TransactionalPartnerTypeId, nameof(TransactionalPartner.TransactionalPartnerType), context)
            .BindingEnumeration(transactionalPartner.CurrencyType, ShadowProperties.CurrencyTypeId, nameof(TransactionalPartner.CurrencyType), context)
            .BindingEnumeration(transactionalPartner.LocationType, ShadowProperties.LocationTypeId, nameof(TransactionalPartner.LocationType), context);
    }

    public async Task BulkDeleteAsync(TransactionalPartnerId id, CancellationToken cancellationToken)
    {
        await dbSet.Where(x => x.Id == id).ExecuteDeleteAsync(cancellationToken);
    }
}