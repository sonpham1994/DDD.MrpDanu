using Domain.Extensions;
using Domain.SharedKernel.Extensions.LinqExtensions;
using Domain.SharedKernel.Base;
using Domain.SharedKernel.ValueObjects;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Services.UniqueMaterialCodeServices;
using Domain.Tests.Helpers;
using Domain.Tests.SupplyAndProductionManagement.SupplyChainManagement;
using FluentAssertions;
namespace Domain.Tests.Extensions;

public class LinqExtensionTests
{
    [Fact]
    public void AnyFailure_ExistFailure()
    {
        var array = new List<int> { 2, 4, 5 };
        var result = array.AnyFailure(ExistElement);
        result.IsSuccess.Should().BeFalse();
        result.Error.Code.Should().Be("Fail");
    }

    [Fact]
    public void AnyFailure_NotExistFailure()
    {
        var array = new List<int> { 2, 4, 6 };
        var isSuccess = array.AnyFailure(ExistElement);
        isSuccess.IsSuccess.Should().Be(true);
        isSuccess.Error.IsEmpty().Should().BeTrue();
    }

    [Fact]
    public void ItemDuplication_ExistItemDuplication_ReturnItemDuplication()
    {
        var materialId1 = (MaterialId)Guid.NewGuid();
        var materialId2 = (MaterialId)Guid.NewGuid();
        var materialId3 = (MaterialId)Guid.NewGuid();
        var materialId4 = (MaterialId)Guid.NewGuid();
        var material1 = Material.Create(MaterialCode.Create("code1").Value!, MaterialName.Create("name1").Value!, SupplyChainManagementPreparingData.MaterialAttributes1, MaterialType.Material, RegionalMarket.None, UniqueMaterialCodeResult.Success()).Value!.WithId(materialId1);
        var items = new List<Material?>
        {
            Material.Create(MaterialCode.Create("code2").Value!, MaterialName.Create("name2").Value!,SupplyChainManagementPreparingData.MaterialAttributes1, MaterialType.Material, RegionalMarket.None, UniqueMaterialCodeResult.Success()).Value!.WithId(materialId2),
            material1,
            Material.Create(MaterialCode.Create("code3").Value!, MaterialName.Create("name3").Value!,SupplyChainManagementPreparingData.MaterialAttributes1, MaterialType.Material, RegionalMarket.None, UniqueMaterialCodeResult.Success()).Value!.WithId(materialId3),
            Material.Create(MaterialCode.Create("code4").Value!, MaterialName.Create("name4").Value!,SupplyChainManagementPreparingData.MaterialAttributes1, MaterialType.Material, RegionalMarket.None, UniqueMaterialCodeResult.Success()).Value!.WithId(materialId4),
            material1,
        };

        var result = items.GetEntityDuplication();

        result.Should().NotBeNull();
        result.Should().Be(material1);
    }

    [Fact]
    public void ItemDuplication_NotExistItemDuplication_ReturnNull()
    {
        var materialId1 = (MaterialId)Guid.NewGuid();
        var materialId2 = (MaterialId)Guid.NewGuid();
        var materialId3 = (MaterialId)Guid.NewGuid();
        var materialId4 = (MaterialId)Guid.NewGuid();
        var material1 = Material.Create(MaterialCode.Create("code1").Value!, MaterialName.Create("name1").Value!, SupplyChainManagementPreparingData.MaterialAttributes1, MaterialType.Material, RegionalMarket.None, UniqueMaterialCodeResult.Success()).Value!.WithId(materialId1);
        var items = new List<Material?>
        {
            Material.Create(MaterialCode.Create("code2").Value!, MaterialName.Create("name2").Value!, SupplyChainManagementPreparingData.MaterialAttributes1, MaterialType.Material, RegionalMarket.None, UniqueMaterialCodeResult.Success()).Value!.WithId(materialId2),
            material1,
            Material.Create(MaterialCode.Create("code3").Value!, MaterialName.Create("name3").Value!,SupplyChainManagementPreparingData.MaterialAttributes1, MaterialType.Material, RegionalMarket.None, UniqueMaterialCodeResult.Success()).Value!.WithId(materialId3),
            Material.Create(MaterialCode.Create("code4").Value!, MaterialName.Create("name4").Value!,SupplyChainManagementPreparingData.MaterialAttributes1, MaterialType.Material, RegionalMarket.None, UniqueMaterialCodeResult.Success()).Value!.WithId(materialId4),
        };

        var result = items.GetEntityDuplication();

        result.Should().BeNull();
    }

