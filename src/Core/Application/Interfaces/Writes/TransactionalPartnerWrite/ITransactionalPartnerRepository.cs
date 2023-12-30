using Domain.SupplyChainManagement.TransactionalPartnerAggregate;
using Domain.SharedKernel.ValueObjects;

namespace Application.Interfaces.Writes.TransactionalPartnerWrite;

public interface ITransactionalPartnerRepository
{
    ValueTask<TransactionalPartner?> GetByIdAsync(TransactionalPartnerId id, CancellationToken cancellationToken);
    void Save(TransactionalPartner transactionalPartner);
    Task DeleteAsync(TransactionalPartnerId id, CancellationToken cancellationToken);
}