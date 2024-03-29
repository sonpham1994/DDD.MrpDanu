using Application.Interfaces.Writes;
using Domain.SupplyAndProductionManagement.ProductionPlanning;

namespace Infrastructure.Persistence.Writes.BoMWrite;

internal sealed class BoMEfRepository : BaseGenericEfRepository<BoM, BoMId>, IBoMRepository
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