using SharedKernel;

namespace Domain.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate;

public class CompanyName : ValueObject<CompanyName>
{
    private static ushort CompanyNameMaxLength => 300;
    public string Value { get; } = default!;

    private CompanyName(string name)
    {
        Value = name;
    }

    protected CompanyName() { }

    public static Result<CompanyName?> Create(string? name)
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

    protected override bool EqualComponents(CompanyName other) 
        => Value == other.Value;
}