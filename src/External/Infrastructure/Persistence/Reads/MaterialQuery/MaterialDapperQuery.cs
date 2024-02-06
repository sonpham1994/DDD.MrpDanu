using System.Data;
using Application.Interfaces.Reads;
using Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Queries.GetMaterialById;
using Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Queries.GetMaterials;
using Infrastructure.Persistence.Reads.MaterialQuery.Extensions;

namespace Infrastructure.Persistence.Reads.MaterialQuery;

/*
 * Cqrs: we can use domain model to retrieve data by using Select extension method. But if we need to refactor
 *  Domain model (write side), the read side is also impacted. 
 */
internal sealed class MaterialDapperQuery(IDbConnection _dbConnection) : IMaterialQuery
{
    public async Task<MaterialResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        MaterialResponse? result = null;

        var materialReadModel = await _dbConnection.GetByIdAsync(id, cancellationToken);
        if (materialReadModel is not null)
        {
            result = materialReadModel.ToResponse();
        }

        return result;
    }

    public async Task<IReadOnlyList<MaterialsResponse>> GetListAsync(CancellationToken cancellationToken)
    {
        var material = await _dbConnection.GetListAsync(cancellationToken);

        return material.ToResponse();
    }
}