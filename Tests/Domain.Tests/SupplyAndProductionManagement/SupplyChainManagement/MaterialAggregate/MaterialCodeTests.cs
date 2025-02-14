using Domain.SupplyAndProductionManagement.SupplyChainManagement;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;
using Domain.Tests.Helpers;
using Shouldly;

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

        result.IsSuccess.ShouldBeFalse();
        result.Error.ShouldBe(DomainErrors.Material.EmptyCode);
    }
    
    [Fact]
    public void Cannot_create_material_code_with_exceeding_max_length()
    {
        var name = Helper.CreateStringByLength(201, 't');
        var result = MaterialCode.Create(name);

        result.IsSuccess.ShouldBeFalse();
        result.Error.ShouldBe(DomainErrors.Material.ExceedsMaxLengthCode);
    }

    [Theory]
    [InlineData("!")]
    [InlineData("@")]
    [InlineData("$")]
    [InlineData("%")]
    [InlineData("^")]
    [InlineData("&")]
    [InlineData("*")]
    [InlineData("(")]
    [InlineData(")")]
    [InlineData("_")]
    [InlineData("+")]
    [InlineData("=")]
    [InlineData("`")]
    [InlineData("~")]
    [InlineData("[")]
    [InlineData("{")]
    [InlineData("]")]
    [InlineData("}")]
    [InlineData("\\")]
    [InlineData("|")]
    [InlineData(";")]
    [InlineData(":")]
    [InlineData("'")]
    [InlineData("\"")]
    [InlineData(",")]
    [InlineData("<")]
    [InlineData(">")]
    [InlineData("?")]
    [InlineData("a b")] // space
    [InlineData("a  c")] // tab
    public void Cannot_create_material_code_with_not_allowed_special_characters(string specialCharacter)
    {
        string code = "code" + specialCharacter;
        var result = MaterialCode.Create(code);

        result.IsSuccess.ShouldBeFalse();
        result.Error.ShouldBe(DomainErrors.Material.NotAllowedSpecialCharacters);
    }

    [Theory]
    [InlineData(".")]
    [InlineData("/")]
    [InlineData("-")]
    public void Cannot_create_material_code_with_only_allowed_special_character(string specialCharacter)
    {
        var result = MaterialCode.Create(specialCharacter);

        result.IsSuccess.ShouldBeFalse();
        result.Error.ShouldBe(DomainErrors.Material.NotAllowedOnlyAllowedSpecialCharacters);
    }

    [Fact]
    public void Create_material_code_successfully()
    {
        string code = "code";
        var result = MaterialCode.Create(code);

        result.IsSuccess.ShouldBeTrue();
        result.Value!.Value.ShouldBe(code);
    }
    
    [Theory]
    [InlineData(" code")]
    [InlineData("code ")]
    [InlineData(" code ")]
    [InlineData("   code")]
    [InlineData("code    ")]
    [InlineData("   code    ")]
    public void Material_code_should_trim_value(string code)
    {
        var result = MaterialCode.Create(code);

        result.IsSuccess.ShouldBeTrue();
        result.Value!.Value.ShouldBe(code.Trim());
    }

    [Theory]
    [InlineData(".")]
    [InlineData("/")]
    [InlineData("-")]
    public void Create_material_code_successfully_with_allowed_special_character(string specialCharacter)
    {
        string code = "code" + specialCharacter;
        var result = MaterialCode.Create(code);

        result.IsSuccess.ShouldBeTrue();
        result.Value!.Value.ShouldBe(code);
    }
}