    [Fact]
    public void ItemDuplication_NullItemAndNoneItemDuplication_ReturnNull()
    {
        var materialId1 = (MaterialId)Guid.NewGuid();
        var materialId2 = (MaterialId)Guid.NewGuid();
        var material1 = Material.Create(MaterialCode.Create("code1").Value!, MaterialName.Create("name1").Value!, SupplyChainManagementPreparingData.MaterialAttributes1, MaterialType.Material, RegionalMarket.None, UniqueMaterialCodeResult.Success()).Value!.WithId(materialId1);
        var items = new List<Material?>
        {
            Material.Create(MaterialCode.Create("code2").Value!, MaterialName.Create("name1").Value!,SupplyChainManagementPreparingData.MaterialAttributes1, MaterialType.Material, RegionalMarket.None, UniqueMaterialCodeResult.Success()).Value!.WithId(materialId2),
            material1,
            null,
            null
        };

        var result = items.GetEntityDuplication();

        result.Should().BeNull();
    }

    [Fact]
    public void ItemDuplication_NullItemAndItemDuplication_ReturnItemDuplication()
    {
        var materialId1 = (MaterialId)Guid.NewGuid();
        var materialId2 = (MaterialId)Guid.NewGuid();
        var materialId3 = (MaterialId)Guid.NewGuid();
        var material1 = Material.Create(MaterialCode.Create("code1").Value!, MaterialName.Create("name1").Value!, SupplyChainManagementPreparingData.MaterialAttributes1, MaterialType.Material, RegionalMarket.None, UniqueMaterialCodeResult.Success()).Value!.WithId(materialId1);
        var items = new List<Material?>
        {
            Material.Create(MaterialCode.Create("code2").Value!, MaterialName.Create("name1").Value!,SupplyChainManagementPreparingData.MaterialAttributes1, MaterialType.Material, RegionalMarket.None, UniqueMaterialCodeResult.Success()).Value!.WithId(materialId2),
            material1,
            Material.Create(MaterialCode.Create("code3").Value!, MaterialName.Create("name1").Value!,SupplyChainManagementPreparingData.MaterialAttributes1, MaterialType.Material, RegionalMarket.None, UniqueMaterialCodeResult.Success()).Value!.WithId(materialId3),
            null,
            material1
        };

        var result = items.GetEntityDuplication();

        result.Should().NotBeNull();
        result.Should().Be(material1);
    }

    [Fact]
    public void ItemDuplication_NullItemDuplicationAndItemDuplication_ReturnItemDuplication()
    {
        var materialId1 = (MaterialId)Guid.NewGuid();
        var materialId2 = (MaterialId)Guid.NewGuid();
        var materialId3 = (MaterialId)Guid.NewGuid();
        var material1 = Material.Create(MaterialCode.Create("code1").Value!, MaterialName.Create("name1").Value!, SupplyChainManagementPreparingData.MaterialAttributes1, MaterialType.Material, RegionalMarket.None, UniqueMaterialCodeResult.Success()).Value!.WithId(materialId1);
        var items = new List<Material?>
        {
            null,
            Material.Create(MaterialCode.Create("code2").Value!, MaterialName.Create("name1").Value!,SupplyChainManagementPreparingData.MaterialAttributes1, MaterialType.Material, RegionalMarket.None, UniqueMaterialCodeResult.Success()).Value!.WithId(materialId2),
            material1,
            Material.Create(MaterialCode.Create("code3").Value!, MaterialName.Create("name1").Value!,SupplyChainManagementPreparingData.MaterialAttributes1, MaterialType.Material, RegionalMarket.None, UniqueMaterialCodeResult.Success()).Value!.WithId(materialId3),
            null,
            material1
        };

        var result = items.GetEntityDuplication();

        result.Should().NotBeNull();
        result.Should().Be(material1);
    }
    
    [Fact]
    public void PropertyValueObjectDuplication_ExistItemDuplication_ReturnItemDuplication()
    {
        var items = new List<Material?>
        {
            Material.Create(MaterialCode.Create("code2").Value!, MaterialName.Create("name2").Value!,SupplyChainManagementPreparingData.MaterialAttributes1, MaterialType.Material, RegionalMarket.None, UniqueMaterialCodeResult.Success()).Value!,
            Material.Create(MaterialCode.Create("code3").Value!, MaterialName.Create("name3").Value!,SupplyChainManagementPreparingData.MaterialAttributes2, MaterialType.Material, RegionalMarket.None, UniqueMaterialCodeResult.Success()).Value!,
            Material.Create(MaterialCode.Create("code4").Value!, MaterialName.Create("name4").Value!,SupplyChainManagementPreparingData.MaterialAttributes1, MaterialType.Material, RegionalMarket.None, UniqueMaterialCodeResult.Success()).Value!,
        };

        var result = items.GetItemDuplication(x => x!.Attributes);

        result.Should().NotBeNull();
        result.Should().Be(items[0]!.Attributes);
    }

