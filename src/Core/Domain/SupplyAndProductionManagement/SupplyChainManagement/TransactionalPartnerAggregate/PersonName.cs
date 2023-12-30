using Domain.SharedKernel.Base;

namespace Domain.SupplyChainManagement.TransactionalPartnerAggregate;

public class PersonName : ValueObject
{
    private static byte PersonNameMaxLength => 200;
    public string Value { get; }

    private PersonName(string name) 
    {
        Value = name;
    }

    protected PersonName() { }

    public static Result<PersonName> Create(string name)
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            return DomainErrors.ContactPersonInformation.EmptyName;

        name = name.Trim();

        if (name.Length > PersonNameMaxLength)
            return DomainErrors.ContactPersonInformation.TheLengthOfNameExceedsMaxLength;

        return new PersonName(name);
    }

    protected override IEnumerable<int> GetHashCodeComponents()
    {
        yield return Value.GetHashCode();
    }

    protected override bool EqualComponents(ValueObject valueObject)
    {
        if (valueObject is not PersonName other)
            return false;
        if (Value != other.Value)
            return false;

        return true;
    }
}
