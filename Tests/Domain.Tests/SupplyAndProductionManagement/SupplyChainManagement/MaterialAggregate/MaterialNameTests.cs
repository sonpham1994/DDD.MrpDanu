using Domain.SupplyAndProductionManagement.SupplyChainManagement;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;
using Domain.Tests.Helpers;
using FluentAssertions;

namespace Domain.Tests.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;

public class MaterialNameTests
{
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Cannot_create_material_name_with_null_or_empty_name(string? code)
    {
        var result = MaterialName.Create(code);

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(DomainErrors.Material.EmptyName);
    }
    
    [Fact]
    public void Cannot_create_material_name_with_exceeding_max_length()
    {
        var name = Helper.CreateStringByLength(501, 't');
        var result = MaterialName.Create(name);

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(DomainErrors.Material.ExceedsMaxLengthName);
    }
    
    [Fact]
    public void Create_material_name_successfully()
    {
        string name = "name";
        var result = MaterialName.Create(name);

        result.IsSuccess.Should().BeTrue();
        result.Value!.Value.Should().Be(name);
    }
    
    [Theory]
    [InlineData(" name")]
    [InlineData("name ")]
    [InlineData(" name ")]
    [InlineData("   name")]
    [InlineData("name    ")]
    [InlineData("   name    ")]
    public void Material_name_should_trim_value(string name)
    {
        var result = MaterialName.Create(name);

        result.IsSuccess.Should().BeTrue();
        result.Value!.Value.Should().Be("name");
    }
}