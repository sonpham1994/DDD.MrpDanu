using Domain.MaterialManagement.MaterialAggregate;

namespace Application.Interfaces.Repositories;

public interface IMaterialRepository
{
    Task<Material?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    void Save(Material material);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}