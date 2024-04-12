using Application.Interfaces.Writes;
using Domain.SupplyAndProductionManagement.ProductionPlanning;
using Infrastructure.Persistence.Writes;

namespace Infrastructure.Persistence.SupplyAndProductionManagement.ProductPlanning.Writes;

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