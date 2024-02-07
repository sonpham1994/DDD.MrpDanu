using Domain.SharedKernel.Base;
using Domain.SharedKernel.Enumerations;
using Domain.SharedKernel.ValueObjects;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Services.UniqueMaterialCodeServices;
using FluentAssertions;
using DomainErrors = Domain.SupplyAndProductionManagement.SupplyChainManagement.DomainErrors;

namespace Domain.Tests.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;

public class MaterialTests
{
    [Fact]
    public void Cannot_create_material_with_material_type_and_not_none_regional_market()
    {
        var materialAttributes = SupplyChainManagementPreparingData.MaterialAttributes1;
        var material = Material.Create("code1", "name1", materialAttributes, MaterialType.Material, RegionalMarket.Florida, (UniqueMaterialCodeResult)Result.Success());

        material.IsFailure.Should().Be(true);
        material.Error.Should().Be(DomainErrors.Material.InvalidMaterialType);
    }

    [Fact]
    public void Cannot_create_material_with_subassemblies_type_and_none_regional_market()
    {
        var materialAttributes = SupplyChainManagementPreparingData.MaterialAttributes1;
        var material = Material.Create("code1", "name1", materialAttributes, MaterialType.Subassemblies, RegionalMarket.None, (UniqueMaterialCodeResult)Result.Success());

        material.IsFailure.Should().Be(true);
        material.Error.Should().Be(DomainErrors.Material.InvalidSubassembliesType);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Cannot_create_material_with_null_or_empty_code(string code)
    {
        var materialAttributes = SupplyChainManagementPreparingData.MaterialAttributes1;
        var material = Material.Create(code, "name1", materialAttributes, MaterialType.Subassemblies, RegionalMarket.Florida, (UniqueMaterialCodeResult)Result.Success());

        material.IsFailure.Should().Be(true);
        material.Error.Should().Be(DomainErrors.Material.EmptyCode);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Cannot_create_material_with_null_or_empty_name(string name)
    {
        var materialAttributes = SupplyChainManagementPreparingData.MaterialAttributes1;
        var material = Material.Create("code1", name, materialAttributes, MaterialType.Subassemblies, RegionalMarket.Florida, (UniqueMaterialCodeResult)Result.Success());

        material.IsFailure.Should().Be(true);
        material.Error.Should().Be(DomainErrors.Material.EmptyName);
    }

    [Fact]
    public async Task Cannot_create_material_if_unique_material_code_exists()
    {
        string code = "code1";
        var materialAttributes = SupplyChainManagementPreparingData.MaterialAttributes1;
        Func<string, CancellationToken, Task<MaterialIdWithCode>> getMaterialByCode = (code, cancellationToken) =>
        {
            var materialIdWithCode1 = new MaterialIdWithCode
            {
                Id = SupplyChainManagementPreparingData.MaterialId1.Value,
                Code = code
            };

            return Task.FromResult(materialIdWithCode1);
        };

        var uniqueMaterialCodeResult = await UniqueMaterialCodeService.CheckUniqueMaterialCodeAsync(code, getMaterialByCode, default);
        var material = Material
            .Create(
                code,
                "name1",
                materialAttributes,
                MaterialType.Subassemblies,
                RegionalMarket.Florida,
                uniqueMaterialCodeResult
            );

        material.IsFailure.Should().BeTrue();
        material.Error.Should().Be(DomainErrors.Material.ExistedCode(code, SupplyChainManagementPreparingData.MaterialId1));
    }

    [Fact]
    public async Task Create_material_if_unique_material_code_is_empty()
    {
        string code = "code1";
        var materialAttributes = SupplyChainManagementPreparingData.MaterialAttributes1;
        Func<string, CancellationToken, Task<MaterialIdWithCode>> getMaterialByCode = (code, cancellationToken) =>
        {
            return Task.FromResult(default(MaterialIdWithCode));
        };

        var uniqueMaterialCodeResult = await UniqueMaterialCodeService.CheckUniqueMaterialCodeAsync(code, getMaterialByCode, default);
        var material = Material
            .Create(
                code,
                "name1",
                materialAttributes,
                MaterialType.Subassemblies,
                RegionalMarket.Florida,
                uniqueMaterialCodeResult
            );

        material.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public void Create_material_with_subassemblies_type_successfully()
    {
        string code = "code1";
        var materialAttributes = SupplyChainManagementPreparingData.MaterialAttributes1;
        var material = Material.Create($" {code} ", "name1", materialAttributes, MaterialType.Subassemblies, RegionalMarket.Florida, (UniqueMaterialCodeResult)Result.Success());

        material.IsSuccess.Should().Be(true);
        material.Value.Attributes.Should().Be(materialAttributes);
        material.Value.Code.Should().Be(code);
        material.Value.MaterialType.Should().Be(MaterialType.Subassemblies);
        material.Value.RegionalMarket.Should().Be(RegionalMarket.Florida);
    }

    [Fact]
    public void Create_material_with_material_type_successfully()
    {
        string code = "code1";
        var materialAttributes = SupplyChainManagementPreparingData.MaterialAttributes1;
        var material = Material.Create($" {code} ", "name1", materialAttributes, MaterialType.Material, RegionalMarket.None, (UniqueMaterialCodeResult)Result.Success());

        material.IsSuccess.Should().BeTrue();
        material.Value.Attributes.Should().Be(materialAttributes);
        material.Value.Code.Should().Be(code);
        material.Value.MaterialType.Should().Be(MaterialType.Material);
        material.Value.RegionalMarket.Should().Be(RegionalMarket.None);
    }

    [Fact]
    public void Cannot_update_material_with_material_type_and_not_none_regional_market()
    {
        var materialAttributes = SupplyChainManagementPreparingData.MaterialAttributes1;
        var material = Material.Create("code1", "name1", materialAttributes, MaterialType.Material, RegionalMarket.None, (UniqueMaterialCodeResult)Result.Success());

        var result = material.Value.UpdateMaterial("code1", "name1", materialAttributes, MaterialType.Material, RegionalMarket.Florida, (UniqueMaterialCodeResult)Result.Success());

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.Material.InvalidMaterialType);
    }

    [Fact]
    public void Cannot_update_material_with_subassemblies_type_and_none_regional_market()
    {
        var materialAttributes = SupplyChainManagementPreparingData.MaterialAttributes1;
        var material = Material.Create("code1", "name1", materialAttributes, MaterialType.Subassemblies, RegionalMarket.Florida, (UniqueMaterialCodeResult)Result.Success());

        var result = material.Value.UpdateMaterial("code1", "name1", materialAttributes, MaterialType.Subassemblies, RegionalMarket.None, (UniqueMaterialCodeResult)Result.Success());

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.Material.InvalidSubassembliesType);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Cannot_update_material_with_null_or_empty_code(string code)
    {
        var materialAttributes = SupplyChainManagementPreparingData.MaterialAttributes1;
        var material = Material.Create("code1", "name1", materialAttributes, MaterialType.Subassemblies, RegionalMarket.Florida, (UniqueMaterialCodeResult)Result.Success());

        var result = material.Value.UpdateMaterial(code, "name1", materialAttributes, MaterialType.Subassemblies, RegionalMarket.Florida, (UniqueMaterialCodeResult)Result.Success());

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.Material.EmptyCode);
    }

    [Fact]
    public async Task Cannot_update_material_if_unique_material_code_exists_on_another_material()
    {
        string code = "code2";
        var materialAttributes = SupplyChainManagementPreparingData.MaterialAttributes1;
        var material = Material.Create("code1", "name1", materialAttributes, MaterialType.Subassemblies, RegionalMarket.Florida, (UniqueMaterialCodeResult)Result.Success());
        material.Value.WithId(SupplyChainManagementPreparingData.MaterialId1);
        Func<string, CancellationToken, Task<MaterialIdWithCode>> getMaterialByCode = (code, cancellationToken) =>
        {
            var materialIdWithCode1 = new MaterialIdWithCode
            {
                Id = SupplyChainManagementPreparingData.MaterialId2.Value,
                Code = code
            };

            return Task.FromResult(materialIdWithCode1);
        };
        var uniqueMaterialCodeResult = await UniqueMaterialCodeService.CheckUniqueMaterialCodeAsync(material.Value.Id, code, getMaterialByCode, default);

        var result = material.Value.UpdateMaterial(code, "name1", materialAttributes, MaterialType.Subassemblies, RegionalMarket.None, uniqueMaterialCodeResult);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.Material.ExistedCode(code, SupplyChainManagementPreparingData.MaterialId2));
    }

