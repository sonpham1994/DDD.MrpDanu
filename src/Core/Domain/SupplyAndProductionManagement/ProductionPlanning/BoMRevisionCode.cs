using Domain.SharedKernel.Base;

namespace Domain.ProductionPlanning;

public class BoMRevisionCode : ValueObject
{
    public string Value { get; }

    //required EF
    //protected BoMRevisionCode() {}
    //or you can use constructor with the same parameter with the properties like this. For example property is
    //'Value' so the parameter of the constructor would be 'value'. So in this case we don't need to introduce
    // the 'protected BoMRevisionCode() {}' like this
    private BoMRevisionCode(string value)
    {
        Value = value;
    }

    public Result<BoMRevisionCode> Create(BoMCode boMCode, BoMRevisionId boMRevisionId)
    {
        if (boMRevisionId.Value == 0)
            return DomainErrors.BoMRevision.InvalidId(boMRevisionId.Value);
        string code = boMRevisionId.Value.ToString($"{boMCode.Value}-00#");

        return new BoMRevisionCode(code);
    }
    
    protected override IEnumerable<int> GetHashCodeComponents()
    {
        yield return Value.GetHashCode();
    }

    protected override bool EqualComponents(ValueObject valueObject)
    {
        if (valueObject is not BoMRevisionCode boMRevisionCode)
            return false;
        if (Value != boMRevisionCode.Value)
            return false;

        return true;
    }
}