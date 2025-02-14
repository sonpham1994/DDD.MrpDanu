using SharedKernel;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate;
using Shouldly;
using DomainErrors = Domain.SupplyAndProductionManagement.SupplyChainManagement.DomainErrors;

namespace Domain.Tests.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate;

public class AddressTests
{
    [Theory]
    [MemberData(nameof(GetNullOrEmptyOfMandatoryProperties))]
    public void Mandatory_parameters_should_not_be_null_or_empty(string? street,
        string? city,
        string? district,
        string? ward,
        string? zipCode,
        DomainError error)
    {
        var address = Address.Create(street, city, district, ward, zipCode, Country.VietNam);
        address.IsSuccess.ShouldBeFalse();
        address.Error.ShouldBe(error);
    }

    [Fact]
    public void Cannot_create_address_if_zipcode_length_is_not_five_characters()
    {
        var address = Address.Create("street", "city", "district", "ward", "123456", Country.VietNam);
        address.IsSuccess.ShouldBeFalse();
        address.Error.ShouldBe(DomainErrors.TransactionalPartner.InvalidAddressZipCode);
    }

    [Fact]
    public void Cannot_create_address_if_zipcode_is_not_numbers()
    {
        var address = Address.Create("street", "city", "district", "ward", "abcde", Country.VietNam);
        address.IsSuccess.ShouldBeFalse();
        address.Error.ShouldBe(DomainErrors.TransactionalPartner.InvalidAddressZipCode);
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
        address.IsSuccess.ShouldBeTrue();
        address.Value!.Street.ShouldBe(street);
        address.Value.City.ShouldBe(city);
        address.Value.District.ShouldBe(district);
        address.Value.Ward.ShouldBe(ward);
        address.Value.ZipCode.ShouldBe(zipCode);
        address.Value.Country.ShouldBe(Country.VietNam);
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

        address.IsSuccess.ShouldBeTrue();
        address.Value!.Street.ShouldBe(street);
        address.Value.City.ShouldBe(city);
        address.Value.District.ShouldBe(district);
        address.Value.Ward.ShouldBe(ward);
        address.Value.ZipCode.ShouldBe(zipCode);
    }

    public static IEnumerable<object?[]> GetNullOrEmptyOfMandatoryProperties()
    {
        yield return [" ", "city", "district", "ward", "zipCode", DomainErrors.TransactionalPartner.EmptyAddressStreet];
        yield return ["", "city", "district", "ward", "zipCode", DomainErrors.TransactionalPartner.EmptyAddressStreet];
        yield return [null, "city", "district", "ward", "zipCode", DomainErrors.TransactionalPartner.EmptyAddressStreet];

        yield return ["street", " ", "district", "ward", "zipCode", DomainErrors.TransactionalPartner.EmptyAddressCity];
        yield return ["street", "", "district", "ward", "zipCode", DomainErrors.TransactionalPartner.EmptyAddressCity];
        yield return ["street", null, "district", "ward", "zipCode", DomainErrors.TransactionalPartner.EmptyAddressCity];

        yield return ["street", "city", " ", "ward", "zipCode", DomainErrors.TransactionalPartner.EmptyAddressDistrict];
        yield return ["street", "city", "", "ward", "zipCode", DomainErrors.TransactionalPartner.EmptyAddressDistrict];
        yield return ["street", "city", null, "ward", "zipCode", DomainErrors.TransactionalPartner.EmptyAddressDistrict];

        yield return ["street", "city", "district", " ", "zipCode", DomainErrors.TransactionalPartner.EmptyAddressWard];
        yield return ["street", "city", "district", "", "zipCode", DomainErrors.TransactionalPartner.EmptyAddressWard];
        yield return ["street", "city", "district", null, "zipCode", DomainErrors.TransactionalPartner.EmptyAddressWard];

        yield return ["street", "city", "district", "ward", " ", DomainErrors.TransactionalPartner.EmptyAddressZipCode];
        yield return ["street", "city", "district", "ward", "", DomainErrors.TransactionalPartner.EmptyAddressZipCode];
        yield return ["street", "city", "district", "ward", null, DomainErrors.TransactionalPartner.EmptyAddressZipCode];
    }
}