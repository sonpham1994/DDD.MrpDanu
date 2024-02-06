using Application.Interfaces.Writes.TransactionalPartnerWrite;
using Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Commands.Models;
using Microsoft.EntityFrameworkCore;
using PersistenceExtensions = Infrastructure.Persistence.Extensions;

namespace Infrastructure.Persistence.Writes.TransactionalPartnerWrite;

internal sealed class TransactionalPartnerEFQueryForWrite(AppDbContext _context) : ITransactionalPartnerQueryForWrite
{
    //We cant put Contact Info Value Object as parameter, but it is not necessarily. On query side, we don't need 
    //to have any restrictions on write side (DDD). So we don't need to follow all the concepts of DDD on query side
    public async Task<IReadOnlyList<SupplierIdWithCurrencyTypeId>> GetSupplierIdsWithCurrencyTypeIdBySupplierIdsAsync(IReadOnlyList<Guid> ids, CancellationToken cancellationToken)
    {
        var supplierTypeIds = PersistenceExtensions.GetSupplierTypeIds();

        //https://devblogs.microsoft.com/dotnet/announcing-ef8-preview-4/ using OPENJSON for collection instead of IN for SQL 2019 or later version
        //https://learn.microsoft.com/en-gb/ef/core/what-is-new/ef-core-8.0/whatsnew - "However, this strategy does not work well with database query caching"
        // the reason why we want to use OPENJSON is to use cached execution plan. Please check SqlServerKnowledge/PerformanceBetweenEFCoreAndStoredProcedures or TestCompanyEntity.Benchmark/InVsExistsOpenJson

        var suppliers = await _context
            .Database
            .SqlQuery<SupplierIdWithCurrencyTypeId>(@$"
                SELECT Id, CurrencyTypeId
                FROM TransactionalPartner as transactionalPartner
                WHERE EXISTS (
                    SELECT 1
                    FROM OPENJSON({ids}) WITH ([value] uniqueidentifier '$') AS [ids]
                    WHERE [ids].[value] = transactionalPartner.Id
                )
                AND EXISTS (
                    SELECT 1
                    FROM OPENJSON({supplierTypeIds}) WITH ([value] tinyint '$') AS [supplierTypeIds]
                    WHERE [supplierTypeIds].[value] = transactionalPartner.TransactionalPartnerTypeId
                )
                ")
            .ToListAsync(cancellationToken);

        return suppliers;
    }

    
    // public async Task<bool> ExistByContactInfoAsync(string email, string telNo, CancellationToken cancellationToken)
    // {
    //     string whereClause = $"WHERE {GetExistingEmailOrTelNoWhereClause(email, telNo)}";

    //     bool existEmailOrTelNo = await _dbConnection.QueryFirstAsync<bool>(@$"
    //                 SELECT 1
    //                 FROM ContactPersonInformation
    //                 {whereClause}
    //                 union select 0",
    //         new { Email = email, TelNo = telNo });

    //     return existEmailOrTelNo;
    // }

    public async Task<bool> ExistByContactInfoAsync(string email, string telNo, CancellationToken cancellationToken)
    {
        // dont change sql query structure for the sake of cached execution plan
        bool existContact = await _context
                .Database
                .SqlQuery<bool>(@$"
                    SELECT 1 AS ExistsValue
                    FROM ContactPersonInformation
                    WHERE (Email != '' AND Email = {email}) OR (TelNo != '' AND TelNo = {telNo})
                    ")
                .AnyAsync(cancellationToken);

        return existContact;
    }

    public async Task<bool> ExistByContactInfoAsync(Guid id, string email, string telNo, CancellationToken cancellationToken)
    {
        // dont change sql query structure for the sake of cached execution plan
        /*
        * SELECT CASE
    WHEN EXISTS (
        SELECT 1
        FROM (

                                SELECT 1 AS ExistsValue
                                FROM ContactPersonInformation
                                WHERE Id != @p0 AND (Email = @p1 OR TelNo = @p2)
                                UNION 
                                SELECT 0 AS ExistsValue
        ) AS [t]) THEN CAST(1 AS bit)
    ELSE CAST(0 AS bit)
END)
        */
        bool existContact = await _context
                .Database
                .SqlQuery<bool>(@$"
                    SELECT 1 AS ExistsValue
                    FROM ContactPersonInformation
                    WHERE Id != {id} AND ((Email != '' AND Email = {email}) OR (TelNo != '' AND TelNo = {telNo}))
                    ")
                .AnyAsync(cancellationToken);
       
        return existContact;
    }
}
