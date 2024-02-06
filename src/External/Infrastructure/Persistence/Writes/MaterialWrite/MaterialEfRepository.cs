using Domain.SharedKernel.ValueObjects;
using Domain.Extensions;
using Infrastructure.Persistence.Writes.Extensions;
using Application.Interfaces.Writes.MaterialWrite;
using Domain.SharedKernel.Enumerations;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;

namespace Infrastructure.Persistence.Writes.MaterialWrite;

internal sealed class MaterialEfRepository : BaseEfGuidStronglyTypedIdRepository<Material, MaterialId>, IMaterialRepository
{
    public MaterialEfRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Material?> GetByIdAsync(MaterialId id, CancellationToken cancellationToken = default)
    {
        Material? material = null;

        if (id.IsEmpty())
            return material;

        material = await base.GetByIdAsync(id, cancellationToken);
        if (material is null)
            return material;

        await dbSet.Entry(material).Collection(x => x.MaterialSupplierCosts).LoadAsync(cancellationToken);

        //material = await dbSet
        //    .Include(x => x.MaterialType)
        //    .Include(x => x.RegionalMarket)
        //    .Include(x => x.MaterialSupplierCosts)
        //    .ThenInclude(x => x.TransactionalPartner).ThenInclude(x => x.TransactionalPartnerType)
        //    .Include(x => x.MaterialSupplierCosts).ThenInclude(x => x.TransactionalPartner).ThenInclude(x => x.CurrencyType)
        //    .FirstOrDefaultAsync(x => x.Id == id);

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
        //material.BindingEnumeration<MaterialType>(ShadowProperties.MaterialTypeId, nameof(Material.MaterialType), context);
        //material.BindingEnumeration<RegionalMarket>(ShadowProperties.RegionalMarketId, nameof(Material.RegionalMarket), context);

        // material
        //     .BindingEnumeration<MaterialType, MaterialId>(ShadowProperties.MaterialTypeId, nameof(Material.MaterialType), context)
        //     .BindingEnumeration<RegionalMarket, MaterialId>(ShadowProperties.RegionalMarketId, nameof(Material.RegionalMarket), context);

         material
            .BindingEnumeration(material.MaterialType, ShadowProperties.MaterialTypeId, nameof(Material.MaterialType), context)
            .BindingEnumeration(material.RegionalMarket, ShadowProperties.RegionalMarketId, nameof(Material.RegionalMarket), context);

        foreach (var materialSupplierCost in material.MaterialSupplierCosts)
        {
            materialSupplierCost.Price.BindingEnumeration<CurrencyType>(ShadowProperties.CurrencyTypeId,
                nameof(MaterialSupplierCost.Price.CurrencyType), context);
            materialSupplierCost.Surcharge.BindingEnumeration<CurrencyType>(ShadowProperties.CurrencyTypeId,
                nameof(MaterialSupplierCost.Surcharge.CurrencyType), context);
        }
        
        return material;
    }

    public async Task DeleteAsync(MaterialId id, CancellationToken cancellationToken)
    {
        if (id.IsEmpty())
            return;
        var material = await GetByIdAsync(id, cancellationToken);
        if (material is null)
            return;
            
        context.Materials.Remove(material);

        //we use remove method instead of "context.Database.ExecuteSqlAsync" because it will go through db context,
        // and it has change tracker which is for sake of executing audit data. While "context.Database.ExecuteSqlAsync"
        // does not go through change tracker, so it cannot get the change tracker.
        //await context.Database.ExecuteSqlAsync($"DELETE Material WHERE Id = {id}", cancellationToken);
    }
}