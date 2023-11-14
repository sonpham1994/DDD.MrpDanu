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
    
    protected override IEnumerable<int> GetHashCodeComponents()
    {
        yield return ZipCode.GetHashCode();
        yield return Ward.GetHashCode();
        yield return District.GetHashCode();
        yield return City.GetHashCode();
        yield return Street.GetHashCode();
        yield return Country.GetHashCode();
    }

    protected override bool EqualComponents(ValueObject obj)
    {
        if (obj is not Address other)
            return false;
        if (ZipCode != other.ZipCode)
            return false;
        if (Ward != other.Ward)
            return false;
        if (District != other.District)
            return false;
        if (City != other.City)
            return false;
        if (Street != other.Street)
            return false;
        if (Country != other.Country)
            return false;

        return true;
    }
}