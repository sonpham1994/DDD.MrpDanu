using Domain.SupplyAndProductionManagement.SupplyChainManagement;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate;
using FluentAssertions;

namespace Domain.Tests.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate;

public class CompanyNameTests
{
    [Theory]
    [InlineData(" ")]
    [InlineData("")]
    [InlineData(null)]
    public void Cannot_create_company_name_if_parameter_name_is_null_or_empty(string name)
    {
        var result = CompanyName.Create(name);
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.TransactionalPartner.EmptyName);
    }

    [Fact]
    public void Cannot_create_company_name_if_parameter_name_length_exceed_300_characters()
    {
        var name = new string('t', 301);
        var result = CompanyName.Create(name);
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.TransactionalPartner.TheLengthOfNameExceedsMaxLength);
    }


    [Fact]
    public void Create_company_name_successfully()
    {
        var name = "this is my name";
        var result = CompanyName.Create(name);
        result.IsSuccess.Should().BeTrue();
        result.Value.Value.Should().Be(name);
    }

    [Fact]
    public void Should_trim_name_property()
    {
        var name = "this is my name";
        var result = CompanyName.Create($"  {name}  ");
        result.IsSuccess.Should().BeTrue();
        result.Value.Value.Should().Be(name);
    }
}