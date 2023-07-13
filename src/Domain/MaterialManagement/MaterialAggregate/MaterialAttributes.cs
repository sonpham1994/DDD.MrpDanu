﻿using Domain.Errors;
using System.Text.RegularExpressions;
using Domain.SharedKernel.Base;

namespace Domain.MaterialManagement.MaterialAggregate;

//Should we consider whether this class should be a Value Object or not?
public class MaterialAttributes : ValueObject
{
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
        string ReplaceSpecialCharacters(string data)
        {
            return Regex.Replace(data, "[^A-Za-z0-9]", string.Empty);
        }
        
        string ReplaceSpecialCharactersWithEmptyData(string data)
        {
            return (string.IsNullOrEmpty(data) || string.IsNullOrWhiteSpace(data)) 
                ? "000" 
                : ReplaceSpecialCharacters(data);
        }

        return $"{ReplaceSpecialCharacters(Name)}" +
               $"_{ReplaceSpecialCharactersWithEmptyData(ColorCode)}" +
               $"_{ReplaceSpecialCharacters(Width)}" +
               $"_{ReplaceSpecialCharactersWithEmptyData(Weight)}" +
               $"_{ReplaceSpecialCharacters(Unit)}" +
               $"_{ReplaceSpecialCharacters(Varian)}";
    }
    
    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Name;
        yield return Varian;
        yield return Width;
        yield return Weight;
        yield return Unit;
        yield return ColorCode;
    }
}
