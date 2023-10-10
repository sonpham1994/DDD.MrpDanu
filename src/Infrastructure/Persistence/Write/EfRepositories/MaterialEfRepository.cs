using Application.Interfaces.Repositories;
using Domain.MaterialManagement.MaterialAggregate;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Persistence.Write.EfRepositories;

internal sealed class MaterialEfRepository : BaseEfRepository<Material>, IMaterialRepository
{
    public MaterialEfRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Material?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Material? material = null;
        
        if (id == Guid.Empty)
            return material;

        material = await base.GetByIdAsync(id, cancellationToken);
        if (material is null)
            return material;

        await dbSet.Entry(material).Collection(x => x.MaterialCostManagements).LoadAsync(cancellationToken);

        //material = await dbSet
        //    .Include(x => x.MaterialType)
        //    .Include(x => x.RegionalMarket)
        //    .Include(x => x.MaterialCostManagements)
        //    .ThenInclude(x => x.TransactionalPartner).ThenInclude(x => x.TransactionalPartnerType)
        //    .Include(x => x.MaterialCostManagements).ThenInclude(x => x.TransactionalPartner).ThenInclude(x => x.CurrencyType)
        //    .FirstOrDefaultAsync(x => x.Id == id);

        /*
         * if you want to reduce round trip from backend to database, you may use reflection for enumeration data type, due to the
         fact that the enumeration data type store own data in memory, hence we don't need to make a call to database, we may
         only the data that we store in memory. In fact, EF Core also use reflection to bind data to your entity. By doing this
         we reduce the round-trip but the use of reflection remains intact. And another benefit is that, we reduce the memory
         usage when we retrieve the enumeration data type from database, which increase additional enumeration object. We just
         use the enumeration object that store in database.
         Please check EnumerationLoadingBenchmark in Benchmark.Infrastructure
         */
        
        var materialTypeId = context.Entry(material).Property<byte>(ShadowProperties.MaterialTypeId).CurrentValue;
        var regionalMarketId = context.Entry(material).Property<byte>(ShadowProperties.RegionalMarketId).CurrentValue;

        typeof(Material).GetProperty(nameof(Material.MaterialType))!.SetValue(material, MaterialType.FromId(materialTypeId).Value, null);
        typeof(Material).GetProperty(nameof(Material.RegionalMarket))!.SetValue(material, RegionalMarket.FromId(regionalMarketId).Value, null);

        return material;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await context.Database.ExecuteSqlAsync($"DELETE Material WHERE Id = {id}", cancellationToken);
    }
}