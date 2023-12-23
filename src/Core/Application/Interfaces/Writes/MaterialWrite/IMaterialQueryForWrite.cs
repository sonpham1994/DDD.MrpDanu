using Domain.MaterialManagement.MaterialAggregate.Services.UniqueMaterialCodeServices;

namespace Application.Interfaces.Writes.MaterialWrite;

public interface IMaterialQueryForWrite
{
    Task<IReadOnlyList<MaterialIdWithCode>> GetByCodeAsync(string code, CancellationToken cancellationToken);
}
