using Application.Interfaces.Repositories;
using Domain.MaterialManagement.TransactionalPartnerAggregate;
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

        return transactionalPartner;
    }
    
    public async ValueTask<IReadOnlyList<TransactionalPartner>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
    {
        IReadOnlyList<TransactionalPartner> transactionalPartners = Array.Empty<TransactionalPartner>();
        var transactionalPartnerIds = ids.Where(x => x != Guid.Empty).Distinct().ToList();
        
        if (!transactionalPartnerIds.Any())
            return transactionalPartners;

        transactionalPartners = await dbSet.Where(x => transactionalPartnerIds.Contains(x.Id)).ToListAsync(cancellationToken);

        return transactionalPartners;
    }
    
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await context.Database.ExecuteSqlAsync($"DELETE TransactionalPartner WHERE Id = {id}", cancellationToken);
    }
}