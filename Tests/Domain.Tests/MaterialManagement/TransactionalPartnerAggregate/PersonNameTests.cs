using Domain.MaterialManagement;
using Domain.MaterialManagement.TransactionalPartnerAggregate;
using FluentAssertions;

namespace Domain.Tests.MaterialManagement.TransactionalPartnerAggregate;

public class PersonNameTests
{
    [Theory]
    [InlineData(" ")]
    [InlineData("")]
    [InlineData(null)]
    public void Cannot_create_person_name_if_parameter_name_is_null_or_empty(string name)
    {
        var result = PersonName.Create(name);
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.ContactPersonInformation.EmptyName);
    }

    [Fact]
    public void Cannot_create_person_name_if_parameter_name_length_exceed_200_characters()
    {
        var name = new string('t', 201);
        var result = PersonName.Create(name);
        result.IsFailure.Should().BeTrue();
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