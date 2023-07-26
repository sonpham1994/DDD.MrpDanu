using Domain.Extensions;
using Domain.MaterialManagement.MaterialAggregate;
using Domain.SharedKernel.Base;
using Domain.Tests.MaterialManagement;
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
        isSuccess.Error.Should().Be(DomainError.Empty);
    }

    [Fact]
    public void ItemDuplication_ExistItemDuplication_ReturnItemDuplication()
    {
        var materialId1 = Guid.NewGuid();
        var materialId2 = Guid.NewGuid();
        var materialId3 = Guid.NewGuid();
        var materialId4 = Guid.NewGuid();
        var material1 = Material.Create("code1", MaterialManagementPreparingData.MaterialAttributes1, MaterialType.Material, RegionalMarket.None).Value.WithId(materialId1);
        var items = new List<Material>
        {
            Material.Create("code2", MaterialManagementPreparingData.MaterialAttributes1, MaterialType.Material, RegionalMarket.None).Value.WithId(materialId2),
            material1,
            Material.Create("code3", MaterialManagementPreparingData.MaterialAttributes1, MaterialType.Material, RegionalMarket.None).Value.WithId(materialId3),
            Material.Create("code4", MaterialManagementPreparingData.MaterialAttributes1, MaterialType.Material, RegionalMarket.None).Value.WithId(materialId4),
            material1,
        };

        var result = items.ItemDuplication(x => x);

        result.Should().NotBeNull();
        result.Should().Be(material1);
    }

    [Fact]
    public void ItemDuplication_NotExistItemDuplication_ReturnNull()
    {
        var materialId1 = Guid.NewGuid();
        var materialId2 = Guid.NewGuid();
        var materialId3 = Guid.NewGuid();
        var materialId4 = Guid.NewGuid();
        var material1 = Material.Create("code1", MaterialManagementPreparingData.MaterialAttributes1, MaterialType.Material, RegionalMarket.None).Value.WithId(materialId1);
        var items = new List<Material>
        {
            Material.Create("code2", MaterialManagementPreparingData.MaterialAttributes1, MaterialType.Material, RegionalMarket.None).Value.WithId(materialId2),
            material1,
            Material.Create("code3", MaterialManagementPreparingData.MaterialAttributes1, MaterialType.Material, RegionalMarket.None).Value.WithId(materialId3),
            Material.Create("code4", MaterialManagementPreparingData.MaterialAttributes1, MaterialType.Material, RegionalMarket.None).Value.WithId(materialId4),
        };

        var result = items.ItemDuplication(x => x);

        result.Should().BeNull();
    }

    [Fact]
    public void ItemDuplication_NullItemAndNoneItemDuplication_ReturnNull()
    {
        var materialId1 = Guid.NewGuid();
        var materialId2 = Guid.NewGuid();
        var materialId3 = Guid.NewGuid();
        var materialId4 = Guid.NewGuid();
        var material1 = Material.Create("code1", MaterialManagementPreparingData.MaterialAttributes1, MaterialType.Material, RegionalMarket.None).Value.WithId(materialId1);
        var items = new List<Material>
        {
            Material.Create("code2", MaterialManagementPreparingData.MaterialAttributes1, MaterialType.Material, RegionalMarket.None).Value.WithId(materialId2),
            material1,
            null,
            null
        };

        var result = items.ItemDuplication(x => x);

        result.Should().BeNull();
    }

    [Fact]
    public void ItemDuplication_NullItemAndItemDuplication_ReturnItemDuplication()
    {
        var materialId1 = Guid.NewGuid();
        var materialId2 = Guid.NewGuid();
        var materialId3 = Guid.NewGuid();
        var materialId4 = Guid.NewGuid();
        var material1 = Material.Create("code1", MaterialManagementPreparingData.MaterialAttributes1, MaterialType.Material, RegionalMarket.None).Value.WithId(materialId1);
        var items = new List<Material>
        {
            Material.Create("code2", MaterialManagementPreparingData.MaterialAttributes1, MaterialType.Material, RegionalMarket.None).Value.WithId(materialId2),
            material1,
            Material.Create("code3", MaterialManagementPreparingData.MaterialAttributes1, MaterialType.Material, RegionalMarket.None).Value.WithId(materialId3),
            null,
            material1
        };

        var result = items.ItemDuplication(x => x);

        result.Should().NotBeNull();
        result.Should().Be(material1);
    }

    [Fact]
    public void ItemDuplication_NullItemDuplicationAndItemDuplication_ReturnItemDuplication()
    {
        var materialId1 = Guid.NewGuid();
        var materialId2 = Guid.NewGuid();
        var materialId3 = Guid.NewGuid();
        var materialId4 = Guid.NewGuid();
        var material1 = Material.Create("code1", MaterialManagementPreparingData.MaterialAttributes1, MaterialType.Material, RegionalMarket.None).Value.WithId(materialId1);
        var items = new List<Material>
        {
            null,
            Material.Create("code2", MaterialManagementPreparingData.MaterialAttributes1, MaterialType.Material, RegionalMarket.None).Value.WithId(materialId2),
            material1,
            Material.Create("code3", MaterialManagementPreparingData.MaterialAttributes1, MaterialType.Material, RegionalMarket.None).Value.WithId(materialId3),
            null,
            material1
        };

        var result = items.ItemDuplication(x => x);

        result.Should().NotBeNull();
        result.Should().Be(material1);
    }

    private IResult ExistElement(int id)
    {
        var array = new List<int> { 1, 2, 3, 4, 6 };
        var exist = array.Any(x => x == id);
        if (!exist)
            return Result.Failure(new DomainError("Fail", "Fail Message"));

        return Result.Success();
    }
}