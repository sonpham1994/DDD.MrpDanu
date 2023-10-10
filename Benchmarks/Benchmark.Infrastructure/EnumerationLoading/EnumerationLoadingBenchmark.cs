using Benchmark.Infrastructure.EnumerationLoading.Setups;
using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Benchmark.Infrastructure.EnumerationLoading;

[MemoryDiagnoser]
public class EnumerationLoadingBenchmark
{
    public const string ConnectionString = "Server=son-quang-pham-0131,1433;Database=DDD.MrpDanu;User Id=sa;Password=Vsa*12345#;TrustServerCertificate=true";
    public static readonly Guid Id = new Guid("c011706a-d3e0-4a9c-758b-08db70ae0176");

    [GlobalSetup]
    public void Setup()
    {
        var a = MaterialTypeList.List;
        var b = MaterialTypeList.List;
    }

    [Benchmark]
    public void GetMaterialWithLazyLoading()
    {
        using var dbContext = new AppDbContext(ConnectionString);
        var material = dbContext.Materials.FirstOrDefault(x => x.Id == Id);

        dbContext.Materials.Entry(material).Reference(x => x.MaterialType).Load();
        dbContext.Materials.Entry(material).Reference(x => x.RegionalMarket).Load();
    }

    [Benchmark]
    public void GetMaterialWithoutLazyLoadingAndUseEnumeration()
    {
        using var dbContext = new AppDbContext(ConnectionString);
        var material = dbContext.Materials.FirstOrDefault(x => x.Id == Id);

        var materialTypeId = dbContext.Entry(material).Property<byte>("MaterialTypeId").CurrentValue;
        var regionalMarketId = dbContext.Entry(material).Property<byte>("RegionalMarketId").CurrentValue;

        typeof(Material).GetProperty(nameof(Material.MaterialType))!.SetValue(material, MaterialTypeList.FromId(materialTypeId), null);
        typeof(Material).GetProperty(nameof(Material.RegionalMarket))!.SetValue(material, RegionalMarketList.FromId(regionalMarketId), null);
    }

    [Benchmark]
    public void GetMaterialWithLazyLoadingTwice()
    {
        using (var dbContext = new AppDbContext(ConnectionString))
        {
            var material = dbContext.Materials.FirstOrDefault(x => x.Id == Id);

            dbContext.Materials.Entry(material).Reference(x => x.MaterialType).Load();
            dbContext.Materials.Entry(material).Reference(x => x.RegionalMarket).Load();
        }

        using (var dbContext = new AppDbContext(ConnectionString))
        {
            var material = dbContext.Materials.FirstOrDefault(x => x.Id == Id);

            dbContext.Materials.Entry(material).Reference(x => x.MaterialType).Load();
            dbContext.Materials.Entry(material).Reference(x => x.RegionalMarket).Load();
        }
    }

    [Benchmark]
    public void GetMaterialWithoutLazyLoadingAndUseEnumerationTwice()
    {
        using (var dbContext = new AppDbContext(ConnectionString))
        {
            var material = dbContext.Materials.FirstOrDefault(x => x.Id == Id);

            var materialTypeId = dbContext.Entry(material).Property<byte>("MaterialTypeId").CurrentValue;
            var regionalMarketId = dbContext.Entry(material).Property<byte>("RegionalMarketId").CurrentValue;

            typeof(Material).GetProperty(nameof(Material.MaterialType))!.SetValue(material, MaterialTypeList.FromId(materialTypeId), null);
            typeof(Material).GetProperty(nameof(Material.RegionalMarket))!.SetValue(material, RegionalMarketList.FromId(regionalMarketId), null);
        }

        using (var dbContext = new AppDbContext(ConnectionString))
        {
            var material = dbContext.Materials.FirstOrDefault(x => x.Id == Id);

            var materialTypeId = dbContext.Entry(material).Property<byte>("MaterialTypeId").CurrentValue;
            var regionalMarketId = dbContext.Entry(material).Property<byte>("RegionalMarketId").CurrentValue;

            typeof(Material).GetProperty(nameof(Material.MaterialType))!.SetValue(material, MaterialTypeList.FromId(materialTypeId), null);
            typeof(Material).GetProperty(nameof(Material.RegionalMarket))!.SetValue(material, RegionalMarketList.FromId(regionalMarketId), null);
        }
    }
}
