using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmark.Infrastructure.EnumerationLoading.Setups;

public class Material
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public string CodeUnique { get; set; }
    public string Name { get; set; }
    public string ColorCode { get; set; }
    public string Width { get; set; }
    public string Weight { get; set; }
    public string Unit { get; set; }
    public string Varian { get; set; }
    public virtual MaterialType MaterialType { get; set; }
    public virtual RegionalMarket RegionalMarket { get; set; }
}

public class MaterialType
{
    public byte Id { get; set; }
    public string Name { get; set; }
}

public static class MaterialTypeList
{
    public static readonly List<MaterialType> List = new List<MaterialType>
    {
        new MaterialType { Id = 1, Name = "Material" },
        new MaterialType{ Id = 2, Name = "Subassemblies" }
    };

    public static MaterialType FromId(byte id)
    {
        return List.FirstOrDefault(x => x.Id == id);
    }
}

public class RegionalMarket
{
    public byte Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }


}

public static class RegionalMarketList
{
    public static readonly List<RegionalMarket> List = new List<RegionalMarket>
    {
        new RegionalMarket{Id = 1,  Code = "None", Name = "None" },
        new RegionalMarket{Id=3, Code = "CN", Name = "China"}
    };

    public static RegionalMarket FromId(byte id)
    {
        return List.FirstOrDefault(x => x.Id == id);
    }
}