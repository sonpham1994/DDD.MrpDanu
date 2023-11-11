using System.Data;
using Dapper;
using Application.Interfaces.Queries;
using Application.MaterialManagement.Shared;
using Application.MaterialManagement.TransactionalPartnerAggregate.Queries.GetTransactionalPartnerById;
using Application.MaterialManagement.TransactionalPartnerAggregate.Queries.GetTransactionalPartners;
using Infrastructure.Persistence.Read.Extensions;
using Infrastructure.Persistence.Read.Models;

namespace Infrastructure.Persistence.Read.Queries;

internal sealed class TransactionalPartnerDapperQuery : ITransactionalPartnerQuery
{
    private readonly IDbConnection _dbConnection;
    public TransactionalPartnerDapperQuery(IDbConnection dbConnection)
        => _dbConnection = dbConnection;
    
    public async Task<IReadOnlyList<SuppliersResponse>> GetSuppliersAsync(CancellationToken cancellationToken)
    {
        /*
         * https://enterprisecraftsmanship.com/posts/cqrs-vs-specification-pattern/
         * ...
         * The guideline here is this: loose coupling wins in the vast majority of cases.
         * ...
         * However, it large systems, you should almost always choose the loose coupling over preventing the domain
         *  knowledge duplication between the reads and writes. The freedom to choose the most appropriate solution
         *  for the problem at hand trumps the DRY principle.
         *
         * if we use TransactionalPartnerType.GetSupplierTypes() to get suppliers, we reduce domain knowledge duplication
         *  , but have a highly coupling between read/write side, so we move it to MaterialManagementMapping
         */
        var supplierTypeIds = MaterialManagementExtension.GetSupplierTypeIds();

        var suppliers = await _dbConnection
            .QueryAsync<SuppliersReadModel>(
            @"SELECT transactionalPartner.Id, transactionalPartner.Name, transactionalPartner.CurrencyTypeId
                FROM TransactionalPartner transactionalPartner
                WHERE transactionalPartner.TransactionalPartnerTypeId IN @SupplierTypeIds
                "
            , new { supplierTypeIds }
            );

        return suppliers.ToSuppliersResponse();
    }
    
    public async Task<IReadOnlyList<TransactionalPartnersResponse>> GetTransactionalPartnersAsync(CancellationToken cancellationToken)
    {
        var transactionalPartners = await _dbConnection
            .QueryAsync<TransactionalPartnersReadModel>(
                @"SELECT Id, Name, TaxNo, Website, CurrencyTypeId, TransactionalPartnerTypeId
                FROM TransactionalPartner transactionalPartner"
                , cancellationToken
            );
        
        return transactionalPartners.AsTransactionalPartnersResponse();
    }

    public async Task<TransactionalPartnerResponse?> GetTransactionalPartnerByIdAsync(Guid id,
        CancellationToken cancellationToken)
    {
        var transactionalPartner = await _dbConnection
            .QueryFirstOrDefaultAsync<TransactionalPartnerReadModel>(
                @"SELECT transactionalPartner.Id, transactionalPartner.Name, transactionalPartner.TaxNo, 
                transactionalPartner.Website, transactionalPartner.LocationTypeId, transactionalPartner.TransactionalPartnerTypeId,
                transactionalPartner.CurrencyTypeId, transactionalPartner.Address_City, transactionalPartner.Address_District,
                transactionalPartner.Address_Street, transactionalPartner.Address_Ward, transactionalPartner.Address_ZipCode,
                transactionalPartner.CountryId,
                contactPersonInformation.Name as ContactPersonName, contactPersonInformation.Email, contactPersonInformation.TelNo
                FROM TransactionalPartner transactionalPartner
                JOIN ContactPersonInformation contactPersonInformation on contactPersonInformation.Id = transactionalPartner.Id
                WHERE transactionalPartner.Id = @Id"
                , new { id }
            );

        return transactionalPartner.ToTransactionalPartnerResponse();
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