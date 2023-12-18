using Domain.SharedKernel.Base;

namespace Domain.ProductManagement;

public class BoMCode : ValueObject
{
    public string Value { get; }

    //required EF
    //protected BoMCode() {}
    //or you can use constructor with the same parameter with the properties like this. For example property is
    //'Value' so the parameter of the constructor would be 'value'. So in this case we don't need to introduce
    // the 'protected BoMCode() {}' like this
    private BoMCode(string value)
    {
        Value = value;
    }

    public Result<BoMCode> Create(BoMId bomId)
    {
        if (bomId.Value == 0)
            return DomainErrors.BoM.InvalidId(bomId.Value);
        
        string revision = bomId.Value.ToString("BOM000000#");
        
        return new BoMCode(revision);
    }

    protected override IEnumerable<int> GetHashCodeComponents()
    {
        yield return Value.GetHashCode();
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