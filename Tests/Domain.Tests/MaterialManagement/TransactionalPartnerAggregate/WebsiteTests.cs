using Domain.SupplyAndProductionManagement.SupplyChainManagement;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate;
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
        result.Error.Should().Be(DomainErrors.TransactionalPartner.InvalidWebsite(website));
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
    public void Cannot_create_website_with_exceeding_100_length()
    {
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        string value = new string(Enumerable.Repeat(chars, 101)
            .Select(s => s[random.Next(s.Length)]).ToArray());
        var result = Website.Create(value);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.TransactionalPartner.ExceedsMaxLengthWebsite(100));
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