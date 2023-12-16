using System.Data;
using Dapper;
using Application.Interfaces.Queries;
using Application.MaterialManagement.MaterialAggregate.Queries.GetMaterialById;
using Application.MaterialManagement.MaterialAggregate.Queries.GetMaterials;
using Infrastructure.Persistence.Read.Extensions;
using Infrastructure.Persistence.Read.Models;

namespace Infrastructure.Persistence.Read.Queries.Material;

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