using Domain.SharedKernel.Base;

namespace Domain.MaterialManagement.TransactionalPartnerAggregate;

public class Address : ValueObject
{
    private static byte ZipCodeLength => 5;
    
    public string Street { get; }
    public string City { get; }
    public string District { get; }
    public string Ward { get; }
    public string ZipCode { get; }
    public virtual Country Country { get; }

    protected Address() {}

    private Address(string street, string city, string district, string ward, string zipCode, Country country)
    {
        Street = street;
        City = city;
        District = district;
        Ward = ward;
        ZipCode = zipCode;
        Country = country;
    }

    public static Result<Address> Create(string street, string city, string district, string ward, string zipCode,
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
    
    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return ZipCode;
        yield return Ward;
        yield return District;
        yield return City;
        yield return Street;
        yield return Country;
    }
}