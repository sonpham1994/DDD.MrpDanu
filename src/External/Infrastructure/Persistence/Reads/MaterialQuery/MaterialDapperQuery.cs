using System.Data;
using Application.Interfaces.Reads;
using Application.SupplyChainManagement.MaterialAggregate.Queries.GetMaterialById;
using Application.SupplyChainManagement.MaterialAggregate.Queries.GetMaterials;
using Domain.SupplyChainManagement.MaterialAggregate.Services.UniqueMaterialCodeServices;
using Infrastructure.Persistence.Read.MaterialQuery.Extensions;

namespace Infrastructure.Persistence.Read.MaterialQuery;

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