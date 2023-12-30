using System.Data;
using Dapper;
using Application.SupplyChainManagement.MaterialAggregate.Commands.Models;
using Application.SupplyChainManagement.Shared;
using Application.SupplyChainManagement.TransactionalPartnerAggregate.Queries.GetTransactionalPartnerById;
using Application.SupplyChainManagement.TransactionalPartnerAggregate.Queries.GetTransactionalPartners;
using Infrastructure.Persistence.Read.TransactionalPartnerQuery.Extensions;
using Application.Interfaces.Reads;

namespace Infrastructure.Persistence.Read.TransactionalPartnerQuery;

internal sealed class TransactionalPartnerDapperQuery : ITransactionalPartnerQuery
{
    private readonly IDbConnection _dbConnection;
    public TransactionalPartnerDapperQuery(IDbConnection dbConnection)
        => _dbConnection = dbConnection;
    
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