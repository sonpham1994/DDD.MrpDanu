using SharedKernel;

namespace Domain.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate;

public class Address : ValueObject<Address>
{
    private const byte ZipCodeLength = 5;

    public string Street { get; } = default!;
    public string City { get; } = default!;
    public string District { get; } = default!;
    public string Ward { get; } = default!;
    public string ZipCode { get; } = default!;
    public Country Country { get; } = default!;

    protected Address() { }

    private Address(string street, string city, string district, string ward, string zipCode, Country country)
    {
        Street = street;
        City = city;
        District = district;
        Ward = ward;
        ZipCode = zipCode;
        Country = country;
    }

    public static Result<Address?> Create(string? street, string? city, string? district, string? ward, string? zipCode,
        Country country)
    {
        if (string.IsNullOrEmpty(street) || string.IsNullOrWhiteSpace(street))
            return DomainErrors.TransactionalPartner.EmptyAddressStreet;
        if (string.IsNullOrEmpty(city) || string.IsNullOrWhiteSpace(city))
            return DomainErrors.TransactionalPartner.EmptyAddressCity;
        if (string.IsNullOrEmpty(district) || string.IsNullOrWhiteSpace(district))
            return DomainErrors.TransactionalPartner.EmptyAddressDistrict;
        if (string.IsNullOrEmpty(ward) || string.IsNullOrWhiteSpace(ward))
            return DomainErrors.TransactionalPartner.EmptyAddressWard;
        if (string.IsNullOrEmpty(zipCode) || string.IsNullOrWhiteSpace(zipCode))
            return DomainErrors.TransactionalPartner.EmptyAddressZipCode;
        if (zipCode.Length != ZipCodeLength || !zipCode.All(char.IsDigit))
            return DomainErrors.TransactionalPartner.InvalidAddressZipCode;

        return new Address(street.Trim(), city.Trim(), district.Trim(), ward.Trim(), zipCode, country);
    }

    protected override IEnumerable<int> GetHashCodeComponents()
    {
        yield return ZipCode.GetHashCode();
        yield return Ward.GetHashCode();
        yield return District.GetHashCode();
        yield return City.GetHashCode();
        yield return Street.GetHashCode();
        yield return Country.GetHashCode();
    }

    protected override bool EqualComponents(Address other) 
        => ZipCode == other.ZipCode 
           && Ward == other.Ward 
           && District == other.District 
           && City == other.City 
           && Street == other.Street 
           && Country == other.Country;
}