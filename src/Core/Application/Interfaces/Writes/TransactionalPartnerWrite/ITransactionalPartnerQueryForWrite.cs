using Application.MaterialManagement.MaterialAggregate.Commands.Models;

namespace Application.Interfaces.Writes.TransactionalPartnerWrite;

public interface ITransactionalPartnerQueryForWrite
{
    Task<IReadOnlyList<SupplierIdWithCurrencyTypeId>> GetSupplierIdsWithCurrencyTypeIdBySupplierIdsAsync(IReadOnlyList<Guid> ids, CancellationToken cancellationToken);
    Task<bool> ExistByContactInfoAsync(string email, string telNo, CancellationToken cancellationToken);
    Task<bool> ExistByContactInfoAsync(Guid id, string email, string telNo, CancellationToken cancellationToken);
}
