using Domain.SharedKernel.Base;

namespace Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;

public sealed class MaterialCode : ValueObject
{
    private static byte CodeMaxLength => 200;
    
    private MaterialCode(string code)
    {
        Value = code;
    }
    protected MaterialCode() { }
   
    public string Value { get; }

    public static Result<MaterialCode?> Create(string code)
    {
        if (string.IsNullOrEmpty(code) || string.IsNullOrWhiteSpace(code))
            return DomainErrors.Material.EmptyCode;
        code = code.Trim();
        if (code.Length > CodeMaxLength)
            return DomainErrors.Material.ExceedsMaxLengthCode;

        return new MaterialCode(code);
    }
    protected override IEnumerable<int> GetHashCodeComponents()
    {
        yield return Value.GetHashCode();
    }

    protected override bool EqualComponents(ValueObject valueObject)
    {
        if (valueObject is not MaterialCode materialCode)
            return false;
        if (Value != materialCode.Value)
            return false;

        return true;
    }
}