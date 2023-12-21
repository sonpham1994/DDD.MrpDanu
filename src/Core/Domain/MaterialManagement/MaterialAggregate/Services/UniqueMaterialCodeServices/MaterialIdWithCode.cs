using Domain.SharedKernel.ValueObjects;

namespace Domain.MaterialManagement.MaterialAggregate.Services.UniqueMaterialCodeServices;

public sealed class MaterialIdWithCode
{
    public Guid Id { get; init; }
    public string Code { get; init; }
}