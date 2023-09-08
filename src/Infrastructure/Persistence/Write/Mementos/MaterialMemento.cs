using Domain.MaterialManagement.MaterialAggregate;

namespace Infrastructure.Persistence.Write.Mementos;

internal sealed class MaterialMemento
{
    internal Guid Id { get; }
    internal string Code { get; }
    internal string CodeUnique { get; }
    internal MaterialAttributes Attributes { get; }
    internal MaterialType MaterialType { get; }
    internal RegionalMarket RegionalMarket { get; }

    internal List<MaterialCostManagement> MaterialCostManagements { get; }

    internal MaterialMemento(Material material)
    {
        Id = material.Id;
        Code = material.Code;
        CodeUnique = material.CodeUnique;
        Attributes = material.Attributes;
        MaterialType = material.MaterialType;
        RegionalMarket = material.RegionalMarket;
        MaterialCostManagements = material.MaterialCostManagements.ToList();
    }
}
