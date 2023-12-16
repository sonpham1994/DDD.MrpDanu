using Domain.MaterialManagement.MaterialAggregate;
using Domain.SharedKernel.Base;

namespace Domain.Services;

public static class UniqueMaterialCode
{
    public static Result CheckUniqueMaterialCode(Material material, IReadOnlyList<Material> materials)
    {
        var existsCodeInAnotherMaterial = materials.FirstOrDefault(x => x != material && material.Code == x.Code);
        if (existsCodeInAnotherMaterial is not null)
        {
            return DomainErrors.ExistsMaterialCode(material.Code, existsCodeInAnotherMaterial.Id);
        }

        return Result.Success();
    }
}