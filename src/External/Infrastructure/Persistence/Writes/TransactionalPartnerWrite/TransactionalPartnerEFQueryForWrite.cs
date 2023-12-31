﻿using Application.Interfaces.Writes.TransactionalPartnerWrite;
using Application.SupplyChainManagement.MaterialAggregate.Commands.Models;
using System.Data;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Writes.TransactionalPartnerWrite;

internal sealed class TransactionalPartnerEFQueryForWrite(AppDbContext _context, IDbConnection _dbConnection) : ITransactionalPartnerQueryForWrite
{
    public async Task<IReadOnlyList<SupplierIdWithCurrencyTypeId>> GetSupplierIdsWithCurrencyTypeIdBySupplierIdsAsync(IReadOnlyList<Guid> ids, CancellationToken cancellationToken)
    {
        var supplierTypeIds = Extensions.GetSupplierTypeIds();
        var supplierIdsParam = "'" + string.Join(", '", ids) + "'";
        var supplierTypeIdsParam = string.Join(", ", supplierTypeIds);
        
        var suppliers = await _context.Database.SqlQuery<SupplierIdWithCurrencyTypeId>(@$"
                SELECT Id, CurrencyTypeId
                FROM TransactionalPartner
                WHERE Id IN ({supplierIdsParam}) AND TransactionalPartnerTypeId IN ({supplierTypeIdsParam})
            ")
        .ToListAsync(cancellationToken);
        //var suppliers = await _dbConnection.GetSupplierIdsWithCurrencyTypeIdByBySupplierIdsAsync(ids, cancellationToken);

        return suppliers;
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
