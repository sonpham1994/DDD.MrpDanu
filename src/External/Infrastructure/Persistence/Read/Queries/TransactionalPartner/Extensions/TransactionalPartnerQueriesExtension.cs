using System.Data;
using Dapper;
using Domain.MaterialManagement.TransactionalPartnerAggregate;
using Infrastructure.Persistence.Read.Models;

namespace Infrastructure.Persistence.Read.Queries.TransactionalPartner.Extensions;

// the reason why we put the query in extensions class is that, we can reuse the projection from another place,
// reduce duplication projection. So other methods in TransactionalPartnerQuery can reuse this projection to
// do their own business
// please check https://www.youtube.com/watch?v=bnTxWV99qdE&t=562s&ab_channel=MilanJovanovi%C4%87
internal static class TransactionalPartnerQueriesExtension
{
    public static async Task<List<TransactionalPartnersReadModel>> GetTransactionalPartnersAsync(this IDbConnection dbConnection, CancellationToken cancellationToken)
    {
        var transactionalPartners = await dbConnection
            .QueryAsync<TransactionalPartnersReadModel>(
                @"SELECT Id, Name, TaxNo, Website, CurrencyTypeId, TransactionalPartnerTypeId
                FROM TransactionalPartner transactionalPartner"
                , cancellationToken
            );

        return transactionalPartners.ToList();
    }

    public static async Task<TransactionalPartnerReadModel?> GetTransactionalPartnerByIdAsync(this IDbConnection dbConnection, Guid id,
        CancellationToken cancellationToken)
    {
        var transactionalPartner = await dbConnection
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

        return transactionalPartner;
    }

    public static async Task<List<SuppliersReadModel>> GetSuppliersAsync(this IDbConnection dbConnection, CancellationToken cancellationToken)
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
        var supplierTypeIds = GetSupplierTypeIds();

        var suppliers = await dbConnection
            .QueryAsync<SuppliersReadModel>(
                @"SELECT transactionalPartner.Id, transactionalPartner.Name, transactionalPartner.CurrencyTypeId
                FROM TransactionalPartner transactionalPartner
                WHERE transactionalPartner.TransactionalPartnerTypeId IN @SupplierTypeIds
                "
                , new { supplierTypeIds }
            );

        return suppliers.ToList();
    }
    
    private static IReadOnlyList<byte> GetSupplierTypeIds()
    {
        return TransactionalPartnerType.GetSupplierTypes().ToArray().Select(x => x.Id).ToList();
    }
}