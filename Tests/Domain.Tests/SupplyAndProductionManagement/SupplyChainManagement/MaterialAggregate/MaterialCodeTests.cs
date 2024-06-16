using Domain.SupplyAndProductionManagement.SupplyChainManagement;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;
using Domain.Tests.Helpers;
using FluentAssertions;

namespace Domain.Tests.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;

public class MaterialCodeTests
{
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Cannot_create_material_code_with_null_or_empty_code(string? code)
    {
        var result = MaterialCode.Create(code);

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(DomainErrors.Material.EmptyCode);
    }
    
    [Fact]
    public void Cannot_create_material_code_with_exceeding_max_length()
    {
        var name = Helper.CreateStringByLength(201, 't');
        var result = MaterialCode.Create(name);

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(DomainErrors.Material.ExceedsMaxLengthCode);
    }
    
    [Fact]
    public void Create_material_code_successfully()
    {
        string code = "code";
        var result = MaterialCode.Create(code);

        result.IsSuccess.Should().BeTrue();
        result.Value!.Value.Should().Be(code);
    }
}