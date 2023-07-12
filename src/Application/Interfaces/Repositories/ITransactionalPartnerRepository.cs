using Domain.MaterialManagement.TransactionalPartnerAggregate;

namespace Application.Interfaces.Repositories;

public interface ITransactionalPartnerRepository
{
    ValueTask<TransactionalPartner?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    ValueTask<IReadOnlyList<TransactionalPartner>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);
    void Save(TransactionalPartner transactionalPartner);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}