using Domain.SharedKernel.Base;
using Domain.SupplyAndProductionManagement.SupplyChainManagement;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;
using FluentAssertions;

namespace Domain.Tests.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;

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
        DomainError error)
    {
        var materialAttributes = MaterialAttributes.Create(colorCode, width, weight, unit, varian);

        materialAttributes.IsSuccess.Should().BeFalse();
        materialAttributes.Error.Should().Be(error);
    }

    [Fact]
    public void Should_trim_all_properties()
    {
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
        yield return new object[] { "", "", "", "unit", "varian", DomainErrors.Material.EmptyWidth };
        yield return new object[] { "", null, "", "unit", "varian", DomainErrors.Material.EmptyWidth };
        yield return new object[] { "", " ", "", "unit", "varian", DomainErrors.Material.EmptyWidth };

        yield return new object[] { "", "width1", "", "", "varian", DomainErrors.Material.EmptyUnit };
        yield return new object[] { "", "width1", "", null, "varian", DomainErrors.Material.EmptyUnit };
        yield return new object[] { "", "width1", "", " ", "varian", DomainErrors.Material.EmptyUnit };

        yield return new object[] { "", "width1", "", "unit", "", DomainErrors.Material.EmptyVarian };
        yield return new object[] { "", "width1", "", "unit", null, DomainErrors.Material.EmptyVarian };
        yield return new object[] { "", "width1", "", "unit", " ", DomainErrors.Material.EmptyVarian };
    }
}