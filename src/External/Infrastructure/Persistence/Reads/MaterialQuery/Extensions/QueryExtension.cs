using System.Data;
using Dapper;
using Infrastructure.Persistence.Reads.MaterialQuery.Models;

namespace Infrastructure.Persistence.Reads.MaterialQuery.Extensions;

// the reason why we put the query in extensions class is that, we can reuse the projection from another place,
// reduce duplication projection. So other methods in TransactionalPartnerQuery can reuse this projection to
// do their own business
// please check https://www.youtube.com/watch?v=bnTxWV99qdE&t=562s&ab_channel=MilanJovanovi%C4%87
internal static class QueryExtension
{
    public static async Task<MaterialReadModel?> GetByIdAsync(this IDbConnection dbConnection,
        Guid id,
        CancellationToken cancellationToken)
    {
        //using multiple query to avoid cartesian explosion
        string sql = """
            SELECT Id, Code, Name, ColorCode, Unit, 
            Varian, Weight, Width, RegionalMarketId, MaterialTypeId
            FROM Material
            WHERE Id = @Id;
            
            SELECT materialSupplierCost.Id, materialSupplierCost.Price, materialSupplierCost.Surcharge, materialSupplierCost.MinQuantity,
            supplier.Id as SupplierId, supplier.Name as SupplierName, supplier.CurrencyTypeId
            FROM MaterialSupplierCost materialSupplierCost
            JOIN TransactionalPartner supplier on supplier.Id = materialSupplierCost.SupplierId
            WHERE materialSupplierCost.MaterialId = @Id;
            """;

        await using var multiQuery = await dbConnection.QueryMultipleAsync(sql, new { id });
        var materialReadModel = await multiQuery.ReadFirstOrDefaultAsync<MaterialReadModel>();

        if (materialReadModel is not null)
        {
            var materialCostReadModel = await multiQuery.ReadAsync<MaterialCostReadModel>();
            materialReadModel.MaterialCosts = materialCostReadModel.ToList();
        }

        return materialReadModel;
    }

    public static async Task<List<MaterialsReadModel>> GetListAsync(this IDbConnection dbConnection, CancellationToken cancellationToken)
    {
        string sql = """
                    SELECT Id, Code, Name, ColorCode, Unit, 
                    Varian, Weight, Width, MaterialTypeId, RegionalMarketId
                    FROM Material
                    """;
        var material = await dbConnection
            .QueryAsync<MaterialsReadModel>(sql,
                cancellationToken);

        return material.ToList();
    }
}