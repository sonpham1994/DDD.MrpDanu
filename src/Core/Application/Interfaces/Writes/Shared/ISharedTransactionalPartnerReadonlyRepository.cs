using Domain.Services.GetListSupplierIdAndCurrencyTypeIdServices;

namespace Application.Interfaces.Writes.Shared;

public interface ISharedTransactionalPartnerReadonlyRepository
{
    Task<IReadOnlyList<SupplierIdWithCurrencyTypeId>> GetSupplierIdsWithCurrencyTypeIdBySupplierIdsAsync(IReadOnlyList<Guid> ids, CancellationToken cancellationToken);
}