using Domain.Services.UniqueMaterialCodeServices;
using SharedKernel.Enumerations;
using SharedKernel.ValueObjects;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;
using Domain.Tests.Helpers;
using FluentAssertions;
using DomainErrors = Domain.SupplyAndProductionManagement.SupplyChainManagement.DomainErrors;

namespace Domain.Tests.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;

public class MaterialTests
{
    [Fact]
    public void Cannot_create_material_with_material_type_and_not_none_regional_market()
    {
        var materialAttributes = SupplyChainManagementPreparingData.MaterialAttributes1;
        var code1 = SupplyChainManagementPreparingData.MaterialCode1;
        var name1 = SupplyChainManagementPreparingData.MaterialName1;
        var material = Material.Create(code1, name1, materialAttributes, MaterialType.Material, RegionalMarket.Florida, UniqueMaterialCodeResult.Success());

        material.IsSuccess.Should().BeFalse();
        material.Error.Should().Be(DomainErrors.Material.InvalidMaterialType);
    }

    [Fact]
    public void Cannot_create_material_with_subassemblies_type_and_none_regional_market()
    {
        var materialAttributes = SupplyChainManagementPreparingData.MaterialAttributes1;
        var code1 = SupplyChainManagementPreparingData.MaterialCode1;
        var name1 = SupplyChainManagementPreparingData.MaterialName1;
        var material = Material.Create(code1, name1, materialAttributes, MaterialType.Subassemblies, RegionalMarket.None, UniqueMaterialCodeResult.Success());

        material.IsSuccess.Should().BeFalse();
        material.Error.Should().Be(DomainErrors.Material.InvalidSubassembliesType);
    }

    [Fact]
    public async Task Cannot_create_material_if_unique_material_code_exists()
    {
        var code1 = SupplyChainManagementPreparingData.MaterialCode1;
        var name1 = SupplyChainManagementPreparingData.MaterialName1;
        var materialAttributes = SupplyChainManagementPreparingData.MaterialAttributes1;

        var uniqueMaterialCodeResult = await CheckUniqueMaterialCodeService
            .HandleAsync(code1, GetMaterialByCodeWithReturningCodeValueForMaterialId1, default);
        var material = Material
            .Create(
                code1,
                name1,
                materialAttributes,
                MaterialType.Subassemblies,
                RegionalMarket.Florida,
                uniqueMaterialCodeResult
            );

        material.IsSuccess.Should().BeFalse();
        material.Error.Should().Be(DomainErrors.Material.ExistedCode(code1, SupplyChainManagementPreparingData.MaterialId1));
    }

