using System.Data;
using Dapper;
using Application.Interfaces.Queries;
using Application.MaterialManagement.MaterialAggregate.Queries.GetMaterialById;
using Application.MaterialManagement.MaterialAggregate.Queries.GetMaterials;
using Infrastructure.Persistence.Read.Extensions;
using Infrastructure.Persistence.Read.Models;

namespace Infrastructure.Persistence.Read.Queries;

/*
 * Cqrs: we can use domain model to retrieve data by using Select extension method. But if we need to refactor
 *  Domain model (write side), the read side is also impacted. 
 */
internal sealed class MaterialDapperQuery : IMaterialQuery
{
    private readonly IDbConnection _dbConnection;
    public MaterialDapperQuery(IDbConnection dbConnection)
        => _dbConnection = dbConnection;

    public async Task<MaterialResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        MaterialResponse? result = null;
        string sql = @"SELECT Id, Code, Name, ColorCode, Unit, 
                         Varian, Weight, Width, RegionalMarketId, MaterialTypeId
                         FROM Material
                         WHERE Id = @Id;
                        
                         SELECT materialCost.Id, materialCost.Price, materialCost.Surcharge, materialCost.MinQuantity,
                         supplier.Id as SupplierId, supplier.Name as SupplierName, supplier.CurrencyTypeId
                         FROM MaterialCostManagement materialCost
                         JOIN TransactionalPartner supplier on supplier.Id = materialCost.TransactionalPartnerId
                         WHERE materialCost.MaterialId = @Id;";
        
        using var multiQuery = await _dbConnection.QueryMultipleAsync(sql, new { id });
        var materialReadModel = await multiQuery.ReadFirstOrDefaultAsync<MaterialReadModel>();

        if (materialReadModel is not null)
        {
            var materialCostReadModel = await multiQuery.ReadAsync<MaterialCostReadModel>();
            result = materialReadModel.ToMaterialResponse(materialCostReadModel.ToMaterialCostManagementResponse());
        }

        return result;
    }

    public async Task<IReadOnlyList<MaterialsResponse>> GetListAsync(CancellationToken cancellationToken)
    {
        var material = await _dbConnection
            .QueryAsync<MaterialsReadModel>(
                @"SELECT material.Id, material.Code, material.Name, material.ColorCode, material.Unit, 
                    material.Varian, material.Weight, material.Width,
                    material.MaterialTypeId, material.RegionalMarketId
                    FROM Material material", 
                cancellationToken);

        return material.ToMaterialsResponse();
    }
}