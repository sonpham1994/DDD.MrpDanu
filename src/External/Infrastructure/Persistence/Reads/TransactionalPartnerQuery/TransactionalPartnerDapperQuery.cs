using System.Data;
using Dapper;
using Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Commands.Models;
using Application.SupplyAndProductionManagement.SupplyChainManagement.Shared;
using Application.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate.Queries.GetTransactionalPartnerById;
using Application.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate.Queries.GetTransactionalPartners;
using Infrastructure.Persistence.Reads.TransactionalPartnerQuery.Extensions;
using Application.Interfaces.Reads;

namespace Infrastructure.Persistence.Reads.TransactionalPartnerQuery;

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