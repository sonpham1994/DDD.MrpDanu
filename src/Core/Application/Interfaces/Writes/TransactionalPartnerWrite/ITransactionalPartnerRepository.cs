using Domain.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate;
using SharedKernel.ValueObjects;

namespace Application.Interfaces.Writes.TransactionalPartnerWrite;

public interface ITransactionalPartnerRepository : IRepository
{
    ValueTask<TransactionalPartner?> GetByIdAsync(TransactionalPartnerId id, CancellationToken cancellationToken);
    void Save(TransactionalPartner transactionalPartner);
    Task DeleteAsync(TransactionalPartnerId id, CancellationToken cancellationToken);
}