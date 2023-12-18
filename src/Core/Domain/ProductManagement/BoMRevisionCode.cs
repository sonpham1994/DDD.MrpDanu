using Domain.SharedKernel.Base;

namespace Domain.ProductManagement;

public class BoMRevisionCode : ValueObject
{
    public string Value { get; }

    private BoMRevisionCode(string code)
    {
        Value = code;
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