using Application.MaterialManagement.MaterialAggregate.Commands.Models;
using Application.MaterialManagement.Shared;
using Application.MaterialManagement.TransactionalPartnerAggregate.Queries.GetTransactionalPartnerById;
using Application.MaterialManagement.TransactionalPartnerAggregate.Queries.GetTransactionalPartners;

namespace Application.Interfaces.Queries;

public interface ITransactionalPartnerQuery
{
    Task<IReadOnlyList<SuppliersResponse>> GetSuppliersAsync(CancellationToken cancellationToken);
    Task<IReadOnlyList<SupplierIdWithCurrencyTypeId>> GetSupplierIdsWithCurrencyTypeIdBySupplierIdsAsync(IReadOnlyList<Guid> ids, CancellationToken cancellationToken);
    Task<IReadOnlyList<TransactionalPartnersResponse>> GetTransactionalPartnersAsync(CancellationToken cancellationToken);
    Task<TransactionalPartnerResponse?> GetTransactionalPartnerByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> ExistByContactInfoAsync(string email, string telNo, CancellationToken cancellationToken);
    Task<bool> ExistByContactInfoAsync(Guid id, string email, string telNo, CancellationToken cancellationToken);
}