using SharedKernel.ValueObjects;

namespace Domain.Services.UniqueMaterialCodeServices;

public class MaterialIdWithCode
{
    public Guid Id { get; init; }
    public string Code { get; init; } = default!;
    public MaterialId MaterialId => (MaterialId)Id;
}