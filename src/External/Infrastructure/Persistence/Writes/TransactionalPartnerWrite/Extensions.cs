﻿using Application.MaterialManagement.MaterialAggregate.Commands.Models;
using Dapper;
using Domain.MaterialManagement.TransactionalPartnerAggregate;
using System.Data;

namespace Infrastructure.Persistence.Writes.TransactionalPartnerWrite;

internal static class Extensions
{
    public static async Task<List<SupplierIdWithCurrencyTypeId>> GetSupplierIdsWithCurrencyTypeIdByBySupplierIdsAsync(this IDbConnection dbConnection, IReadOnlyList<Guid> ids, CancellationToken cancellationToken)
    {
        var supplierTypeIds = GetSupplierTypeIds();
        var suppliers = await dbConnection
            .QueryAsync<SupplierIdWithCurrencyTypeId>("""
                SELECT Id, CurrencyTypeId
                FROM TransactionalPartner
                WHERE Id IN @Ids AND TransactionalPartnerTypeId IN @SupplierTypeIds
                """
                , new { Ids = ids, SupplierTypeIds = supplierTypeIds }
            );

        return suppliers.ToList();
    }

    private static IReadOnlyList<byte> GetSupplierTypeIds()
    {
        return TransactionalPartnerType.GetSupplierTypes().ToArray().Select(x => x.Id).ToList();
    }
}