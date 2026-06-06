using SharedKernel;

namespace Domain.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate;

public class PersonName : ValueObject<PersonName>
{
    private const byte PersonNameMaxLength = 200;
    public string Value { get; } = default!;

    private PersonName(string name) => Value = name;

    protected PersonName() { }

    public static Result<PersonName?> Create(string? name)
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

    protected override bool EqualComponents(PersonName other) 
        => Value == other.Value;
}
