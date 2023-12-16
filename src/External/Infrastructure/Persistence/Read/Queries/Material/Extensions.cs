using System.Data;
using Dapper;
using Infrastructure.Persistence.Read.Models;

namespace Infrastructure.Persistence.Read.Queries.Material;

// the reason why we put the query in extensions class is that, we can reuse the projection from another place,
// reduce duplication projection. So other methods in TransactionalPartnerQuery can reuse this projection to
// do their own business
// please check https://www.youtube.com/watch?v=bnTxWV99qdE&t=562s&ab_channel=MilanJovanovi%C4%87
internal static class Extensions
{
    public static async Task<MaterialReadModel?> GetByIdAsync(this IDbConnection dbConnection,
        Guid id,
        CancellationToken cancellationToken)
    {
        string sql = @"SELECT Id, Code, Name, ColorCode, Unit, 
                         Varian, Weight, Width, RegionalMarketId, MaterialTypeId
                         FROM Material
                         WHERE Id = @Id;
                        
                         SELECT materialCost.Id, materialCost.Price, materialCost.Surcharge, materialCost.MinQuantity,
                         supplier.Id as SupplierId, supplier.Name as SupplierName, supplier.CurrencyTypeId
                         FROM MaterialCostManagement materialCost
                         JOIN TransactionalPartner supplier on supplier.Id = materialCost.TransactionalPartnerId
                         WHERE materialCost.MaterialId = @Id;";

        using var multiQuery = await dbConnection.QueryMultipleAsync(sql, new { id });
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
        var material = await dbConnection
            .QueryAsync<MaterialsReadModel>(
                @"SELECT material.Id, material.Code, material.Name, material.ColorCode, material.Unit, 
                    material.Varian, material.Weight, material.Width,
                    material.MaterialTypeId, material.RegionalMarketId
                    FROM Material material", 
                cancellationToken);

        return material.ToList();
    }
}