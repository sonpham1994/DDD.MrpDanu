using Domain.MaterialManagement.MaterialAggregate;
using Domain.MaterialManagement.TransactionalPartnerAggregate;
using Domain.SharedKernel.Enumerations;
using Domain.SharedKernel.ValueObjects;
using FluentAssertions;
using DomainErrors = Domain.MaterialManagement.DomainErrors;

namespace Domain.Tests.MaterialManagement.MaterialAggregate;

public class MaterialTests
{
    [Fact]
    public void Cannot_create_material_with_material_type_and_not_none_regional_market()
    {
        var materialAttributes = MaterialManagementPreparingData.MaterialAttributes1;
        var material = Material.Create("code1", "name1", materialAttributes, MaterialType.Material, RegionalMarket.Florida);

        material.IsFailure.Should().Be(true);
        material.Error.Should().Be(DomainErrors.Material.InvalidMaterialType);
    }

    [Fact]
    public void Cannot_create_material_with_subassemblies_type_and_none_regional_market()
    {
        var materialAttributes = MaterialManagementPreparingData.MaterialAttributes1;
        var material = Material.Create("code1", "name1",materialAttributes, MaterialType.Subassemblies, RegionalMarket.None);

        material.IsFailure.Should().Be(true);
        material.Error.Should().Be(DomainErrors.Material.InvalidSubassembliesType);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Cannot_create_material_with_null_or_empty_code(string code)
    {
        var materialAttributes = MaterialManagementPreparingData.MaterialAttributes1;
        var material = Material.Create(code, "name1", materialAttributes, MaterialType.Subassemblies, RegionalMarket.Florida);

        material.IsFailure.Should().Be(true);
        material.Error.Should().Be(DomainErrors.Material.EmptyCode);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Cannot_create_material_with_null_or_empty_name(string name)
    {
        var materialAttributes = MaterialManagementPreparingData.MaterialAttributes1;
        var material = Material.Create("code1", name, materialAttributes, MaterialType.Subassemblies, RegionalMarket.Florida);

        material.IsFailure.Should().Be(true);
        material.Error.Should().Be(DomainErrors.Material.EmptyName);
    }

    [Fact]
    public void Create_material_with_subassemblies_type_successfully()
    {
        string code = "code1";
        var materialAttributes = MaterialManagementPreparingData.MaterialAttributes1;
        var material = Material.Create($" {code} ", "name1", materialAttributes, MaterialType.Subassemblies, RegionalMarket.Florida);

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
        var materialAttributes = MaterialManagementPreparingData.MaterialAttributes1;
        var material = Material.Create($" {code} ", "name1", materialAttributes, MaterialType.Material, RegionalMarket.None);

        material.IsSuccess.Should().BeTrue();
        material.Value.Attributes.Should().Be(materialAttributes);
        material.Value.Code.Should().Be(code);
        material.Value.MaterialType.Should().Be(MaterialType.Material);
        material.Value.RegionalMarket.Should().Be(RegionalMarket.None);
    }

    [Fact]
    public void Cannot_update_material_with_material_type_and_not_none_regional_market()
    {
        var materialAttributes = MaterialManagementPreparingData.MaterialAttributes1;
        var material = Material.Create("code1", "name1", materialAttributes, MaterialType.Material, RegionalMarket.None);

        var result = material.Value.UpdateMaterial("code1", "name1",materialAttributes, MaterialType.Material, RegionalMarket.Florida);
        
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.Material.InvalidMaterialType);
    }

    [Fact]
    public void Cannot_update_material_with_subassemblies_type_and_none_regional_market()
    {
        var materialAttributes = MaterialManagementPreparingData.MaterialAttributes1;
        var material = Material.Create("code1", "name1",materialAttributes, MaterialType.Subassemblies, RegionalMarket.Florida);

        var result = material.Value.UpdateMaterial("code1", "name1",materialAttributes, MaterialType.Subassemblies, RegionalMarket.None);
        
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.Material.InvalidSubassembliesType);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Cannot_update_material_with_null_or_empty_code(string code)
    {
        var materialAttributes = MaterialManagementPreparingData.MaterialAttributes1;
        var material = Material.Create("code1", "name1",materialAttributes, MaterialType.Subassemblies, RegionalMarket.Florida);
        
        var result = material.Value.UpdateMaterial(code, "name1",materialAttributes, MaterialType.Subassemblies, RegionalMarket.Florida);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.Material.EmptyCode);
    }
    
