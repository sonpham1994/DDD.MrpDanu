using Domain.MaterialManagement.MaterialAggregate;

namespace Infrastructure.Persistence.Write.Mementos.MementoModels;

internal sealed class MaterialMemento
{
    internal Guid Id { get; }
    internal string Code { get; }
    internal string CodeUnique { get; }
    internal string Name { get; }
    internal string ColorCode { get; }
    internal string Width { get; }
    internal string Unit { get; }
    internal string Varian { get; } 
    internal string Weight { get; }
    internal byte MaterialTypeId { get; }
    internal byte RegionalMarketId { get; }

    internal List<MaterialCostManagementMemento> MaterialCostManagements { get; }

    internal MaterialMemento(Material material)
    {
        Id = material.Id;
        Code = material.Code;
        CodeUnique = material.CodeUnique;
        Name = material.Attributes.Name;
        ColorCode = material.Attributes.ColorCode;
        Width = material.Attributes.Width;
        Unit = material.Attributes.Unit;
        Varian = material.Attributes.Varian;
        Weight = material.Attributes.Weight;
        MaterialTypeId = material.MaterialType.Id;
        RegionalMarketId = material.RegionalMarket.Id;
        MaterialCostManagements = material.MaterialCostManagements
            .Select(x=> new MaterialCostManagementMemento(x, Id))
            .ToList();
    }
}

internal sealed class MaterialCostManagementMemento
{
    internal Guid Id { get; }
    internal Guid MaterialId { get; }
    internal Guid TransactionalPartnerId { get; }
    internal decimal Surcharge { get; }
    //Due to "No mapping exists from DbType UInt32 to a known SqlDbType." exception. This is because the MSSql does
    //not support unsigned data type. So we replace uint by int
    internal int MinQuantity { get; }
    internal decimal Price { get; }
    internal byte CurrencyTypeId { get; }

    public MaterialCostManagementMemento(MaterialCostManagement materialCost, Guid materialId)
    {
        Id = materialCost.Id;
        MaterialId = materialId;
        TransactionalPartnerId = materialCost.TransactionalPartner.Id;
        Surcharge = materialCost.Surcharge.Value;
        MinQuantity = Convert.ToInt32(materialCost.MinQuantity);
        Price = materialCost.Price.Value;
        CurrencyTypeId = materialCost.Price.CurrencyType.Id;
    }
}
