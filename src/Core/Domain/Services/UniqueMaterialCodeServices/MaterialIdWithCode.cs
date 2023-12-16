namespace Domain.Services.UniqueMaterialCodeServices;

public sealed class MaterialIdWithCode
{
    public Guid Id { get; init; }
    public string Code { get; init; }
}