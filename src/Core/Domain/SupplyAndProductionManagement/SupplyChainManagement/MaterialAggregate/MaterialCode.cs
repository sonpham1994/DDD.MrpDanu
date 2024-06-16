using System.Text.RegularExpressions;
using Domain.SharedKernel.Base;

namespace Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;

public class MaterialCode : ValueObject
{
    private static byte CodeMaxLength => 200;
    
    // validation for varchar, we don't use nvarchar
    private static readonly Regex CharactersCodeAllowed = new(@"^[a-zA-Z0-9\s!@#\$%\^&\*\(\)_\-\+=\{\}\[\]\|\\:;\""'<>\.,\?/]*$",
        RegexOptions.Compiled,
        // why we need timeout for Regex: https://www.youtube.com/watch?v=NOLn0QwGlEE&ab_channel=NickChapsas
        TimeSpan.FromMilliseconds(250));
    
    private MaterialCode(string code)
    {
        Value = code;
    }
    protected MaterialCode() { }
   
    public string Value { get; } = default!;

    public static Result<MaterialCode?> Create(string? code)
    {
        if (string.IsNullOrEmpty(code) || string.IsNullOrWhiteSpace(code))
            return DomainErrors.Material.EmptyCode;
        
        code = code.Trim();
        
        if (code.Length > CodeMaxLength)
            return DomainErrors.Material.ExceedsMaxLengthCode;
        if (!CharactersCodeAllowed.IsMatch(code))
            return DomainErrors.Material.CodeNotAllowedSpecialCharacters;

        return new MaterialCode(code);
    }
    protected override IEnumerable<int> GetHashCodeComponents()
    {
        yield return Value.GetHashCode();
    }

    protected override bool EqualComponents(ValueObject valueObject)
    {
        if (valueObject is not MaterialCode materialCode)
            return false;
        if (Value != materialCode.Value)
            return false;

        return true;
    }
}