    [Fact]
    public async Task Create_material_if_unique_material_code_is_empty()
    {
        var code1 = SupplyChainManagementPreparingData.MaterialCode1;
        var name1 = SupplyChainManagementPreparingData.MaterialName1;
        var materialAttributes = SupplyChainManagementPreparingData.MaterialAttributes1;

        var uniqueMaterialCodeResult = await CheckUniqueMaterialCodeService.HandleAsync(code1, GetMaterialByCodeWithReturningNullValue, default);
        var material = Material
            .Create(
                code1,
                name1,
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
        var code1 = SupplyChainManagementPreparingData.MaterialCode1;
        var name1 = SupplyChainManagementPreparingData.MaterialName1;
        var materialAttributes = SupplyChainManagementPreparingData.MaterialAttributes1;
        var material = Material.Create(code1, name1, materialAttributes, MaterialType.Subassemblies, RegionalMarket.Florida, UniqueMaterialCodeResult.Success());

        material.IsSuccess.Should().Be(true);
        material.Value!.Attributes.Should().Be(materialAttributes);
        material.Value.Code.Should().Be(code1);
        material.Value.MaterialType.Should().Be(MaterialType.Subassemblies);
        material.Value.RegionalMarket.Should().Be(RegionalMarket.Florida);
    }

    [Fact]
    public void Create_material_with_material_type_successfully()
    {
        var code1 = SupplyChainManagementPreparingData.MaterialCode1;
        var name1 = SupplyChainManagementPreparingData.MaterialName1;
        var materialAttributes = SupplyChainManagementPreparingData.MaterialAttributes1;
        var material = Material.Create(code1, name1, materialAttributes, MaterialType.Material, RegionalMarket.None, UniqueMaterialCodeResult.Success());

        material.IsSuccess.Should().BeTrue();
        material.Value!.Attributes.Should().Be(materialAttributes);
        material.Value.Code.Should().Be(code1);
        material.Value.MaterialType.Should().Be(MaterialType.Material);
        material.Value.RegionalMarket.Should().Be(RegionalMarket.None);
    }

    [Fact]
    public void Cannot_update_material_with_material_type_and_not_none_regional_market()
    {
        var code1 = SupplyChainManagementPreparingData.MaterialCode1;
        var name1 = SupplyChainManagementPreparingData.MaterialName1;
        var materialAttributes = SupplyChainManagementPreparingData.MaterialAttributes1;
        var material = Material.Create(code1, name1, materialAttributes, MaterialType.Material, RegionalMarket.None, UniqueMaterialCodeResult.Success());

        var result = material.Value!.UpdateMaterial(code1, name1, materialAttributes, MaterialType.Material, RegionalMarket.Florida, UniqueMaterialCodeResult.Success());

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(DomainErrors.Material.InvalidMaterialType);
    }

    [Fact]
    public void Cannot_update_material_with_subassemblies_type_and_none_regional_market()
    {
        var materialAttributes = SupplyChainManagementPreparingData.MaterialAttributes1;
        var code1 = SupplyChainManagementPreparingData.MaterialCode1;
        var name1 = SupplyChainManagementPreparingData.MaterialName1;
        var material = Material.Create(code1, name1, materialAttributes, MaterialType.Subassemblies, RegionalMarket.Florida, UniqueMaterialCodeResult.Success());

        var result = material.Value!.UpdateMaterial(code1, name1, materialAttributes, MaterialType.Subassemblies, RegionalMarket.None, UniqueMaterialCodeResult.Success());

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(DomainErrors.Material.InvalidSubassembliesType);
    }

    [Fact]
    public async Task Cannot_update_material_if_unique_material_code_exists_on_another_material()
    {
        var code1 = SupplyChainManagementPreparingData.MaterialCode1;
        var code2 = MaterialCode.Create("code2").Value!;
        var name1 = SupplyChainManagementPreparingData.MaterialName1;
        var materialAttributes = SupplyChainManagementPreparingData.MaterialAttributes1;
        var material = Material.Create(code1, name1, materialAttributes, MaterialType.Subassemblies, RegionalMarket.Florida, UniqueMaterialCodeResult.Success());
        material.Value!.WithId(SupplyChainManagementPreparingData.MaterialId1);

        var uniqueMaterialCodeResult = await CheckUniqueMaterialCodeService
            .HandleAsync(
                material.Value!.Id, code2, 
                GetMaterialByCodeWithReturningCodeValueForMaterialId2, 
                default);

        var result = material.Value.UpdateMaterial(code2, name1, materialAttributes, MaterialType.Subassemblies, RegionalMarket.None, uniqueMaterialCodeResult);

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(DomainErrors.Material.ExistedCode(code2, SupplyChainManagementPreparingData.MaterialId2));
    }

    [Fact]
    public async Task Update_material_successfully_if_unique_material_code_exists_on_the_same_material()
    {
        var code1 = SupplyChainManagementPreparingData.MaterialCode1;
        var materialAttributes = SupplyChainManagementPreparingData.MaterialAttributes1;
        var name1 = SupplyChainManagementPreparingData.MaterialName1;
        var material = Material.Create(code1, name1, materialAttributes, MaterialType.Subassemblies, RegionalMarket.Florida, UniqueMaterialCodeResult.Success());
        material.Value!.WithId(SupplyChainManagementPreparingData.MaterialId1);
        var uniqueMaterialCodeResult = await CheckUniqueMaterialCodeService
            .HandleAsync(
                material.Value!.Id, code1, 
                GetMaterialByCodeWithReturningCodeValueForMaterialId1, 
                default);

        var result = material.Value.UpdateMaterial(code1, name1, materialAttributes, MaterialType.Subassemblies, RegionalMarket.Florida, uniqueMaterialCodeResult);

        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Update_material_successfully_if_unique_material_code_is_empty()
    {
        var code1 = SupplyChainManagementPreparingData.MaterialCode1;
        var code2 = MaterialCode.Create("code2").Value!;
        var name1 = SupplyChainManagementPreparingData.MaterialName1;
        var materialAttributes = SupplyChainManagementPreparingData.MaterialAttributes1;
        var material = Material.Create(code1, name1, materialAttributes, MaterialType.Subassemblies, RegionalMarket.Florida, UniqueMaterialCodeResult.Success());
        material.Value!.WithId(SupplyChainManagementPreparingData.MaterialId1);
        var uniqueMaterialCodeResult = await CheckUniqueMaterialCodeService.HandleAsync(material.Value!.Id, code2, GetMaterialByCodeWithReturningNullValue, default);

        var result = material.Value.UpdateMaterial(code2, name1, materialAttributes, MaterialType.Subassemblies, RegionalMarket.Florida, uniqueMaterialCodeResult);

        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public void Update_material_with_subassemblies_type_successfully()
    {
        var code1 = SupplyChainManagementPreparingData.MaterialCode1;
        var name1 = SupplyChainManagementPreparingData.MaterialName1;
        var materialAttributes1 = SupplyChainManagementPreparingData.MaterialAttributes1;
        var material = Material.Create(code1, name1, materialAttributes1, MaterialType.Material, RegionalMarket.None, UniqueMaterialCodeResult.Success());

        var result = material.Value!.UpdateMaterial(code1, name1, materialAttributes1, MaterialType.Subassemblies, RegionalMarket.Florida, UniqueMaterialCodeResult.Success());

        result.IsSuccess.Should().Be(true);
        material.Value.Attributes.Should().Be(materialAttributes1);
        material.Value.MaterialType.Should().Be(MaterialType.Subassemblies);
        material.Value.RegionalMarket.Should().Be(RegionalMarket.Florida);
    }

    [Fact]
    public void Update_material_with_material_type_successfully()
    {
        var code1 = SupplyChainManagementPreparingData.MaterialCode1;
        var materialAttributes = SupplyChainManagementPreparingData.MaterialAttributes1;
        var name1 = SupplyChainManagementPreparingData.MaterialName1;

        var material = Material.Create(code1, name1, materialAttributes, MaterialType.Subassemblies, RegionalMarket.Florida, UniqueMaterialCodeResult.Success());

        var result = material.Value!.UpdateMaterial(code1, name1, materialAttributes, MaterialType.Material, RegionalMarket.None, UniqueMaterialCodeResult.Success());

        result.IsSuccess.Should().Be(true);
        material.Value.Attributes.Should().Be(materialAttributes);
        material.Value.MaterialType.Should().Be(MaterialType.Material);
        material.Value.RegionalMarket.Should().Be(RegionalMarket.None);
    }

    [Fact]
    public void Add_material_cost_successfully()
    {
        var code1 = SupplyChainManagementPreparingData.MaterialCode1;
        var materialAttributes = SupplyChainManagementPreparingData.MaterialAttributes1;
        var name1 = SupplyChainManagementPreparingData.MaterialName1;
        var material = Material.Create(code1, name1, materialAttributes, MaterialType.Material, RegionalMarket.None, UniqueMaterialCodeResult.Success());
        material.Value!.WithId(SupplyChainManagementPreparingData.MaterialId1);
        var getSuppliersIdAndCurrencyTypeResult = SupplyChainManagementPreparingData.GetListSupplierId1AndId2AndCurrencyTypeVndResult;
        var input = new List<(decimal, uint, decimal, SupplierId)>
        {
            (6m, 5, 8m, SupplyChainManagementPreparingData.SupplierId1),
            (7m, 6, 9m, SupplyChainManagementPreparingData.SupplierId2),
        };
        var materialCosts = MaterialSupplierCost.Create(SupplyChainManagementPreparingData.MaterialId1, input, getSuppliersIdAndCurrencyTypeResult).Value!;

        var result = material.Value!.UpdateCost(materialCosts);

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
        var code1 = SupplyChainManagementPreparingData.MaterialCode1;
        var name1 = SupplyChainManagementPreparingData.MaterialName1;
        var materialAttributes = SupplyChainManagementPreparingData.MaterialAttributes1;
        var material = Material.Create(code1, name1, materialAttributes, MaterialType.Material, RegionalMarket.None, UniqueMaterialCodeResult.Success());
        material.Value!.WithId(SupplyChainManagementPreparingData.MaterialId1);

        var getSuppliersIdAndCurrencyTypeResult = SupplyChainManagementPreparingData.GetListSupplierId1AndId2AndCurrencyTypeVndResult;
        var inputAdded = new List<(decimal, uint, decimal, SupplierId)>
        {
            (6m, 5, 8m, SupplyChainManagementPreparingData.SupplierId1),
            (7m, 6, 9m, SupplyChainManagementPreparingData.SupplierId2),
        };
        var materialCostsAdded = MaterialSupplierCost.Create(SupplyChainManagementPreparingData.MaterialId1, inputAdded, getSuppliersIdAndCurrencyTypeResult).Value!;

        var inputUpdated = new List<(decimal, uint, decimal, SupplierId)>
        {
            (10m, 20, 30m, SupplyChainManagementPreparingData.SupplierId1),
            (40m, 50, 60m, SupplyChainManagementPreparingData.SupplierId2),
        };
        var materialCostsUpdated = MaterialSupplierCost.Create(SupplyChainManagementPreparingData.MaterialId1, inputUpdated, getSuppliersIdAndCurrencyTypeResult).Value!;

        material.Value!.UpdateCost(materialCostsAdded);
        var result = material.Value.UpdateCost(materialCostsUpdated);

        result.IsSuccess.Should().BeTrue();
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
        var code1 = SupplyChainManagementPreparingData.MaterialCode1;
        var materialAttributes = SupplyChainManagementPreparingData.MaterialAttributes1;
        var name1 = SupplyChainManagementPreparingData.MaterialName1;
        var material = Material.Create(code1, name1, materialAttributes, MaterialType.Material, RegionalMarket.None, UniqueMaterialCodeResult.Success());
        material.Value!.WithId(SupplyChainManagementPreparingData.MaterialId1);
        var getSuppliersIdAndCurrencyTypeResult = SupplyChainManagementPreparingData.GetListSupplierId1AndId2AndCurrencyTypeVndResult;
        var inputAdded = new List<(decimal, uint, decimal, SupplierId)>
        {
            (6m, 5, 8m, SupplyChainManagementPreparingData.SupplierId1),
            (7m, 6, 9m, SupplyChainManagementPreparingData.SupplierId2),
        };
        var materialCostsAdded = MaterialSupplierCost.Create(SupplyChainManagementPreparingData.MaterialId1, inputAdded, getSuppliersIdAndCurrencyTypeResult).Value!;

        var inputDeleted = new List<(decimal, uint, decimal, SupplierId)>
        {
             (6m, 5, 8m, SupplyChainManagementPreparingData.SupplierId1),
        };
        var materialCostsDeleted = MaterialSupplierCost.Create(SupplyChainManagementPreparingData.MaterialId1, inputDeleted, getSuppliersIdAndCurrencyTypeResult).Value!;

        material.Value!.UpdateCost(materialCostsAdded);
        var result = material.Value.UpdateCost(materialCostsDeleted);

        result.IsSuccess.Should().BeTrue();
        material.Value.MaterialSupplierCosts.Should().HaveCount(1);
        material.Value.MaterialSupplierCosts[0].Price.Should().Be(Money.Create(6m, CurrencyType.VND).Value);
        material.Value.MaterialSupplierCosts[0].MinQuantity.Should().Be(5);
        material.Value.MaterialSupplierCosts[0].Surcharge.Should().Be(Money.Create(8m, CurrencyType.VND).Value);
        material.Value.MaterialSupplierCosts[0].MaterialSupplierIdentity.Should().Be(MaterialSupplierIdentity.Create(SupplyChainManagementPreparingData.MaterialId1, SupplyChainManagementPreparingData.SupplierId1).Value);
    }
    
    private Task<MaterialIdWithCode?> GetMaterialByCodeWithReturningNullValue(MaterialCode code, CancellationToken cancellationToken)
    {
        return Task.FromResult(default(MaterialIdWithCode));
    }
    
    private Task<MaterialIdWithCode?> GetMaterialByCodeWithReturningCodeValueForMaterialId1(MaterialCode code, CancellationToken cancellationToken)
    {
        var materialIdWithCode1 = new MaterialIdWithCode { Id = SupplyChainManagementPreparingData.MaterialId1.Value, Code = code.Value };

        return Task.FromResult(materialIdWithCode1)!;
    }
    
    private Task<MaterialIdWithCode?> GetMaterialByCodeWithReturningCodeValueForMaterialId2(MaterialCode code, CancellationToken cancellationToken)
    {
        var materialIdWithCode1 = new MaterialIdWithCode { Id = SupplyChainManagementPreparingData.MaterialId2.Value, Code = code.Value };

        return Task.FromResult(materialIdWithCode1)!;
    }
}