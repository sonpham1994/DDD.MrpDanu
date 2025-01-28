using Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Commands.Models;

namespace Application.Interfaces.Writes.TransactionalPartnerWrite;

public interface ITransactionalPartnerReadonlyRepository : IReadonlyRepository
{
    Task<IReadOnlyList<SupplierIdWithCurrencyTypeId>> GetSupplierIdsWithCurrencyTypeIdBySupplierIdsAsync(IReadOnlyList<Guid> ids, CancellationToken cancellationToken);
    Task<bool> ExistByContactInfoAsync(string email, string telNo, CancellationToken cancellationToken);
    Task<bool> ExistByContactInfoAsync(Guid id, string email, string telNo, CancellationToken cancellationToken);
    Task<bool> ExistTransactionalPartnerByIdAsync(Guid id, CancellationToken cancellationToken);
}
