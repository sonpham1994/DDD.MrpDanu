using Domain.Extensions;
using Domain.SharedKernel.Base;
using Domain.SharedKernel.ValueObjects;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Services.UniqueMaterialCodeServices;
using Domain.Tests.SupplyAndProductionManagement.SupplyChainManagement;
using FluentAssertions;
namespace Domain.Tests.Extensions;

public class LinqExtensionTests
{
    [Fact]
    public void AnyFailure_ExistFailure()
    {
        var array = new List<int> { 2, 4, 5 };
        var isFail = array.AnyFailure(ExistElement);
        isFail.IsFailure.Should().Be(true);
        isFail.Error.Code.Should().Be("Fail");
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
        var material1 = Material.Create("code1", "name1", SupplyChainManagementPreparingData.MaterialAttributes1, MaterialType.Material, RegionalMarket.None, UniqueMaterialCodeResult.Success()).Value.WithId(materialId1);
        var items = new List<Material>
        {
            Material.Create("code2", "name2",SupplyChainManagementPreparingData.MaterialAttributes1, MaterialType.Material, RegionalMarket.None, UniqueMaterialCodeResult.Success()).Value.WithId(materialId2),
            material1,
            Material.Create("code3", "name3",SupplyChainManagementPreparingData.MaterialAttributes1, MaterialType.Material, RegionalMarket.None, UniqueMaterialCodeResult.Success()).Value.WithId(materialId3),
            Material.Create("code4", "name4",SupplyChainManagementPreparingData.MaterialAttributes1, MaterialType.Material, RegionalMarket.None, UniqueMaterialCodeResult.Success()).Value.WithId(materialId4),
            material1,
        };

        var result = items.ItemDuplication<Material, MaterialId, Material>(x => x);

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
        var material1 = Material.Create("code1", "name1", SupplyChainManagementPreparingData.MaterialAttributes1, MaterialType.Material, RegionalMarket.None, UniqueMaterialCodeResult.Success()).Value.WithId(materialId1);
        var items = new List<Material>
        {
            Material.Create("code2", "name2", SupplyChainManagementPreparingData.MaterialAttributes1, MaterialType.Material, RegionalMarket.None, UniqueMaterialCodeResult.Success()).Value.WithId(materialId2),
            material1,
            Material.Create("code3", "name3",SupplyChainManagementPreparingData.MaterialAttributes1, MaterialType.Material, RegionalMarket.None, UniqueMaterialCodeResult.Success()).Value.WithId(materialId3),
            Material.Create("code4", "name4",SupplyChainManagementPreparingData.MaterialAttributes1, MaterialType.Material, RegionalMarket.None, UniqueMaterialCodeResult.Success()).Value.WithId(materialId4),
        };

        var result = items.ItemDuplication<Material, MaterialId, Material>(x => x);

        result.Should().BeNull();
    }

    [Fact]
    public void ItemDuplication_NullItemAndNoneItemDuplication_ReturnNull()
    {
        var materialId1 = (MaterialId)Guid.NewGuid();
        var materialId2 = (MaterialId)Guid.NewGuid();
        var material1 = Material.Create("code1", "name1", SupplyChainManagementPreparingData.MaterialAttributes1, MaterialType.Material, RegionalMarket.None, UniqueMaterialCodeResult.Success()).Value.WithId(materialId1);
        var items = new List<Material>
        {
            Material.Create("code2", "name1",SupplyChainManagementPreparingData.MaterialAttributes1, MaterialType.Material, RegionalMarket.None, UniqueMaterialCodeResult.Success()).Value.WithId(materialId2),
            material1,
            null,
            null
        };

        var result = items.ItemDuplication<Material, MaterialId, Material>(x => x);

        result.Should().BeNull();
    }

    [Fact]
    public void ItemDuplication_NullItemAndItemDuplication_ReturnItemDuplication()
    {
        var materialId1 = (MaterialId)Guid.NewGuid();
        var materialId2 = (MaterialId)Guid.NewGuid();
        var materialId3 = (MaterialId)Guid.NewGuid();
        var material1 = Material.Create("code1", "name1", SupplyChainManagementPreparingData.MaterialAttributes1, MaterialType.Material, RegionalMarket.None, UniqueMaterialCodeResult.Success()).Value.WithId(materialId1);
        var items = new List<Material>
        {
            Material.Create("code2", "name1",SupplyChainManagementPreparingData.MaterialAttributes1, MaterialType.Material, RegionalMarket.None, UniqueMaterialCodeResult.Success()).Value.WithId(materialId2),
            material1,
            Material.Create("code3", "name1",SupplyChainManagementPreparingData.MaterialAttributes1, MaterialType.Material, RegionalMarket.None, UniqueMaterialCodeResult.Success()).Value.WithId(materialId3),
            null,
            material1
        };

        var result = items.ItemDuplication<Material, MaterialId, Material>(x => x);

        result.Should().NotBeNull();
        result.Should().Be(material1);
    }

    [Fact]
    public void ItemDuplication_NullItemDuplicationAndItemDuplication_ReturnItemDuplication()
    {
        var materialId1 = (MaterialId)Guid.NewGuid();
        var materialId2 = (MaterialId)Guid.NewGuid();
        var materialId3 = (MaterialId)Guid.NewGuid();
        var material1 = Material.Create("code1", "name1", SupplyChainManagementPreparingData.MaterialAttributes1, MaterialType.Material, RegionalMarket.None, UniqueMaterialCodeResult.Success()).Value.WithId(materialId1);
        var items = new List<Material>
        {
            null,
            Material.Create("code2", "name1",SupplyChainManagementPreparingData.MaterialAttributes1, MaterialType.Material, RegionalMarket.None, UniqueMaterialCodeResult.Success()).Value.WithId(materialId2),
            material1,
            Material.Create("code3", "name1",SupplyChainManagementPreparingData.MaterialAttributes1, MaterialType.Material, RegionalMarket.None, UniqueMaterialCodeResult.Success()).Value.WithId(materialId3),
            null,
            material1
        };

        var result = items.ItemDuplication<Material, MaterialId, Material>(x => x);

        result.Should().NotBeNull();
        result.Should().Be(material1);
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