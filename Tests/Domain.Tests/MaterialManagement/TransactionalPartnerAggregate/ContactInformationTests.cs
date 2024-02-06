using Domain.SupplyAndProductionManagement.SupplyChainManagement;
using Domain.SupplyChainManagement.TransactionalPartnerAggregate;
using FluentAssertions;

namespace Domain.Tests.MaterialManagement.TransactionalPartnerAggregate;

public class ContactInformationTests
{
    [Theory]
    [InlineData("", "")]
    [InlineData(" ", " ")]
    [InlineData(null, null)]
    public void Cannot_create_contact_info_with_empty_contact_info(string telNo, string email)
    {
        var result = ContactInformation.Create(telNo, email);
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.ContactPersonInformation.EmptyContact);
    }

    [Fact]
    public void Cannot_create_contact_info_if_tel_no_is_not_numbers()
    {
        var result = ContactInformation.Create("asdvv123aa", "abcxyz@gmail.com");
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.ContactPersonInformation.TelNoIsNotNumbers);
    }
    
    [Fact]
    public void Cannot_create_contact_info_if_email_length_exceed_200_characters()
    {
        var email = new string('e', 201);
        var result = ContactInformation.Create("123456789", email);
        
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.ContactPersonInformation.EmailExceedsMaxLength);
    }
    
    [Theory]
    [InlineData("abc")]
    [InlineData("abc@")]
    [InlineData("abc@gmail")]
    [InlineData("abc@gmail.")]
    [InlineData("abc@gmail.c")]
    [InlineData("abc@gmail.com.v")]
    [InlineData("abc@gmail.com.")]
    [InlineData("abc@gmail.com+")]
    [InlineData("abc@gmail.com-")]
    public void Cannot_create_contact_info_if_email_is_invalid(string email)
    {
        var result = ContactInformation.Create("123456789", email);
        
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.ContactPersonInformation.InvalidEmail);
    }

    [Fact]
    public void Create_contact_info_with_at_least_one_contact_email()
    {
        string email = "abcxyz@gmail.com";
        string telNo = string.Empty;
        var result = ContactInformation.Create(telNo, email);
        
        result.IsSuccess.Should().BeTrue();
        result.Value.Email.Should().Be(email);
        result.Value.TelNo.Should().Be(telNo);
    }
    
    [Fact]
    public void Create_contact_info_with_at_least_one_contact_telNo()
    {
        string email = string.Empty;
        string telNo = "123456789";
        var result = ContactInformation.Create(telNo, email);
        
        result.IsSuccess.Should().BeTrue();
        result.Value.Email.Should().Be(email);
        result.Value.TelNo.Should().Be(telNo);
    }

    [Theory]
    [InlineData(" ")]
    [InlineData(null)]
    public void Tel_no_should_not_store_null_or_white_space_data(string telNo)
    {
        var result = ContactInformation.Create(telNo, "abcxyz@gmail.com");

        result.IsSuccess.Should().BeTrue();
        result.Value.TelNo.Should().Be(string.Empty);
    }

    [Theory]
    [InlineData(" ")]
    [InlineData(null)]
    public void Email_should_not_store_null_or_white_space_data(string email)
    {
        var result = ContactInformation.Create("123456789", email);

        result.IsSuccess.Should().BeTrue();
        result.Value.Email.Should().Be(string.Empty);
    }

    [Fact]
    public void Contact_info_should_trim_email_and_telNo()
    {
        var telNo = "123456789";
        var email = "abcxyz@gmail.com";
        var result = ContactInformation.Create($"  {telNo}  ", $"  {email}  ");

        result.IsSuccess.Should().BeTrue();
        result.Value.Email.Should().Be(email);
        result.Value.TelNo.Should().Be(telNo);
    }
}