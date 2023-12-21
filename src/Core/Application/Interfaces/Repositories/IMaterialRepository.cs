using Domain.MaterialManagement.MaterialAggregate;
using Domain.SharedKernel.ValueObjects;

namespace Application.Interfaces.Repositories;

public interface IMaterialRepository
{
    Task<Material?> GetByIdAsync(MaterialId id, CancellationToken cancellationToken = default);
    void Save(Material material);
    Task DeleteAsync(MaterialId id, CancellationToken cancellationToken);
}