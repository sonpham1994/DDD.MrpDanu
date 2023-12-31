using Domain.ProductionPlanning;

namespace Infrastructure.Persistence.Writes.BoMWrite;

internal class BoMEfRepository : BaseGenericEfRepository<BoM, BoMId>
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