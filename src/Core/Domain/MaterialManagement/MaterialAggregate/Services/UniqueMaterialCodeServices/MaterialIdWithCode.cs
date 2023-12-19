using Domain.SharedKernel.ValueObjects;

namespace Domain.MaterialManagement.MaterialAggregate.Services.UniqueMaterialCodeServices;

public sealed class MaterialIdWithCode
{
    public MaterialId Id { get; init; }
    public string Code { get; init; }
}