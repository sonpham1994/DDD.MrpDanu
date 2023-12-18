using Domain.SharedKernel.Base;

namespace Domain.ProductManagement;

public class BoMCode : ValueObject
{
    public string Value { get; }

    private BoMCode(string code)
    {
        Value = code;
    }
    
    protected override IEnumerable<int> GetHashCodeComponents()
    {
        yield return Value.GetHashCode();
    }

    public Result<BoMCode> Create(BoMId bomId)
    {
        if (bomId.Value == 0)
            return DomainErrors.BoM.InvalidId(bomId.Value);
        
        string code = bomId.Value.ToString("BOM000000#");
        
        return new BoMCode(code);
    }

    protected override bool EqualComponents(ValueObject valueObject)
    {
        if (valueObject is not BoMCode bomCode)
            return false;
        if (Value != bomCode.Value)
            return false;

        return true;
    }
}