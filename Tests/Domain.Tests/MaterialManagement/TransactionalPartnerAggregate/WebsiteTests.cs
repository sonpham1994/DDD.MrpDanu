using Domain.Errors;
using Domain.MaterialManagement.TransactionalPartnerAggregate;
using FluentAssertions;

namespace Domain.Tests.MaterialManagement.TransactionalPartnerAggregate;

public class WebsiteTests
{
    [Theory]
    [InlineData("abc")]
    [InlineData("http://abc")]
    [InlineData("http://abc.c")]
    [InlineData("http://abc.com.")]
    [InlineData("http://abc.com.v")]
    [InlineData("https://abc")]
    [InlineData("https://abc.c")]
    [InlineData("https://abc.com.")]
    [InlineData("https://abc.com.v")]
    public void Cannot_create_website_if_value_is_invalid(string website)
    {
        var result = Website.Create(website);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(MaterialManagementDomainErrors.TransactionalPartner.InvalidWebsite(website));
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Create_null_website_with_null_or_empty_value_successfully(string website)
    {
        var result = Website.Create(website);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be((Website)null);
    }

    [Fact]
    public void Create_website_with_value_successfully()
    {
        string value = "http://abcxyz.com";
        var result = Website.Create(value);

        result.IsSuccess.Should().BeTrue();
        result.Value!.Value.Should().Be(value);
    }
}