using System.Data;
using Dapper;
using Application.MaterialManagement.MaterialAggregate.Commands.Models;
using Application.MaterialManagement.Shared;
using Application.MaterialManagement.TransactionalPartnerAggregate.Queries.GetTransactionalPartnerById;
using Application.MaterialManagement.TransactionalPartnerAggregate.Queries.GetTransactionalPartners;
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

    //We cant put Contact Info Value Object as parameter, but it is not necessarily. On query side, we don't need 
    //to have any restrictions on write side (DDD). So we don't need to follow all the concepts of DDD on query side
    public async Task<bool> ExistByContactInfoAsync(string email, string telNo, CancellationToken cancellationToken)
    {
        string whereClause = $"WHERE {GetExistingEmailOrTelNoWhereClause(email, telNo)}";

        bool existEmailOrTelNo = await _dbConnection.QueryFirstAsync<bool>(@$"
                    SELECT 1
                    FROM ContactPersonInformation
                    {whereClause}
                    union select 0",
            new { Email = email, TelNo = telNo });

        return existEmailOrTelNo;
    }

    public async Task<bool> ExistByContactInfoAsync(Guid id, string email, string telNo, CancellationToken cancellationToken)
    {
        string whereClause = $"WHERE Id != @Id AND ({GetExistingEmailOrTelNoWhereClause(email, telNo)})";

        bool existEmailOrTelNo = await _dbConnection.QueryFirstAsync<bool>(@$"
                    SELECT 1
                    FROM ContactPersonInformation
                    {whereClause}
                    union select 0",
            new { Id = id, Email = email, TelNo = telNo });

        return existEmailOrTelNo;
    }

    private static string GetExistingEmailOrTelNoWhereClause(string email, string telNo)
    {
        bool isEmailEmpty = true;
        string query = string.Empty;
        if (!string.IsNullOrEmpty(email))
        {
            isEmailEmpty = false;
            query += $"Email = @Email";
        }
        if (!string.IsNullOrEmpty(telNo))
        {
            string telNoWhereClause = "TelNo = @TelNo";
            if (!isEmailEmpty)
                telNoWhereClause = $"OR {telNoWhereClause}";

            query += $" {telNoWhereClause}";
        }

        return query;
    }
}