    [Fact]
    public async Task Update_material_successfully_if_unique_material_code_exists_on_the_same_material()
    {
        string code = "code2";
        var materialAttributes = SupplyChainManagementPreparingData.MaterialAttributes1;
        var material = Material.Create("code1", "name1", materialAttributes, MaterialType.Subassemblies, RegionalMarket.Florida, (UniqueMaterialCodeResult)Result.Success());
        material.Value.WithId(SupplyChainManagementPreparingData.MaterialId1);
        Func<string, CancellationToken, Task<MaterialIdWithCode>> getMaterialByCode = (code, cancellationToken) =>
        {
            var materialIdWithCode1 = new MaterialIdWithCode
            {
                Id = SupplyChainManagementPreparingData.MaterialId1.Value,
                Code = code
            };

            return Task.FromResult(materialIdWithCode1);
        };
        var uniqueMaterialCodeResult = await UniqueMaterialCodeService.CheckUniqueMaterialCodeAsync(material.Value.Id, code, getMaterialByCode, default);

        var result = material.Value.UpdateMaterial(code, "name1", materialAttributes, MaterialType.Subassemblies, RegionalMarket.Florida, uniqueMaterialCodeResult);

        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Update_material_successfully_if_unique_material_code_is_empty()
    {
        string code = "code2";
        var materialAttributes = SupplyChainManagementPreparingData.MaterialAttributes1;
        var material = Material.Create("code1", "name1", materialAttributes, MaterialType.Subassemblies, RegionalMarket.Florida, (UniqueMaterialCodeResult)Result.Success());
        material.Value.WithId(SupplyChainManagementPreparingData.MaterialId1);
        Func<string, CancellationToken, Task<MaterialIdWithCode>> getMaterialByCode = (code, cancellationToken) =>
        {
            return Task.FromResult(default(MaterialIdWithCode));
        };
        var uniqueMaterialCodeResult = await UniqueMaterialCodeService.CheckUniqueMaterialCodeAsync(material.Value.Id, code, getMaterialByCode, default);

        var result = material.Value.UpdateMaterial(code, "name1", materialAttributes, MaterialType.Subassemblies, RegionalMarket.Florida, uniqueMaterialCodeResult);

        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public void Update_material_with_subassemblies_type_successfully()
    {
        string code1 = "code1";
        string code2 = "code2";
        var materialAttributes1 = SupplyChainManagementPreparingData.MaterialAttributes1;
        var material = Material.Create($"    {code1}    ", "name1", materialAttributes1, MaterialType.Material, RegionalMarket.None, (UniqueMaterialCodeResult)Result.Success());

        var result = material.Value.UpdateMaterial($"    {code2}    ", "name1", materialAttributes1, MaterialType.Subassemblies, RegionalMarket.Florida, (UniqueMaterialCodeResult)Result.Success());

        result.IsSuccess.Should().Be(true);
        material.Value.Attributes.Should().Be(materialAttributes1);
        material.Value.Code.Should().Be(code2);
        material.Value.MaterialType.Should().Be(MaterialType.Subassemblies);
        material.Value.RegionalMarket.Should().Be(RegionalMarket.Florida);
    }

    [Fact]
    public void Update_material_with_material_type_successfully()
    {
        string code1 = "code1";
        string code2 = "code2";
        var materialAttributes = SupplyChainManagementPreparingData.MaterialAttributes1;

        var material = Material.Create($"    {code1}    ", "name1", materialAttributes, MaterialType.Subassemblies, RegionalMarket.Florida, (UniqueMaterialCodeResult)Result.Success());

        var result = material.Value.UpdateMaterial($"    {code2}    ", "name1", materialAttributes, MaterialType.Material, RegionalMarket.None, (UniqueMaterialCodeResult)Result.Success());

        result.IsSuccess.Should().Be(true);
        material.Value.Attributes.Should().Be(materialAttributes);
        material.Value.Code.Should().Be(code2);
        material.Value.MaterialType.Should().Be(MaterialType.Material);
        material.Value.RegionalMarket.Should().Be(RegionalMarket.None);
    }

    [Fact]
    public void Add_material_cost_successfully()
    {
        var materialAttributes = SupplyChainManagementPreparingData.MaterialAttributes1;
        var material = Material.Create($"code1", "name1", materialAttributes, MaterialType.Material, RegionalMarket.None, (UniqueMaterialCodeResult)Result.Success());
        material.Value.WithId(SupplyChainManagementPreparingData.MaterialId1);
        var suppliers = new List<(SupplierId SupplierId, byte CurrencyTypeId)>
        {
            (SupplyChainManagementPreparingData.SupplierId1, CurrencyType.VND.Id),
            (SupplyChainManagementPreparingData.SupplierId2, CurrencyType.VND.Id)
        };
        var input = new List<(decimal, uint, decimal, SupplierId)>
        {
            (6m, 5, 8m, SupplyChainManagementPreparingData.SupplierId1),
            (7m, 6, 9m, SupplyChainManagementPreparingData.SupplierId2),
        };
        var materialCosts = MaterialSupplierCost.Create(SupplyChainManagementPreparingData.MaterialId1, input, suppliers).Value;

        var result = material.Value.UpdateCost(materialCosts);

        result.IsSuccess.Should().Be(true);
        material.Value.MaterialSupplierCosts[0].Price.Should().Be(Money.Create(6m, CurrencyType.VND).Value);
        material.Value.MaterialSupplierCosts[0].MinQuantity.Should().Be(5);
        material.Value.MaterialSupplierCosts[0].Surcharge.Should().Be(Money.Create(8m, CurrencyType.VND).Value);
        material.Value.MaterialSupplierCosts[0].MaterialSupplierIdentity.Should().Be(MaterialSupplierIdentity.Create(SupplyChainManagementPreparingData.MaterialId1, SupplyChainManagementPreparingData.SupplierId1).Value);

        material.Value.MaterialSupplierCosts[1].Price.Should().Be(Money.Create(7m, CurrencyType.VND).Value);
        material.Value.MaterialSupplierCosts[1].MinQuantity.Should().Be(6);
        material.Value.MaterialSupplierCosts[1].Surcharge.Should().Be(Money.Create(9m, CurrencyType.VND).Value);
        material.Value.MaterialSupplierCosts[1].MaterialSupplierIdentity.Should().Be(MaterialSupplierIdentity.Create(SupplyChainManagementPreparingData.MaterialId1, SupplyChainManagementPreparingData.SupplierId2).Value);
    }

    [Fact]
    public void Update_material_cost_successfully()
    {
        var materialAttributes = SupplyChainManagementPreparingData.MaterialAttributes1;
        var material = Material.Create($"code1", "name1", materialAttributes, MaterialType.Material, RegionalMarket.None, (UniqueMaterialCodeResult)Result.Success());
        material.Value.WithId(SupplyChainManagementPreparingData.MaterialId1);

        var suppliers = new List<(SupplierId SupplierId, byte CurrencyTypeId)>
        {
            (SupplyChainManagementPreparingData.SupplierId1, CurrencyType.VND.Id),
            (SupplyChainManagementPreparingData.SupplierId2, CurrencyType.VND.Id)
        };
        var inputAdded = new List<(decimal, uint, decimal, SupplierId)>
        {
            (6m, 5, 8m, SupplyChainManagementPreparingData.SupplierId1),
            (7m, 6, 9m, SupplyChainManagementPreparingData.SupplierId2),
        };
        var materialCostsAdded = MaterialSupplierCost.Create(SupplyChainManagementPreparingData.MaterialId1, inputAdded, suppliers).Value;

        var inputUpdated = new List<(decimal, uint, decimal, SupplierId)>
        {
            (10m, 20, 30m, SupplyChainManagementPreparingData.SupplierId1),
            (40m, 50, 60m, SupplyChainManagementPreparingData.SupplierId2),
        };
        var materialCostsUpdated = MaterialSupplierCost.Create(SupplyChainManagementPreparingData.MaterialId1, inputUpdated, suppliers).Value;

        material.Value.UpdateCost(materialCostsAdded);
        var result = material.Value.UpdateCost(materialCostsUpdated);

        result.IsSuccess.Should().Be(true);
        material.Value.MaterialSupplierCosts[0].Price.Should().Be(Money.Create(10m, CurrencyType.VND).Value);
        material.Value.MaterialSupplierCosts[0].MinQuantity.Should().Be(20);
        material.Value.MaterialSupplierCosts[0].Surcharge.Should().Be(Money.Create(30m, CurrencyType.VND).Value);
        material.Value.MaterialSupplierCosts[0].MaterialSupplierIdentity.Should().Be(MaterialSupplierIdentity.Create(SupplyChainManagementPreparingData.MaterialId1, SupplyChainManagementPreparingData.SupplierId1).Value);

        material.Value.MaterialSupplierCosts[1].Price.Should().Be(Money.Create(40m, CurrencyType.VND).Value);
        material.Value.MaterialSupplierCosts[1].MinQuantity.Should().Be(50);
        material.Value.MaterialSupplierCosts[1].Surcharge.Should().Be(Money.Create(60m, CurrencyType.VND).Value);
        material.Value.MaterialSupplierCosts[1].MaterialSupplierIdentity.Should().Be(MaterialSupplierIdentity.Create(SupplyChainManagementPreparingData.MaterialId1, SupplyChainManagementPreparingData.SupplierId2).Value);
    }

    [Fact]
    public void Remove_cost_successfully()
    {
        var materialAttributes = SupplyChainManagementPreparingData.MaterialAttributes1;
        var material = Material.Create($"code1", "name1", materialAttributes, MaterialType.Material, RegionalMarket.None, (UniqueMaterialCodeResult)Result.Success());
        material.Value.WithId(SupplyChainManagementPreparingData.MaterialId1);
        var suppliers = new List<(SupplierId SupplierId, byte CurrencyTypeId)>
        {
            (SupplyChainManagementPreparingData.SupplierId1, CurrencyType.VND.Id),
            (SupplyChainManagementPreparingData.SupplierId2, CurrencyType.VND.Id)
        };
        var inputAdded = new List<(decimal, uint, decimal, SupplierId)>
        {
            (6m, 5, 8m, SupplyChainManagementPreparingData.SupplierId1),
            (7m, 6, 9m, SupplyChainManagementPreparingData.SupplierId2),
        };
        var materialCostsAdded = MaterialSupplierCost.Create(SupplyChainManagementPreparingData.MaterialId1, inputAdded, suppliers).Value;

        var inputDeleted = new List<(decimal, uint, decimal, SupplierId)>
        {
             (6m, 5, 8m, SupplyChainManagementPreparingData.SupplierId1),
        };
        var materialCostsDeleted = MaterialSupplierCost.Create(SupplyChainManagementPreparingData.MaterialId1, inputDeleted, suppliers).Value;

        material.Value.UpdateCost(materialCostsAdded);
        var result = material.Value.UpdateCost(materialCostsDeleted);

        result.IsSuccess.Should().BeTrue();
        material.Value.MaterialSupplierCosts.Should().HaveCount(1);
        material.Value.MaterialSupplierCosts[0].Price.Should().Be(Money.Create(6m, CurrencyType.VND).Value);
        material.Value.MaterialSupplierCosts[0].MinQuantity.Should().Be(5);
        material.Value.MaterialSupplierCosts[0].Surcharge.Should().Be(Money.Create(8m, CurrencyType.VND).Value);
        material.Value.MaterialSupplierCosts[0].MaterialSupplierIdentity.Should().Be(MaterialSupplierIdentity.Create(SupplyChainManagementPreparingData.MaterialId1, SupplyChainManagementPreparingData.SupplierId1).Value);
    }
}