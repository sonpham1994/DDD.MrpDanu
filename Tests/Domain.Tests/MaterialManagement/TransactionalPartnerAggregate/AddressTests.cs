using Domain.Errors;
using Domain.MaterialManagement.TransactionalPartnerAggregate;
using Domain.SharedKernel.Base;
using FluentAssertions;

namespace Domain.Tests.MaterialManagement.TransactionalPartnerAggregate;

public class AddressTests
{
    [Theory]
    [MemberData(nameof(GetNullOrEmptyOfMandatoryProperties))]
    public void Mandatory_parameters_should_not_be_null_or_empty(string street, 
        string city, 
        string district, 
        string ward, 
        string zipCode,
        DomainError error)
    {
        var address = Address.Create(street, city, district, ward, zipCode, Country.VietNam);
        address.IsFailure.Should().BeTrue();
        address.Error.Should().Be(error);
    }

    [Fact]
    public void Cannot_create_address_if_zipcode_length_is_not_five_characters()
    {
        var address = Address.Create("street", "city", "district", "ward", "123456", Country.VietNam);
        address.IsFailure.Should().BeTrue();
        address.Error.Should().Be(DomainErrors.TransactionalPartner.InvalidAddressZipCode);
    }

    [Fact]
    public void Cannot_create_address_if_zipcode_is_not_numbers()
    {
        var address = Address.Create("street", "city", "district", "ward", "abcde", Country.VietNam);
        address.IsFailure.Should().BeTrue();
        address.Error.Should().Be(DomainErrors.TransactionalPartner.InvalidAddressZipCode);
    }
    
    [Fact]
    public void Create_address_successfully()
    {
        var street = "street";
        var city = "city";
        var district = "district";
        var ward = "ward";
        var zipCode = "12345";
        var address = Address.Create(
            street, 
            city, 
            district, 
            ward, 
            zipCode, 
            Country.VietNam);
        address.IsSuccess.Should().BeTrue();
        address.Value.Street.Should().Be(street);
        address.Value.City.Should().Be(city);
        address.Value.District.Should().Be(district);
        address.Value.Ward.Should().Be(ward);
        address.Value.ZipCode.Should().Be(zipCode);
        address.Value.Country.Should().Be(Country.VietNam);
    }
    
    [Fact]
    public void Should_trim_all_properties()
    {
        var street = "street";
        var city = "city";
        var district = "district";
        var ward = "ward";
        var zipCode = "12345";
        var address = Address.Create(
            $"  {street}  ", 
            $"  {city}  ", 
            $"  {district}  ", 
            $"  {ward}  ", 
            zipCode, 
            Country.VietNam);
        
        address.IsSuccess.Should().BeTrue();
        address.Value.Street.Should().Be(street);
        address.Value.City.Should().Be(city);
        address.Value.District.Should().Be(district);
        address.Value.Ward.Should().Be(ward);
        address.Value.ZipCode.Should().Be(zipCode);
    }

    public static IEnumerable<object[]> GetNullOrEmptyOfMandatoryProperties()
    {
        yield return new object[]
            { " ", "city", "district", "ward", "zipCode", DomainErrors.TransactionalPartner.EmptyAddressStreet };
        yield return new object[]
            { "", "city", "district", "ward", "zipCode", DomainErrors.TransactionalPartner.EmptyAddressStreet };
        yield return new object[]
            { null, "city", "district", "ward", "zipCode", DomainErrors.TransactionalPartner.EmptyAddressStreet };

        yield return new object[]
            { "street", " ", "district", "ward", "zipCode", DomainErrors.TransactionalPartner.EmptyAddressCity };
        yield return new object[]
            { "street", "", "district", "ward", "zipCode", DomainErrors.TransactionalPartner.EmptyAddressCity };
        yield return new object[]
            { "street", null, "district", "ward", "zipCode", DomainErrors.TransactionalPartner.EmptyAddressCity };

        yield return new object[]
            { "street", "city", " ", "ward", "zipCode", DomainErrors.TransactionalPartner.EmptyAddressDistrict };
        yield return new object[]
            { "street", "city", "", "ward", "zipCode", DomainErrors.TransactionalPartner.EmptyAddressDistrict };
        yield return new object[]
            { "street", "city", null, "ward", "zipCode", DomainErrors.TransactionalPartner.EmptyAddressDistrict };

        yield return new object[]
            { "street", "city", "district", " ", "zipCode", DomainErrors.TransactionalPartner.EmptyAddressWard };
        yield return new object[]
            { "street", "city", "district", "", "zipCode", DomainErrors.TransactionalPartner.EmptyAddressWard };
        yield return new object[]
            { "street", "city", "district", null, "zipCode", DomainErrors.TransactionalPartner.EmptyAddressWard };

        yield return new object[]
            { "street", "city", "district", "ward", " ", DomainErrors.TransactionalPartner.EmptyAddressZipCode };
        yield return new object[]
            { "street", "city", "district", "ward", "", DomainErrors.TransactionalPartner.EmptyAddressZipCode };
        yield return new object[]
            { "street", "city", "district", "ward", null, DomainErrors.TransactionalPartner.EmptyAddressZipCode };
    }
}