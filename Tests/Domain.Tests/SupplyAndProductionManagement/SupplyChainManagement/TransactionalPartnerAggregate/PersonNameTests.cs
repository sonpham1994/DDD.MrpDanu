using Domain.SupplyAndProductionManagement.SupplyChainManagement;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate;
using Domain.Tests.Helpers;
using FluentAssertions;

namespace Domain.Tests.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate;

public class PersonNameTests
{
    [Theory]
    [InlineData(" ")]
    [InlineData("")]
    [InlineData(null)]
    public void Cannot_create_person_name_if_parameter_name_is_null_or_empty(string name)
    {
        var result = PersonName.Create(name);
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(DomainErrors.ContactPersonInformation.EmptyName);
    }

    [Fact]
    public void Cannot_create_person_name_if_parameter_name_length_exceed_max_length()
    {
        var name = Helper.CreateStringByLength(201, 't');
        var result = PersonName.Create(name);
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(DomainErrors.ContactPersonInformation.TheLengthOfNameExceedsMaxLength);
    }


    [Fact]
    public void Create_person_name_successfully()
    {
        var name = "this is my name";
        var result = PersonName.Create(name);
        result.IsSuccess.Should().BeTrue();
        result.Value.Value.Should().Be(name);
    }

    [Fact]
    public void Should_trim_name_property()
    {
        var name = "this is my name";
        var result = PersonName.Create($"  {name}  ");
        result.IsSuccess.Should().BeTrue();
        result.Value.Value.Should().Be(name);
    }
}