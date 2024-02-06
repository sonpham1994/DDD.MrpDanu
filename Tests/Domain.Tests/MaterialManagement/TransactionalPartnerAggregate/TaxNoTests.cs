using Domain.SupplyAndProductionManagement.SupplyChainManagement;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate;
using FluentAssertions;

namespace Domain.Tests.MaterialManagement.TransactionalPartnerAggregate;

public class TaxNoTests
{
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Cannot_create_tax_no_with_empty_value(string taxNo)
    {
        var result = TaxNo.Create(taxNo, Country.Korean);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.TransactionalPartner.EmptyTaxNo);
    }

    [Theory]
    [InlineData("12345678901")]
    [InlineData("123456789")]
    public void Cannot_create_Vietnam_tax_no_if_length_tax_no_is_not_ten(string taxNo)
    {
        var result = TaxNo.Create(taxNo, Country.VietNam);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.TransactionalPartner.InvalidLengthTaxNo);
    }

    [Theory]
    [InlineData("abcxyzbdrt")]
    [InlineData("1bcx4zb5rt")]
    public void Cannot_create_tax_no_Vietnam_if_value_is_not_numbers(string taxNo)
    {
        var result = TaxNo.Create(taxNo, Country.VietNam);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.TransactionalPartner.InvalidTaxNo);
    }

    [Fact]
    public void Create_vietnam_tax_no_successfully()
    {
        string taxNo = "1234567890";
        var result = TaxNo.Create(taxNo, Country.VietNam);

        result.IsSuccess.Should().BeTrue();
        result.Value.Value.Should().Be(taxNo);
        result.Value.Country.Should().Be(Country.VietNam);
    }

    [Fact]
    public void Create_tax_no_successfully()
    {
        string taxNo = "12345";
        var result = TaxNo.Create(taxNo, Country.Korean);

        result.IsSuccess.Should().BeTrue();
        result.Value.Value.Should().Be(taxNo);
        result.Value.Country.Should().Be(Country.Korean);
    }

    [Fact]
    public void Tax_no_should_trim()
    {
        string taxNo = "12345";
        var result = TaxNo.Create($"  {taxNo}  ", Country.Korean);

        result.IsSuccess.Should().BeTrue();
        result.Value.Value.Should().Be(taxNo);
        result.Value.Country.Should().Be(Country.Korean);
    }
}