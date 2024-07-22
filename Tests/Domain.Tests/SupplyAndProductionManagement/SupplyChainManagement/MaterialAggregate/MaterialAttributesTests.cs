using SharedKernel;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;
using FluentAssertions;
using DomainErrors = Domain.SupplyAndProductionManagement.SupplyChainManagement.DomainErrors;

namespace Domain.Tests.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;

public class MaterialAttributesTests
{
    [Theory]
    [MemberData(nameof(GetNullOrEmptyOfMandatoryProperties))]
    public void Mandatory_parameters_should_not_null_or_empty(
        string? colorCode,
        string? width,
        string? weight,
        string? unit,
        string? varian,
        DomainError error)
    {
        var materialAttributes = MaterialAttributes.Create(colorCode, width, weight, unit, varian);

        materialAttributes.IsSuccess.Should().BeFalse();
        materialAttributes.Error.Should().Be(error);
    }

    [Theory]
    [InlineData("color code ", "width", "weight", "unit", "varian")]
    [InlineData(" color code", "width", "weight", "unit", "varian")]
    [InlineData(" color code ", "width", "weight", "unit", "varian")]
    [InlineData("   color code", "width", "weight", "unit", "varian")]
    [InlineData("color code  ", "width", "weight", "unit", "varian")]
    [InlineData("   color code  ", "width", "weight", "unit", "varian")]
    [InlineData("color code", " width", "weight", "unit", "varian")]
    [InlineData("color code", "width ", "weight", "unit", "varian")]
    [InlineData("color code", " width ", "weight", "unit", "varian")]
    [InlineData("color code", "     width", "weight", "unit", "varian")]
    [InlineData("color code", "width    ", "weight", "unit", "varian")]
    [InlineData("color code", "     width    ", "weight", "unit", "varian")]
    [InlineData("color code", "width", " weight", "unit", "varian")]
    [InlineData("color code", "width", "weight ", "unit", "varian")]
    [InlineData("color code", "width", " weight ", "unit", "varian")]
    [InlineData("color code", "width", "    weight", "unit", "varian")]
    [InlineData("color code", "width", "weight  ", "unit", "varian")]
    [InlineData("color code", "width", "    weight  ", "unit", "varian")]
    [InlineData("color code", "width", "weight", " unit", "varian")]
    [InlineData("color code", "width", "weight", "unit ", "varian")]
    [InlineData("color code", "width", "weight", " unit ", "varian")]
    [InlineData("color code", "width", "weight", "  unit", "varian")]
    [InlineData("color code", "width", "weight", "unit  ", "varian")]
    [InlineData("color code", "width", "weight", "  unit  ", "varian")]
    [InlineData("color code", "width", "weight", "unit", " varian")]
    [InlineData("color code", "width", "weight", "unit", "varian ")]
    [InlineData("color code", "width", "weight", "unit", " varian ")]
    [InlineData("color code", "width", "weight", "unit", "  varian")]
    [InlineData("color code", "width", "weight", "unit", "varian    ")]
    [InlineData("color code", "width", "weight", "unit", "  varian    ")]
    public void Create_material_attributes_successfully_and_should_trim_all_properties(
        string colorCode, 
        string width, 
        string weight, 
        string unit, 
        string varian)
    {
        var materialAttributes = MaterialAttributes.Create(
            colorCode,
            width,
            weight,
            unit,
            varian);

        materialAttributes.IsSuccess.Should().BeTrue();
        materialAttributes.Value!.ColorCode.Should().Be(colorCode.Trim());
        materialAttributes.Value.Width.Should().Be(width.Trim());
        materialAttributes.Value.Weight.Should().Be(weight.Trim());
        materialAttributes.Value.Unit.Should().Be(unit.Trim().ToUpper());
        materialAttributes.Value.Varian.Should().Be(varian.Trim());
    }
    
    [Theory]
    [InlineData(" ", " ")]
    [InlineData("", "")]
    [InlineData(null, null)]
    public void Create_material_attributes_successfully_with_colorcode_and_weight_are_null_or_empty(string? colorCode, string? weight)
    {
        string width = "width 1";
        string unit = "unit 1";
        string varian = "varian 1";
        var materialAttributes = MaterialAttributes.Create(
            $" {colorCode} ",
            $" {width} ",
            $" {weight} ",
            $" {unit} ",
            $" {varian} ");

        materialAttributes.IsSuccess.Should().Be(true);
        materialAttributes.Value!.ColorCode.Should().Be(string.Empty);
        materialAttributes.Value.Weight.Should().Be(string.Empty);
    }

    public static IEnumerable<object?[]> GetNullOrEmptyOfMandatoryProperties()
    {
        yield return new object?[] { "", "", "", "unit", "varian", DomainErrors.Material.EmptyWidth };
        yield return new object?[] { "", null, "", "unit", "varian", DomainErrors.Material.EmptyWidth };
        yield return new object?[] { "", " ", "", "unit", "varian", DomainErrors.Material.EmptyWidth };

        yield return new object?[] { "", "width1", "", "", "varian", DomainErrors.Material.EmptyUnit };
        yield return new object?[] { "", "width1", "", null, "varian", DomainErrors.Material.EmptyUnit };
        yield return new object?[] { "", "width1", "", " ", "varian", DomainErrors.Material.EmptyUnit };

        yield return new object?[] { "", "width1", "", "unit", "", DomainErrors.Material.EmptyVarian };
        yield return new object?[] { "", "width1", "", "unit", null, DomainErrors.Material.EmptyVarian };
        yield return new object?[] { "", "width1", "", "unit", " ", DomainErrors.Material.EmptyVarian };
    }
}