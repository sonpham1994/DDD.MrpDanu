using Application.Interfaces.Writes.TransactionalPartnerWrite;
using Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Commands.Models;
using Infrastructure.Persistence.Writes;
using Microsoft.EntityFrameworkCore;
using PersistenceExtensions = Infrastructure.Persistence.Extensions;

namespace Infrastructure.Persistence.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartners.Writes;

internal sealed class TransactionalPartnerEfReadonlyRepository(AppDbContext _context) : ITransactionalPartnerReadonlyRepository
{
    //We cant put Contact Info Value Object as parameter, but it is not necessarily. On query side, we don't need 
    //to have any restrictions on write side (DDD). So we don't need to follow all the concepts of DDD on query side
    public async Task<IReadOnlyList<SupplierIdWithCurrencyTypeId>> GetSupplierIdsWithCurrencyTypeIdBySupplierIdsAsync(IReadOnlyList<Guid> ids, CancellationToken cancellationToken)
    {
        var supplierTypeIds = PersistenceExtensions.GetSupplierTypeIds();

        //https://devblogs.microsoft.com/dotnet/announcing-ef8-preview-4/ using OPENJSON for collection instead of IN for SQL 2019 or later version
        //https://learn.microsoft.com/en-gb/ef/core/what-is-new/ef-core-8.0/whatsnew - "However, this strategy does not work well with database query caching"
        // https://learn.microsoft.com/en-us/shows/dotnetconf-2023/entity-framework-core-8-improved-json-queryable-collections-and-more
        // the reason why we want to use OPENJSON is to use cached execution plan. Please check SqlServerKnowledge/PerformanceBetweenEFCoreAndStoredProcedures or TestCompanyEntity.Benchmark/InVsExistsOpenJson
        // this issue was described here: https://github.com/dotnet/efcore/issues/13617
        
        /*
         * Problem with OPENJSON:
         *  - Performance Impact: While OPENJSON can provide benefits by maintaining query plan cache, it can be less
         *  efficient compared to a simple IN query for primitive collections. Some users have reported that the
         *  OPENJSON approach can be slower than the previous method of using IN
         *  https://github.com/dotnet/efcore/issues/32394
         *  https://github.com/dotnet/efcore/issues/30946.
         *  - Compatibility: This change might not work well with older SQL Server versions or with specific
         *  configurations like non-default collations. Some users have experienced issues with queries not leveraging
         *  indexes properly, leading to slower performance due to index scans instead of index seeks
         *  https://github.com/dotnet/efcore/issues/32147.
         *  - Opting Out: If you encounter performance issues, you can disable this new behavior by adjusting the
         *  SQL Server compatibility level or using specific EF Core configuration options. For example, setting a
         *  lower compatibility level can revert to the previous behavior without affecting other features
         *  significantly. Additionally, you can use EF Core's Expression builder API to handle specific cases
         *  differently https://github.com/dotnet/efcore/issues/32147.
         *
         * Index Scans vs Index Seeks
         * Index Seeks:
         *  - Definition: An index seek is an efficient method of retrieving data from an index. The database engine
         *  uses the index's structure to quickly locate the rows that satisfy the query's condition. This method
         *  directly navigates to the rows that match the criteria, using the index keys.
         *  - Performance: Index seeks are generally faster because they minimize the number of rows that need to be
         *  scanned, reducing the amount of I/O operations. They are highly efficient for queries that target a
         *  specific subset of rows based on indexed columns.
         *  - Usage Scenario: Index seeks are typically used when the query condition is selective, meaning it filters
         *  down to a small number of rows, such as looking up a record by a unique identifier.
         * Index Scans:
         *  - Definition: An index scan involves reading the entire index to find the rows that match the query
         *  condition. The database engine scans all the entries in the index, which can be much slower than a seek,
         *  especially for large tables.
         *  - Performance: Index scans are less efficient because they require reading a larger number of rows. This
         *  can lead to increased I/O operations and longer query execution times.
         *  - Usage Scenario: Index scans occur when the query condition is less selective, meaning it targets a large
         *  number of rows, or when no suitable index exists to optimize the search.
         * Practical Example:
         *  Consider a table Customers with an index on the LastName column:
         *  - Index Seek Example: A query like SELECT * FROM Customers WHERE LastName = 'Smith' would use an index seek
         *  if the LastName column is indexed. This is because the query is looking for a specific value, allowing the
         *  optimizer to directly access the relevant rows.
         *  - Index Scan Example: A query like SELECT * FROM Customers WHERE LastName LIKE 'S%' might result in an
         *  index scan, especially if there are many last names starting with 'S'. The query optimizer might need to
         *  scan a large portion of the index to retrieve all matching rows.
         * Impact in EF Core 8:
         *  In EF Core 8, the change to use OPENJSON for handling Contains queries can lead to situations where the
         *  SQL Server optimizer might not efficiently use indexes. Specifically, queries that previously benefited
         *  from index seeks might now result in index scans due to the way OPENJSON processes the data, particularly
         *  with non-default collations or large datasets.
         */
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
