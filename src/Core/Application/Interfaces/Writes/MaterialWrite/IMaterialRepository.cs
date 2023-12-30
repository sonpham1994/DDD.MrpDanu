using Domain.SupplyChainManagement.MaterialAggregate;
using Domain.SharedKernel.ValueObjects;

namespace Application.Interfaces.Writes.MaterialWrite;

public interface IMaterialRepository
{
    ValueTask<Material?> GetByIdAsync(MaterialId id, CancellationToken cancellationToken = default);
    void Save(Material material);
    Task DeleteAsync(MaterialId id, CancellationToken cancellationToken);
}