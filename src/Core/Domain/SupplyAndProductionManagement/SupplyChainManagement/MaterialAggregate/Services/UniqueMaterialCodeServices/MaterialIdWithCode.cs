using Domain.SharedKernel.ValueObjects;

namespace Domain.SupplyChainManagement.MaterialAggregate.Services.UniqueMaterialCodeServices;

public sealed class MaterialIdWithCode
{
    public Guid Id { get; init; }
    public string Code { get; init; }
    public MaterialId MaterialId => (MaterialId)Id;
}