    [Fact]
    public void Update_material_with_subassemblies_type_successfully()
    {
        string code1 = "code1";
        string code2 = "code2";
        var materialAttributes1 = MaterialManagementPreparingData.MaterialAttributes1;
        var material = Material.Create($"    {code1}    ", "name1",materialAttributes1, MaterialType.Material, RegionalMarket.None);
        
        var result = material.Value.UpdateMaterial($"    {code2}    ", "name1",materialAttributes1, MaterialType.Subassemblies, RegionalMarket.Florida);

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
        var materialAttributes = MaterialManagementPreparingData.MaterialAttributes1;
        
        var material = Material.Create($"    {code1}    ", "name1",materialAttributes, MaterialType.Subassemblies, RegionalMarket.Florida);

        var result = material.Value.UpdateMaterial($"    {code2}    ", "name1",materialAttributes, MaterialType.Material, RegionalMarket.None);
        
        result.IsSuccess.Should().Be(true);
        material.Value.Attributes.Should().Be(materialAttributes);
        material.Value.Code.Should().Be(code2);
        material.Value.MaterialType.Should().Be(MaterialType.Material);
        material.Value.RegionalMarket.Should().Be(RegionalMarket.None);
    }
    
    [Fact]
    public void Do_not_change_code_unique_when_update_material_attributes()
    {
        string code = "code1";
        var materialAttributes1 = MaterialManagementPreparingData.MaterialAttributes1;
        var materialAttributes2 = MaterialManagementPreparingData.MaterialAttributes2;
        
        var material = Material.Create($"    {code}    ", "name1",materialAttributes1, MaterialType.Subassemblies, RegionalMarket.Florida);

        var result = material.Value.UpdateMaterial($"    {code}    ", "name1",materialAttributes2, MaterialType.Subassemblies, RegionalMarket.Florida);
        
        result.IsSuccess.Should().BeTrue();
        material.Value.Attributes.Should().Be(materialAttributes2);
    }
    
