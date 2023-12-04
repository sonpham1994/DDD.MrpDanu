using Application.MaterialManagement.MaterialAggregate.Queries.GetMaterialById;
using Application.MaterialManagement.MaterialAggregate.Queries.GetMaterials;

namespace Application.Interfaces.Queries;

public interface IMaterialQuery
{
    Task<MaterialResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<MaterialsResponse>> GetListAsync(CancellationToken cancellationToken);
}