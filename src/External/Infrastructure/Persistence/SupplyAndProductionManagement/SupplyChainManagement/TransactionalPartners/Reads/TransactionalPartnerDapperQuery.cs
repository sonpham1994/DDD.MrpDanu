using System.Data;
using Application.SupplyAndProductionManagement.SupplyChainManagement.Shared;
using Application.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate.Queries.GetTransactionalPartnerById;
using Application.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate.Queries.GetTransactionalPartners;
using Application.Interfaces.Reads;
using Infrastructure.Persistence.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartners.Reads.Extensions;

namespace Infrastructure.Persistence.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartners.Reads;

internal sealed class TransactionalPartnerDapperQuery(IDbConnection _dbConnection) : ITransactionalPartnerQuery
{
    public async Task<IReadOnlyList<SuppliersResponse>> GetSuppliersAsync(CancellationToken cancellationToken)
    {
        var suppliers = await _dbConnection.GetSuppliersAsync(cancellationToken);

        return suppliers.ToResponse();
    }

    public async Task<IReadOnlyList<TransactionalPartnersResponse>> GetTransactionalPartnersAsync(CancellationToken cancellationToken)
    {
        var transactionalPartners = await _dbConnection.GetTransactionalPartnersAsync(cancellationToken);

        return transactionalPartners.AsTransactionalPartnersResponse();
    }

    public async Task<TransactionalPartnerResponse?> GetTransactionalPartnerByIdAsync(Guid id,
        CancellationToken cancellationToken)
    {
        var transactionalPartner = await _dbConnection.GetTransactionalPartnerByIdAsync(id, cancellationToken);

        return transactionalPartner.ToResponse();
    }
}