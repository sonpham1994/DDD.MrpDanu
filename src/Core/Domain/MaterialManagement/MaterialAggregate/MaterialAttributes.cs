using System.Text.RegularExpressions;
using Domain.SharedKernel.Base;

namespace Domain.MaterialManagement.MaterialAggregate;

//Should we consider whether this class should be a Value Object or not?
public class MaterialAttributes : ValueObject
{
    //https://frugalcafe.beehiiv.com/p/reuse-regular-expressions
    //https://www.youtube.com/watch?v=RSFiiKUvzLI&ab_channel=NickChapsas
    private static readonly Regex UniqueCodePattern = new("[^A-Za-z0-9]", 
        RegexOptions.Compiled,
        //3,732.86 ns From Benchmark.RegexBenchmarks
        // why we need timeout for Regex: https://www.youtube.com/watch?v=NOLn0QwGlEE&ab_channel=NickChapsas
        TimeSpan.FromMilliseconds(250));
    
    public string Name { get; }
    public string ColorCode { get; }
    public string Width { get; }
    public string Weight { get; }
    public string Unit { get; }
    public string Varian { get; }

    protected MaterialAttributes() {}
    
    private MaterialAttributes(string name, string colorCode, string width, string weight, string unit, string varian)
    {
        Name = name;
        ColorCode = colorCode;
        Width = width;
        Weight = weight;
        Unit = unit;
        Varian = varian;
    }

    public static Result<MaterialAttributes> Create(string name, 
        string colorCode, 
        string width, 
        string weight, 
        string unit, 
        string varian)
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            return DomainErrors.Material.EmptyName;
        if (string.IsNullOrEmpty(width) || string.IsNullOrWhiteSpace(width))
            return DomainErrors.Material.EmptyWidth;
        if (string.IsNullOrEmpty(unit) || string.IsNullOrWhiteSpace(unit))
            return DomainErrors.Material.EmptyUnit;
        if (string.IsNullOrEmpty(varian) || string.IsNullOrWhiteSpace(varian))
            return DomainErrors.Material.EmptyVarian;

        name = name.Trim();
        colorCode = colorCode.Trim();
        width = width.Trim();
        weight = weight.Trim();
        unit = unit.Trim().ToUpper();
        varian = varian.Trim();
        
        return new MaterialAttributes(name, colorCode, width, weight, unit, varian);
    }

    public string ToUniqueCode()
    {
        return $"{ReplaceSpecialCharacters(Name)}" +
               $"_{ReplaceSpecialCharactersWithEmptyData(ColorCode)}" +
               $"_{ReplaceSpecialCharacters(Width)}" +
               $"_{ReplaceSpecialCharactersWithEmptyData(Weight)}" +
               $"_{ReplaceSpecialCharacters(Unit)}" +
               $"_{ReplaceSpecialCharacters(Varian)}";
        
        string ReplaceSpecialCharacters(string data)
        {
            return UniqueCodePattern.Replace(data, string.Empty);
        }
        
        string ReplaceSpecialCharactersWithEmptyData(string data)
        {
            return (string.IsNullOrEmpty(data) || string.IsNullOrWhiteSpace(data)) 
                ? "000" 
                : ReplaceSpecialCharacters(data);
        }
    }
    
    protected override IEnumerable<int> GetHashCodeComponents()
    {
        yield return Name.GetHashCode();
        yield return Varian.GetHashCode();
        yield return Width.GetHashCode();
        yield return Weight.GetHashCode();
        yield return Unit.GetHashCode();
        yield return ColorCode.GetHashCode();
    }

    protected override bool EqualComponents(ValueObject valueObject)
    {
        if (valueObject is not MaterialAttributes other)
            return false;

        if (Name != other.Name)
            return false;
        if (Varian != other.Varian)
            return false;
        if (Width != other.Width)
            return false;
        if (Weight != other.Weight)
            return false;
        if (Unit != other.Unit)
            return false;
        if (ColorCode != other.ColorCode)
            return false;

        return true;
    }
}
