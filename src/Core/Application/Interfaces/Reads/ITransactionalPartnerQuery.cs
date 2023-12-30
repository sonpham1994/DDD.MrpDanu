using Application.SupplyChainManagement.Shared;
using Application.SupplyChainManagement.TransactionalPartnerAggregate.Queries.GetTransactionalPartnerById;
using Application.SupplyChainManagement.TransactionalPartnerAggregate.Queries.GetTransactionalPartners;

namespace Application.Interfaces.Reads;

public interface ITransactionalPartnerQuery
{
    Task<IReadOnlyList<SuppliersResponse>> GetSuppliersAsync(CancellationToken cancellationToken);
    Task<IReadOnlyList<TransactionalPartnersResponse>> GetTransactionalPartnersAsync(CancellationToken cancellationToken);
    Task<TransactionalPartnerResponse?> GetTransactionalPartnerByIdAsync(Guid id, CancellationToken cancellationToken);
}