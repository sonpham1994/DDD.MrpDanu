using System.Text.RegularExpressions;
using Domain.SharedKernel.Base;

namespace Domain.MaterialManagement.MaterialAggregate;

//Should we consider whether this class should be a Value Object or not? - Yes, when we want to know that whether
// two materials are the same or not, we should compare attributes of material from Color, width, weight, unit,
//varian. If all of these attributes are the same, we consider two materials to equal. Name maybe not need live
// in this value object here. if we have two different code and name from different suppliers (each supplier define
// name and code of their material differently) but share the same attributes of materials such as color, width,
// weight, unit, varian, we can treat two materials as the same. So we remove Name from this value object
public class MaterialAttributes : ValueObject
{
    //https://frugalcafe.beehiiv.com/p/reuse-regular-expressions
    //https://www.youtube.com/watch?v=RSFiiKUvzLI&ab_channel=NickChapsas
    private static readonly Regex UniqueCodePattern = new("[^A-Za-z0-9]", 
        RegexOptions.Compiled,
        //3,732.86 ns From Benchmark.RegexBenchmarks
        // why we need timeout for Regex: https://www.youtube.com/watch?v=NOLn0QwGlEE&ab_channel=NickChapsas
        TimeSpan.FromMilliseconds(250));
    
    public string ColorCode { get; }
    public string Width { get; }
    public string Weight { get; }
    public string Unit { get; }
    public string Varian { get; }

    protected MaterialAttributes() {}
    
    private MaterialAttributes(string colorCode, string width, string weight, string unit, string varian)
    {
        ColorCode = colorCode;
        Width = width;
        Weight = weight;
        Unit = unit;
        Varian = varian;
    }

    public static Result<MaterialAttributes> Create(
        string colorCode, 
        string width, 
        string weight, 
        string unit, 
        string varian)
    {
        if (string.IsNullOrEmpty(width) || string.IsNullOrWhiteSpace(width))
            return DomainErrors.Material.EmptyWidth;
        if (string.IsNullOrEmpty(unit) || string.IsNullOrWhiteSpace(unit))
            return DomainErrors.Material.EmptyUnit;
        if (string.IsNullOrEmpty(varian) || string.IsNullOrWhiteSpace(varian))
            return DomainErrors.Material.EmptyVarian;

        colorCode = colorCode.Trim();
        width = width.Trim();
        weight = weight.Trim();
        unit = unit.Trim().ToUpper();
        varian = varian.Trim();
        
        return new MaterialAttributes(colorCode, width, weight, unit, varian);
    }

    //because we use value object to check whether two materials are the same, we may don't need to this UniCode function
    // but the problem is that, the other bounded contexts also need to check whether two materials are the same or not
    // maybe we will put this material class in the sharedkernal
    // public string ToUniqueCode()
    // {
    //     return $"{ReplaceSpecialCharacters(Name)}" +
    //            $"_{ReplaceSpecialCharactersWithEmptyData(ColorCode)}" +
    //            $"_{ReplaceSpecialCharacters(Width)}" +
    //            $"_{ReplaceSpecialCharactersWithEmptyData(Weight)}" +
    //            $"_{ReplaceSpecialCharacters(Unit)}" +
    //            $"_{ReplaceSpecialCharacters(Varian)}";
    //     
    //     string ReplaceSpecialCharacters(string data)
    //     {
    //         return UniqueCodePattern.Replace(data, string.Empty);
    //     }
    //     
    //     string ReplaceSpecialCharactersWithEmptyData(string data)
    //     {
    //         return (string.IsNullOrEmpty(data) || string.IsNullOrWhiteSpace(data)) 
    //             ? "000" 
    //             : ReplaceSpecialCharacters(data);
    //     }
    // }
    
    protected override IEnumerable<int> GetHashCodeComponents()
    {
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
