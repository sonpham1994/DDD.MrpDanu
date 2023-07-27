using Domain.Errors;
using Domain.SharedKernel.Base;

namespace Domain.MaterialManagement.TransactionalPartnerAggregate;

public class CompanyName : ValueObject
{
    private const ushort CompanyNameMaxLength = 300;
    public string Value { get; }

    private CompanyName(string name) 
    {
        Value = name;
    }

    protected CompanyName() { }

    public static Result<CompanyName> Create(string name)
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            return MaterialManagementDomainErrors.TransactionalPartner.EmptyName;

        name = name.Trim();

        if (name.Length > CompanyNameMaxLength)
            return MaterialManagementDomainErrors.TransactionalPartner.TheLengthOfNameExceedsMaxLength;

        return new CompanyName(name);
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }
}