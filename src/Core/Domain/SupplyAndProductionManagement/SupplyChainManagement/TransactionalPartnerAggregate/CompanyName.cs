using Domain.SharedKernel.Base;

namespace Domain.SupplyChainManagement.TransactionalPartnerAggregate;

public class CompanyName : ValueObject
{
    private static ushort CompanyNameMaxLength => 300;
    public string Value { get; }

    private CompanyName(string name) 
    {
        Value = name;
    }

    protected CompanyName() { }

    public static Result<CompanyName> Create(string name)
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            return DomainErrors.TransactionalPartner.EmptyName;

        name = name.Trim();

        if (name.Length > CompanyNameMaxLength)
            return DomainErrors.TransactionalPartner.TheLengthOfNameExceedsMaxLength;

        return new CompanyName(name);
    }

    protected override IEnumerable<int> GetHashCodeComponents()
    {
        yield return Value.GetHashCode();
    }

    protected override bool EqualComponents(ValueObject obj)
    {
        if (obj is not CompanyName other)
            return false;
        if (Value != other.Value)
            return false;

        return true;
    }
}