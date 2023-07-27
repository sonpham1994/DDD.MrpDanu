using Application.Interfaces.Repositories;
using Domain.MaterialManagement.MaterialAggregate;
using Microsoft.EntityFrameworkCore;

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
        
        return material;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await context.Database.ExecuteSqlAsync($"DELETE Material WHERE Id = {id}", cancellationToken);
    }
}