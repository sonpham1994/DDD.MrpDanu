using Domain.MaterialManagement.TransactionalPartnerAggregate;
using Domain.SharedKernel.ValueObjects;

namespace Application.Interfaces.Repositories;

public interface ITransactionalPartnerRepository
{
    ValueTask<TransactionalPartner?> GetByIdAsync(TransactionalPartnerId id, CancellationToken cancellationToken);
    ValueTask<IReadOnlyList<TransactionalPartner>> GetByIdsAsync(IReadOnlyList<TransactionalPartnerId> ids, CancellationToken cancellationToken);
    ValueTask<IReadOnlyList<TransactionalPartner>> GetSuppliersByIdsAsync(IReadOnlyList<SupplierId> ids, CancellationToken cancellationToken);
    void Save(TransactionalPartner transactionalPartner);
    Task DeleteAsync(TransactionalPartnerId id, CancellationToken cancellationToken);
}