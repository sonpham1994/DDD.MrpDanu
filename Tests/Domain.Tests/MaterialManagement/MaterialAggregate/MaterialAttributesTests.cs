using Domain.MaterialManagement;
using Domain.MaterialManagement.MaterialAggregate;
using Domain.SharedKernel.Base;
using FluentAssertions;

namespace Domain.Tests.MaterialManagement.MaterialAggregate;

public class MaterialAttributesTests
{
    [Theory]
    [MemberData(nameof(GetNullOrEmptyOfMandatoryProperties))]
    public void Mandatory_parameters_should_not_null_or_empty( 
        string colorCode, 
        string width, 
        string weight, 
        string unit, 
        string varian, 
        bool isFailure, 
        DomainError error)
    {
        var materialAttributes = MaterialAttributes.Create(colorCode, width, weight, unit, varian);

        materialAttributes.IsFailure.Should().Be(isFailure);
        materialAttributes.Error.Should().Be(error);
    }

    // [Theory]
    // [InlineData("Name.1", "", "width", "", "unit", "varian", "Name1_000_width_000_UNIT_varian")]
    // [InlineData("Name-1", "", "width", "", "unit", "varian", "Name1_000_width_000_UNIT_varian")]
    // [InlineData("Name 1", "", "width", "", "unit", "varian", "Name1_000_width_000_UNIT_varian")]
    //
    // [InlineData("name1", "ColorCode", "width", "", "unit", "varian", "name1_ColorCode_width_000_UNIT_varian")]
    // [InlineData("name1", "Color Code", "width", "", "unit", "varian", "name1_ColorCode_width_000_UNIT_varian")]
    // [InlineData("name1", "ColorCode.1", "width", "", "unit", "varian", "name1_ColorCode1_width_000_UNIT_varian")]
    // [InlineData("name1", "ColorCode-1", "width", "", "unit", "varian", "name1_ColorCode1_width_000_UNIT_varian")]
    //
    // [InlineData("name1", "", "Width", "", "unit", "varian", "name1_000_Width_000_UNIT_varian")]
    // [InlineData("name1", "", "Width 1", "", "unit", "varian", "name1_000_Width1_000_UNIT_varian")]
    // [InlineData("name1", "", "Width-1", "", "unit", "varian", "name1_000_Width1_000_UNIT_varian")]
    // [InlineData("name1", "", "Width.1", "", "unit", "varian", "name1_000_Width1_000_UNIT_varian")]
    //
    // [InlineData("name1", "", "width", "Weight", "unit", "varian", "name1_000_width_Weight_UNIT_varian")]
    // [InlineData("name1", "", "width", "Weight 1", "unit", "varian", "name1_000_width_Weight1_UNIT_varian")]
    // [InlineData("name1", "", "width", "Weight.1", "unit", "varian", "name1_000_width_Weight1_UNIT_varian")]
    // [InlineData("name1", "", "width", "Weight-1", "unit", "varian", "name1_000_width_Weight1_UNIT_varian")]
    //
    // [InlineData("name1", "", "width", "", "Unit", "varian", "name1_000_width_000_UNIT_varian")]
    // [InlineData("name1", "", "width", "", "Unit 1", "varian", "name1_000_width_000_UNIT1_varian")]
    // [InlineData("name1", "", "width", "", "Unit.1", "varian", "name1_000_width_000_UNIT1_varian")]
    // [InlineData("name1", "", "width", "", "Unit-1", "varian", "name1_000_width_000_UNIT1_varian")]
    //
    // [InlineData("name1", "", "width", "", "unit", "Varian", "name1_000_width_000_UNIT_Varian")]
    // [InlineData("name1", "", "width", "", "unit", "Varian 1", "name1_000_width_000_UNIT_Varian1")]
    // [InlineData("name1", "", "width", "", "unit", "Varian-1", "name1_000_width_000_UNIT_Varian1")]
    // [InlineData("name1", "", "width", "", "unit", "Varian.1", "name1_000_width_000_UNIT_Varian1")]
    // public void To_code_unique_method_should_return_correct_pattern(string name, string colorCode, string width, string weight, string unit, string varian, string expected)
    // { 
    //     var materialAttributes = MaterialAttributes.Create(name, colorCode, width, weight, unit, varian);
    //     materialAttributes.IsSuccess.Should().Be(true);
    //     materialAttributes.Value.ToUniqueCode().Should().Be(expected);
    // }

    [Fact]
    public void Should_trim_all_properties()
    {
        string name = "name 1";
        string colorCode = "color code 1";
        string width = "width 1";
        string weight = "weight 1";
        string unit = "unit 1";
        string varian = "varian 1";
        var materialAttributes = MaterialAttributes.Create( 
            $" {colorCode} ",
            $" {width} ",
            $" {weight} ",
            $" {unit} ",
            $" {varian} ");

        materialAttributes.IsSuccess.Should().Be(true);
        materialAttributes.Value.ColorCode.Should().Be(colorCode);
        materialAttributes.Value.Width.Should().Be(width);
        materialAttributes.Value.Weight.Should().Be(weight);
        materialAttributes.Value.Unit.Should().Be(unit.ToUpper());
        materialAttributes.Value.Varian.Should().Be(varian);
    }
    
    public static IEnumerable<object[]> GetNullOrEmptyOfMandatoryProperties()
    {
        // yield return new object[] { "", "", "width1", "", "unit", "varian", true, DomainErrors.Material.EmptyName };
        // yield return new object[] { null, "", "width1", "", "unit", "varian", true, DomainErrors.Material.EmptyName };
        // yield return new object[] { " ", "", "width1", "", "unit", "varian", true, DomainErrors.Material.EmptyName };
        
        yield return new object[] { "", "", "", "unit", "varian", true, DomainErrors.Material.EmptyWidth };
        yield return new object[] { "", null, "", "unit", "varian", true, DomainErrors.Material.EmptyWidth };
        yield return new object[] { "", " ", "", "unit", "varian", true, DomainErrors.Material.EmptyWidth };

        yield return new object[] { "", "width1", "", "", "varian", true, DomainErrors.Material.EmptyUnit };
        yield return new object[] { "", "width1", "", null, "varian", true, DomainErrors.Material.EmptyUnit };
        yield return new object[] { "", "width1", "", " ", "varian", true, DomainErrors.Material.EmptyUnit };

        yield return new object[] { "", "width1", "", "unit", "", true, DomainErrors.Material.EmptyVarian };
        yield return new object[] { "", "width1", "", "unit", null, true, DomainErrors.Material.EmptyVarian };
        yield return new object[] { "", "width1", "", "unit", " ", true, DomainErrors.Material.EmptyVarian };
    }
}