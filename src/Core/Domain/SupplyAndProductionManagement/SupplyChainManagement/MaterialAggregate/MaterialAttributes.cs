using SharedKernel;

namespace Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;

//Should we consider whether this class should be a Value Object or not? - Yes, when we want to know that whether
// two materials are the same or not, we should compare attributes of material from Color, width, weight, unit,
//varian. If all of these attributes are the same, we consider two materials to equal. Name maybe not need live
// in this value object here. if we have two different code and name from different suppliers (each supplier define
// name and code of their material differently) but share the same attributes of materials such as color, width,
// weight, unit, varian, we can treat two materials as the same. So we remove Name from this value object.
//Other bounded contexts can use this value object to compare materials
public record MaterialAttributes : IValueObject
{
    public string ColorCode { get; private init; } = null!;
    public string Width { get; private init;} = null!;
    public string Weight { get; private init;} = null!;
    public string Unit { get; private init;} = null!;
    public string Varian { get; private init;} = null!;

    private MaterialAttributes() { }

    public static Result<MaterialAttributes?> Create(
        string? colorCode, 
        string? width, 
        string? weight, 
        string? unit, 
        string? varian)
    {
        if (string.IsNullOrWhiteSpace(width))
            return DomainErrors.Material.EmptyWidth;
        if (string.IsNullOrWhiteSpace(unit))
            return DomainErrors.Material.EmptyUnit;
        if (string.IsNullOrWhiteSpace(varian))
            return DomainErrors.Material.EmptyVarian;

        colorCode ??= string.Empty;
        weight ??= string.Empty;
        
        colorCode = colorCode.Trim();
        width = width.Trim();
        weight = weight.Trim();
        unit = unit.Trim().ToUpper();
        varian = varian.Trim();

        return new MaterialAttributes
        {
            ColorCode = colorCode,
            Width = width,
            Weight = weight,
            Unit = unit,
            Varian = varian,
        };
    }

    //because we use value object to check whether two materials are the same, we may don't need to this UniCode function
    // but the problem is that, the other bounded contexts also need to check whether two materials are the same or not
    // maybe we will put this material attributes value object in the sharedkernal, and then Other bounded contexts can
    //  use this value object to compare materials
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
}
