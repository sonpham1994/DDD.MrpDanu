using Domain.SharedKernel.Base;

namespace Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;

public class MaterialName : ValueObject
{
    private static ushort NameMaxLength => 500;
    private MaterialName(string name)
    {
        Value = name;
    }
    
    protected MaterialName() { }
    
    public string Value { get; } = default!;

    public static Result<MaterialName?> Create(string? name)
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            return DomainErrors.Material.EmptyName;
        name = name.Trim();
        if (name.Length > NameMaxLength)
            return DomainErrors.Material.ExceedsMaxLengthName;

        return new MaterialName(name);
    }
    
    protected override IEnumerable<int> GetHashCodeComponents()
    {
        yield return Value.GetHashCode();
    }

    protected override bool EqualComponents(ValueObject valueObject)
    {
        if (valueObject is not MaterialName materialName)
            return false;
        if (Value != materialName.Value)
            return false;

        return true;
    }
}