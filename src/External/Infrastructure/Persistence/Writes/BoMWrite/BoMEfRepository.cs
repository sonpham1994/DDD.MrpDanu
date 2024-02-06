using Domain.SupplyAndProductionManagement.ProductionPlanning;

namespace Infrastructure.Persistence.Writes.BoMWrite;

internal sealed class BoMEfRepository : BaseGenericEfRepository<BoM, BoMId>
{
    public BoMEfRepository(AppDbContext context) : base(context)
    {
    }
    
    public async Task SaveAsync(BoM bom)
    {
        await dbSet.AddAsync(bom);
        bom.IncreaseRevision();
    }
}