    [Fact]
    public void Cannot_add_material_cost_with_supplier_duplication()
    {
        var materialAttributes = MaterialManagementPreparingData.MaterialAttributes1;
        var material = Material.Create($"code1", "name1",materialAttributes, MaterialType.Material, RegionalMarket.None);
        var randomSupplierId = Guid.NewGuid();
        var suppliers = new List<TransactionalPartner>
        {
            MaterialManagementPreparingData.TransactionalPartnerWithSupplierType.WithId(MaterialManagementPreparingData.TransactionalPartnerId1),
            MaterialManagementPreparingData.TransactionalPartnerWithSupplierType.WithId(randomSupplierId)
        };
        var input = new List<(decimal, uint, decimal, Guid)>
        {
            (6m, 5, 8m, MaterialManagementPreparingData.TransactionalPartnerId1),
            (1m, 1, 1m, randomSupplierId),
            (7m, 6, 9m, MaterialManagementPreparingData.TransactionalPartnerId1),
        };
        var materialCosts = MaterialSupplierCost.Create(input, suppliers).Value;

        var result = material.Value.UpdateCost(materialCosts);
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.MaterialSupplierCost.DuplicationSupplierId(Guid.Empty));
    }
    
    [Fact]
    public void Add_material_cost_successfully()
    {
        var materialAttributes = MaterialManagementPreparingData.MaterialAttributes1;
        var material = Material.Create($"code1", "name1",materialAttributes, MaterialType.Material, RegionalMarket.None);
        
        var suppliers = new List<TransactionalPartner>
        {
            MaterialManagementPreparingData.TransactionalPartnerWithSupplierType.WithId(MaterialManagementPreparingData.TransactionalPartnerId1),
            MaterialManagementPreparingData.TransactionalPartnerWithSupplierType.WithId(MaterialManagementPreparingData.TransactionalPartnerId2)
        };
        var input = new List<(decimal, uint, decimal, Guid)>
        {
            (6m, 5, 8m, MaterialManagementPreparingData.TransactionalPartnerId1),
            (7m, 6, 9m, MaterialManagementPreparingData.TransactionalPartnerId2),
        };
        var materialCosts = MaterialSupplierCost.Create(input, suppliers).Value;

        var result = material.Value.UpdateCost(materialCosts);
        
        result.IsSuccess.Should().Be(true);
        material.Value.MaterialSupplierCosts[0].Price.Should().Be(Money.Create(6m, CurrencyType.VND).Value);
        material.Value.MaterialSupplierCosts[0].MinQuantity.Should().Be(5);
        material.Value.MaterialSupplierCosts[0].Surcharge.Should().Be(Money.Create(8m, CurrencyType.VND).Value);
        material.Value.MaterialSupplierCosts[0].TransactionalPartner.Should().Be(MaterialManagementPreparingData.TransactionalPartnerWithSupplierType.WithId(MaterialManagementPreparingData.TransactionalPartnerId1));
        
        material.Value.MaterialSupplierCosts[1].Price.Should().Be(Money.Create(7m, CurrencyType.VND).Value);
        material.Value.MaterialSupplierCosts[1].MinQuantity.Should().Be(6);
        material.Value.MaterialSupplierCosts[1].Surcharge.Should().Be(Money.Create(9m, CurrencyType.VND).Value);
        material.Value.MaterialSupplierCosts[1].TransactionalPartner.Should().Be(MaterialManagementPreparingData.TransactionalPartnerWithSupplierType.WithId(MaterialManagementPreparingData.TransactionalPartnerId2));
    }
    
    [Fact]
    public void Update_material_cost_successfully()
    {
        var materialAttributes = MaterialManagementPreparingData.MaterialAttributes1;
        var material = Material.Create($"code1", "name1",materialAttributes, MaterialType.Material, RegionalMarket.None);

        var suppliers = new List<TransactionalPartner>
        {
            MaterialManagementPreparingData.TransactionalPartnerWithSupplierType.WithId(MaterialManagementPreparingData.TransactionalPartnerId1),
            MaterialManagementPreparingData.TransactionalPartnerWithSupplierType.WithId(MaterialManagementPreparingData.TransactionalPartnerId2)
        };
        var inputAdded = new List<(decimal, uint, decimal, Guid)>
        {
            (6m, 5, 8m, MaterialManagementPreparingData.TransactionalPartnerId1),
            (7m, 6, 9m, MaterialManagementPreparingData.TransactionalPartnerId2),
        };
        var materialCostsAdded = MaterialSupplierCost.Create(inputAdded, suppliers).Value;

        var inputUpdated = new List<(decimal, uint, decimal, Guid)>
        {
            (10m, 20, 30m, MaterialManagementPreparingData.TransactionalPartnerId1),
            (40m, 50, 60m, MaterialManagementPreparingData.TransactionalPartnerId2),
        };
        var materialCostsUpdated = MaterialSupplierCost.Create(inputUpdated, suppliers).Value;

        material.Value.UpdateCost(materialCostsAdded);
        var result = material.Value.UpdateCost(materialCostsUpdated);

        result.IsSuccess.Should().Be(true);
        material.Value.MaterialSupplierCosts[0].Price.Should().Be(Money.Create(10m, CurrencyType.VND).Value);
        material.Value.MaterialSupplierCosts[0].MinQuantity.Should().Be(20);
        material.Value.MaterialSupplierCosts[0].Surcharge.Should().Be(Money.Create(30m, CurrencyType.VND).Value);
        material.Value.MaterialSupplierCosts[0].TransactionalPartner.Should().Be(MaterialManagementPreparingData.TransactionalPartnerWithSupplierType.WithId(MaterialManagementPreparingData.TransactionalPartnerId1));

        material.Value.MaterialSupplierCosts[1].Price.Should().Be(Money.Create(40m, CurrencyType.VND).Value);
        material.Value.MaterialSupplierCosts[1].MinQuantity.Should().Be(50);
        material.Value.MaterialSupplierCosts[1].Surcharge.Should().Be(Money.Create(60m, CurrencyType.VND).Value);
        material.Value.MaterialSupplierCosts[1].TransactionalPartner.Should().Be(MaterialManagementPreparingData.TransactionalPartnerWithSupplierType.WithId(MaterialManagementPreparingData.TransactionalPartnerId2));
    }

    [Fact]
    public void Remove_cost_successfully()
    {
        var materialAttributes = MaterialManagementPreparingData.MaterialAttributes1;
        var material = Material.Create($"code1", "name1",materialAttributes, MaterialType.Material, RegionalMarket.None);

        var suppliers = new List<TransactionalPartner>
        {
            MaterialManagementPreparingData.TransactionalPartnerWithSupplierType.WithId(MaterialManagementPreparingData.TransactionalPartnerId1),
            MaterialManagementPreparingData.TransactionalPartnerWithSupplierType.WithId(MaterialManagementPreparingData.TransactionalPartnerId2)
        };
        var inputAdded = new List<(decimal, uint, decimal, Guid)>
        {
            (6m, 5, 8m, MaterialManagementPreparingData.TransactionalPartnerId1),
            (7m, 6, 9m, MaterialManagementPreparingData.TransactionalPartnerId2),
        };
        var materialCostsAdded = MaterialCostManagement.Create(inputAdded, suppliers).Value;

        var inputDeleted = new List<(decimal, uint, decimal, Guid)>
        {
             (6m, 5, 8m, MaterialManagementPreparingData.TransactionalPartnerId1),
        };
        var materialCostsDeleted = MaterialCostManagement.Create(inputDeleted, suppliers).Value;

        material.Value.UpdateCost(materialCostsAdded);
        var result = material.Value.UpdateCost(materialCostsDeleted);

        result.IsSuccess.Should().BeTrue();
        material.Value.MaterialSupplierCosts.Should().HaveCount(1);
        material.Value.MaterialSupplierCosts[0].Price.Should().Be(Money.Create(6m, CurrencyType.VND).Value);
        material.Value.MaterialSupplierCosts[0].MinQuantity.Should().Be(5);
        material.Value.MaterialSupplierCosts[0].Surcharge.Should().Be(Money.Create(8m, CurrencyType.VND).Value);
        material.Value.MaterialSupplierCosts[0].TransactionalPartner.Should().Be(MaterialManagementPreparingData.TransactionalPartnerWithSupplierType.WithId(MaterialManagementPreparingData.TransactionalPartnerId1));
    }
}