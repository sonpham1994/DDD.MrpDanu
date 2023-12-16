using Domain.SharedKernel.Base;

namespace Domain.Services;

public sealed class DomainErrors
{
    public static DomainError ExistsMaterialCode(string code, in Guid anotherMaterialId) => new("Material.ExistCode",
        $"The code '{code} exists in another material with id ${anotherMaterialId}'");
}