    [Fact]
    public void PropertyValueObjectDuplication_NotExistItemDuplication_ReturnNull()
    {
        var materialId2 = (MaterialId)Guid.NewGuid();
        var materialId4 = (MaterialId)Guid.NewGuid();
        var items = new List<Material?>
        {
            Material.Create(MaterialCode.Create("code2").Value!, MaterialName.Create("name2").Value!, SupplyChainManagementPreparingData.MaterialAttributes1, MaterialType.Material, RegionalMarket.None, UniqueMaterialCodeResult.Success()).Value!.WithId(materialId2),
            Material.Create(MaterialCode.Create("code4").Value!, MaterialName.Create("name4").Value!,SupplyChainManagementPreparingData.MaterialAttributes2, MaterialType.Material, RegionalMarket.None, UniqueMaterialCodeResult.Success()).Value!.WithId(materialId4)
        };

        var result = items.GetItemDuplication(x => x!.Attributes);
    
        result.Should().BeNull();
    }
    
    [Fact]
    public void PropertyValueObjectDuplication_NullItemAndNoneItemDuplication_ReturnNull()
    {
        var materialId1 = (MaterialId)Guid.NewGuid();
        var materialId2 = (MaterialId)Guid.NewGuid();
        var items = new List<Material?>
        {
            Material.Create(MaterialCode.Create("code2").Value!, MaterialName.Create("name1").Value!,SupplyChainManagementPreparingData.MaterialAttributes2, MaterialType.Material, RegionalMarket.None, UniqueMaterialCodeResult.Success()).Value!.WithId(materialId2),
            Material.Create(MaterialCode.Create("code1").Value!, MaterialName.Create("name1").Value!, SupplyChainManagementPreparingData.MaterialAttributes1, MaterialType.Material, RegionalMarket.None, UniqueMaterialCodeResult.Success()).Value!.WithId(materialId1),
            null,
            null
        };

        var result = items.GetItemDuplication(x => x!.Attributes);
    
        result.Should().BeNull();
    }
    
    [Fact]
    public void PropertyValueObjectDuplication_NullItemAndItemDuplication_ReturnItemDuplication()
    {
        var materialId2 = (MaterialId)Guid.NewGuid();
        var materialId3 = (MaterialId)Guid.NewGuid();
        var items = new List<Material?>
        {
            Material.Create(MaterialCode.Create("code3").Value!, MaterialName.Create("name1").Value!,SupplyChainManagementPreparingData.MaterialAttributes1, MaterialType.Material, RegionalMarket.None, UniqueMaterialCodeResult.Success()).Value!.WithId(materialId3),
            Material.Create(MaterialCode.Create("code2").Value!, MaterialName.Create("name1").Value!,SupplyChainManagementPreparingData.MaterialAttributes2, MaterialType.Material, RegionalMarket.None, UniqueMaterialCodeResult.Success()).Value!.WithId(materialId2),
            null,
            Material.Create(MaterialCode.Create("code3").Value!, MaterialName.Create("name1").Value!,SupplyChainManagementPreparingData.MaterialAttributes1, MaterialType.Material, RegionalMarket.None, UniqueMaterialCodeResult.Success()).Value!.WithId(materialId3),
        };

        var result = items.GetItemDuplication(x => x!.Attributes);
    
        result.Should().NotBeNull();
        result.Should().Be(items[0]!.Attributes);
    }
    
    [Fact]
    public void PropertyValueObjectDuplication_NullItemDuplicationAndItemDuplication_ReturnItemDuplication()
    {
        var materialId1 = (MaterialId)Guid.NewGuid();
        var materialId2 = (MaterialId)Guid.NewGuid();
        var materialId3 = (MaterialId)Guid.NewGuid();
        var material1 = Material.Create(MaterialCode.Create("code1").Value!, MaterialName.Create("name1").Value!, SupplyChainManagementPreparingData.MaterialAttributes1, MaterialType.Material, RegionalMarket.None, UniqueMaterialCodeResult.Success()).Value!.WithId(materialId1);
        var items = new List<Material?>
        {
            null,
            Material.Create(MaterialCode.Create("code2").Value!, MaterialName.Create("name1").Value!,SupplyChainManagementPreparingData.MaterialAttributes2, MaterialType.Material, RegionalMarket.None, UniqueMaterialCodeResult.Success()).Value!.WithId(materialId2),
            null,
            material1,
            null,
            Material.Create(MaterialCode.Create("code3").Value!, MaterialName.Create("name1").Value!,SupplyChainManagementPreparingData.MaterialAttributes1, MaterialType.Material, RegionalMarket.None, UniqueMaterialCodeResult.Success()).Value!.WithId(materialId3),
        };

        var result = items.GetItemDuplication(x => x!.Attributes);
    
        result.Should().NotBeNull();
        result.Should().Be(material1.Attributes);
    }
    
    private Result ExistElement(int id)
    {
        var array = new List<int> { 1, 2, 3, 4, 6 };
        var exist = array.Any(x => x == id);
        if (!exist)
            return Result.Failure(new DomainError("Fail", "Fail Message"));

        return Result.Success();
    }
}