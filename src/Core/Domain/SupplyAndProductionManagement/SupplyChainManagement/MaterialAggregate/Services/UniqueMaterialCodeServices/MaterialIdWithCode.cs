using Domain.SharedKernel.ValueObjects;

namespace Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Services.UniqueMaterialCodeServices;

public class MaterialIdWithCode
{
    public Guid Id { get; init; }
    public string Code { get; init; } = default!;
    public MaterialId MaterialId => (MaterialId)Id;
}