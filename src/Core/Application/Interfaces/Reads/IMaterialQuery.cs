using Application.MaterialManagement.MaterialAggregate.Queries.GetMaterialById;
using Application.MaterialManagement.MaterialAggregate.Queries.GetMaterials;
using Domain.MaterialManagement.MaterialAggregate.Services.UniqueMaterialCodeServices;

namespace Application.Interfaces.Reads;

public interface IMaterialQuery
{
    Task<MaterialResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<MaterialIdWithCode>> GetByCodeAsync(string code, CancellationToken cancellationToken);
    Task<IReadOnlyList<MaterialsResponse>> GetListAsync(CancellationToken cancellationToken);
}