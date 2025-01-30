using Domain.Services.GetListSupplierIdAndCurrencyTypeIdServices;

namespace Application.Interfaces.Writes.TransactionalPartnerWrite;

public interface ITransactionalPartnerReadonlyRepository : IReadonlyRepository
{
    Task<bool> ExistByContactInfoAsync(string email, string telNo, CancellationToken cancellationToken);
    Task<bool> ExistByContactInfoAsync(Guid id, string email, string telNo, CancellationToken cancellationToken);
    Task<bool> ExistTransactionalPartnerByIdAsync(Guid id, CancellationToken cancellationToken);
}
