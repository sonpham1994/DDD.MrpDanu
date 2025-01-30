using Application.Interfaces.Writes.TransactionalPartnerWrite;
using Domain.Services.GetListSupplierIdAndCurrencyTypeIdServices;
using Infrastructure.Persistence.Writes;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartners.Writes;

internal sealed class TransactionalPartnerEfReadonlyRepository(AppDbContext context) : ITransactionalPartnerReadonlyRepository
{

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
        bool existContact = await context
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
        bool existContact = await context
                .Database
                .SqlQuery<bool>(@$"
                    SELECT 1 AS ExistsValue
                    FROM ContactPersonInformation
                    WHERE Id != {id} AND ((Email != '' AND Email = {email}) OR (TelNo != '' AND TelNo = {telNo}))
                    ")
                .AnyAsync(cancellationToken);

        return existContact;
    }

    public async Task<bool> ExistTransactionalPartnerByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        bool exists = await context
            .Database
            .SqlQuery<bool>(@$"
                    SELECT 1 AS ExistsValue
                    FROM TransactionalPartner
                    WHERE Id = {id}
                    ")
            .AnyAsync(cancellationToken);

        return exists;
    }
}
