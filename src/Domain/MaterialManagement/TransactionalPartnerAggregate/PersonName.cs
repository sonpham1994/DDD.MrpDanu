using Domain.Errors;
using Domain.SharedKernel.Base;

namespace Domain.MaterialManagement.TransactionalPartnerAggregate;

public class PersonName : ValueObject
{
    private const byte PersonNameMaxLength = 200;
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

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }
}
