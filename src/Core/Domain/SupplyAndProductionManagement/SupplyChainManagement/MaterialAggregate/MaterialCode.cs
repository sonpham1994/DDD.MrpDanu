using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using SharedKernel;

namespace Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;

public class MaterialCode : ValueObject<MaterialCode>
{
    private const byte CodeMaxLength = 200;
    // validation for characters and special characters like '-', '.', '/'
    private static readonly Regex CharactersAndSpecialCharactersAllowed = new(@"^[a-zA-Z0-9\-/\.]*$",
        RegexOptions.Compiled,
        // why we need timeout for Regex: https://www.youtube.com/watch?v=NOLn0QwGlEE&ab_channel=NickChapsas
        TimeSpan.FromMilliseconds(250));
    private static readonly Regex SpecialCharactersAllowed = new(@"^[\-/\.]*$",
        RegexOptions.Compiled,
        // why we need timeout for Regex: https://www.youtube.com/watch?v=NOLn0QwGlEE&ab_channel=NickChapsas
        TimeSpan.FromMilliseconds(250));

    private MaterialCode(string code) => Value = code;
    protected MaterialCode() { }
   
    public string Value { get; } = default!;

    public static Result<MaterialCode?> Create(string? code)
    {
        if (string.IsNullOrWhiteSpace(code))
            return DomainErrors.Material.EmptyCode;
        
        code = code.Trim();
        
        if (code.Length > CodeMaxLength)
            return DomainErrors.Material.ExceedsMaxLengthCode;

        // This validation should be discussed with client, if the client accept English characters and
        // special characters like '-', '.', '/', we will bring them into this invariant.
        var resultCodeAllowed = CharactersAndSpecialCharactersAllowed.IsMatch(code);
        if (!resultCodeAllowed)
            return DomainErrors.Material.NotAllowedSpecialCharacters;
        
        var resultOnlySpecialCharacterCodeAllowed = SpecialCharactersAllowed.IsMatch(code);
        if (resultOnlySpecialCharacterCodeAllowed)
            return DomainErrors.Material.NotAllowedOnlyAllowedSpecialCharacters;

        return new MaterialCode(code);
    }
    protected override IEnumerable<int> GetHashCodeComponents()
    {
        yield return Value.GetHashCode();
    }

    protected override bool EqualComponents(MaterialCode other) 
        => Value == other.Value;
}