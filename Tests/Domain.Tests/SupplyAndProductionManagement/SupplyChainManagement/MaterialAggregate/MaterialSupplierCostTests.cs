using Domain.Services.GetListSupplierIdAndCurrencyTypeIdServices;
using SharedKernel.Enumerations;
using SharedKernel.ValueObjects;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;
using FluentAssertions;
using DomainErrors = Domain.SupplyAndProductionManagement.SupplyChainManagement.DomainErrors;
using SharedKernelDomainErrors = SharedKernel.Enumerations.DomainErrors;

namespace Domain.Tests.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;

public class MaterialSupplierCostTests
{
    [Fact]
    public void Cannot_create_material_cost_with_not_exist_supplierId_input()
    {
        var supplierId = (SupplierId)Guid.NewGuid();
        var input = new List<(decimal, uint, decimal, SupplierId)>
        {
            (6m, 5, 8m, supplierId)
        };

        var getSuppliersIdAndCurrencyTypeResult = SupplyChainManagementPreparingData.GetListSupplierId1AndId2AndCurrencyTypeVndResult;
        var materialCosts = MaterialSupplierCost.Create(SupplyChainManagementPreparingData.MaterialId1, input, getSuppliersIdAndCurrencyTypeResult);

        materialCosts.IsSuccess.Should().BeFalse();
        materialCosts.Error.Should().Be(DomainErrors.MaterialSupplierCost.NotFoundSupplierId(supplierId));
    }

    [Fact]
    public void Cannot_create_material_cost_with_zero_min_quantity()
    {
        var input = new List<(decimal, uint, decimal, SupplierId)>
        {
            (6m, 0, 8m, SupplyChainManagementPreparingData.SupplierId1),
        };

        var getSuppliersIdAndCurrencyTypeResult = SupplyChainManagementPreparingData.GetListSupplierId1AndId2AndCurrencyTypeVndResult;

        var materialCosts = MaterialSupplierCost.Create(SupplyChainManagementPreparingData.MaterialId1, input, getSuppliersIdAndCurrencyTypeResult);

        materialCosts.IsSuccess.Should().BeFalse();
        materialCosts.Error.Should().Be(DomainErrors.MaterialSupplierCost.InvalidMinQuantity);
    }

    [Fact]
    public void Cannot_create_material_cost_with_zero_price()
    {
        var input = new List<(decimal, uint, decimal, SupplierId)>
        {
            (0m, 5, 8m, SupplyChainManagementPreparingData.SupplierId1),
        };

        var getSuppliersIdAndCurrencyTypeResult = SupplyChainManagementPreparingData.GetListSupplierId1AndId2AndCurrencyTypeVndResult;

        var materialCosts = MaterialSupplierCost.Create(SupplyChainManagementPreparingData.MaterialId1, input, getSuppliersIdAndCurrencyTypeResult);

        materialCosts.IsSuccess.Should().BeFalse();
        materialCosts.Error.Should().Be(DomainErrors.MaterialSupplierCost.InvalidPrice);
    }

    [Fact]
    public void Cannot_create_material_cost_with_negative_price()
    {
        var input = new List<(decimal, uint, decimal, SupplierId)>
        {
            (-5m, 5, 8m, SupplyChainManagementPreparingData.SupplierId1),
        };

        var getSuppliersIdAndCurrencyTypeResult = SupplyChainManagementPreparingData.GetListSupplierId1AndId2AndCurrencyTypeVndResult;

        var materialCosts = MaterialSupplierCost.Create(SupplyChainManagementPreparingData.MaterialId1, input, getSuppliersIdAndCurrencyTypeResult);

        materialCosts.IsSuccess.Should().BeFalse();
        materialCosts.Error.Should().Be(DomainErrors.MaterialSupplierCost.InvalidPrice);
    }

    [Fact]
    public void Cannot_create_material_cost_with_zero_surcharge()
    {
        var input = new List<(decimal, uint, decimal, SupplierId)>
        {
            (5m, 5, 0m, SupplyChainManagementPreparingData.SupplierId1),
        };

        var getSuppliersIdAndCurrencyTypeResult = SupplyChainManagementPreparingData.GetListSupplierId1AndId2AndCurrencyTypeVndResult;

        var materialCosts = MaterialSupplierCost.Create(SupplyChainManagementPreparingData.MaterialId1, input, getSuppliersIdAndCurrencyTypeResult);

        materialCosts.IsSuccess.Should().BeFalse();
        materialCosts.Error.Should().Be(DomainErrors.MaterialSupplierCost.InvalidSurcharge);
    }

    [Fact]
    public void Cannot_create_material_cost_with_negative_surcharge()
    {
        var input = new List<(decimal, uint, decimal, SupplierId)>
        {
            (5m, 5, -4m, SupplyChainManagementPreparingData.SupplierId1),
        };

        var getSuppliersIdAndCurrencyTypeResult = SupplyChainManagementPreparingData.GetListSupplierId1AndId2AndCurrencyTypeVndResult;

        var materialCosts = MaterialSupplierCost.Create(SupplyChainManagementPreparingData.MaterialId1, input, getSuppliersIdAndCurrencyTypeResult);

        materialCosts.IsSuccess.Should().BeFalse();
        materialCosts.Error.Should().Be(DomainErrors.MaterialSupplierCost.InvalidSurcharge);
    }

    [Fact]
    public void Cannot_create_material_cost_with_duplication_supplier_from_request()
    {
        var input = new List<(decimal, uint, decimal, SupplierId)>
        {
            (5m, 5, 8m, SupplyChainManagementPreparingData.SupplierId1),
            (6m, 1, 3m, SupplyChainManagementPreparingData.SupplierId1),
        };

        var getSuppliersIdAndCurrencyTypeResult = SupplyChainManagementPreparingData.GetListSupplierId1AndId2AndCurrencyTypeVndResult;

        var materialCosts = MaterialSupplierCost.Create(SupplyChainManagementPreparingData.MaterialId1, input, getSuppliersIdAndCurrencyTypeResult);

        materialCosts.IsSuccess.Should().BeFalse();
        materialCosts.Error.Should().Be(DomainErrors.MaterialSupplierCost.DuplicationSupplierId(SupplyChainManagementPreparingData.SupplierId1));
    }
    
    [Fact]
    public void Create_material_cost_successfully()
    {
        var input = new List<(decimal, uint, decimal, SupplierId)>
        {
            (6m, 5, 8m, SupplyChainManagementPreparingData.SupplierId1)
        };
        var getSuppliersIdAndCurrencyTypeResult = SupplyChainManagementPreparingData.GetListSupplierId1AndId2AndCurrencyTypeVndResult;

        var materialCosts = MaterialSupplierCost.Create(SupplyChainManagementPreparingData.MaterialId1, input, getSuppliersIdAndCurrencyTypeResult);

        materialCosts.IsSuccess.Should().BeTrue();
        materialCosts.Value.Should().HaveCount(1);
        materialCosts.Value!.First().Price.CurrencyType.Should().Be(CurrencyType.VND);
        materialCosts.Value!.First().Price.Value.Should().Be(6m);
        materialCosts.Value!.First().Surcharge.Value.Should().Be(8m);
        materialCosts.Value!.First().Surcharge.CurrencyType.Should().Be(CurrencyType.VND);
    }

    [Fact]
    public void Cannot_set_material_cost_with_different_surcharge_currency_from_supplier()
    {
        var input = new List<(decimal, uint, decimal, SupplierId)>
        {
            (6m, 5, 8m, SupplyChainManagementPreparingData.SupplierId1)
        };
        var getSuppliersIdAndCurrencyTypeResult = SupplyChainManagementPreparingData.GetListSupplierId1AndId2AndCurrencyTypeVndResult;
        var price = Money.Create(500m, CurrencyType.VND).Value;
        var surcharge = Money.Create(400m, CurrencyType.USD).Value;

        var materialCost = MaterialSupplierCost.Create(SupplyChainManagementPreparingData.MaterialId1, input, getSuppliersIdAndCurrencyTypeResult).Value!.First();
        var result = materialCost.SetMaterialCost(price!, 12, surcharge!);

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(DomainErrors.MaterialSupplierCost.DifferentCurrencyBetweenSupplierAndPriceWithSurcharge(CurrencyType.VND, CurrencyType.USD));
    }

    [Fact]
    public void Cannot_set_material_cost_with_different_price_currency_from_supplier()
    {
        var input = new List<(decimal, uint, decimal, SupplierId)>
        {
            (6m, 5, 8m, SupplyChainManagementPreparingData.SupplierId1),
        };
        var getSuppliersIdAndCurrencyTypeResult = SupplyChainManagementPreparingData.GetListSupplierId1AndId2AndCurrencyTypeVndResult;
        var price = Money.Create(500m, CurrencyType.USD).Value;
        var surcharge = Money.Create(400m, CurrencyType.VND).Value;

        var materialCost = MaterialSupplierCost.Create(SupplyChainManagementPreparingData.MaterialId1, input, getSuppliersIdAndCurrencyTypeResult).Value!.First();
        var result = materialCost.SetMaterialCost(price!, 12, surcharge!);

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(DomainErrors.MaterialSupplierCost.DifferentCurrencyBetweenSupplierAndPriceWithSurcharge(CurrencyType.USD, CurrencyType.VND));
    }

    [Fact]
    public void Cannot_set_material_cost_with_zero_min_quantity()
    {
        var input = new List<(decimal, uint, decimal, SupplierId)>
        {
            (6m, 5, 8m, SupplyChainManagementPreparingData.SupplierId1)
        };
        var getSuppliersIdAndCurrencyTypeResult = SupplyChainManagementPreparingData.GetListSupplierId1AndId2AndCurrencyTypeVndResult;
        var price = Money.Create(500m, CurrencyType.VND).Value;
        var surcharge = Money.Create(400m, CurrencyType.VND).Value;

        var materialCost = MaterialSupplierCost.Create(SupplyChainManagementPreparingData.MaterialId1, input, getSuppliersIdAndCurrencyTypeResult).Value!.First();
        var result = materialCost.SetMaterialCost(price!, 0, surcharge!);

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(DomainErrors.MaterialSupplierCost.InvalidMinQuantity);
    }

    [Fact]
    public void Set_material_cost_successfully()
    {
        var input = new List<(decimal, uint, decimal, SupplierId)>
        {
            (6m, 5, 8m, SupplyChainManagementPreparingData.SupplierId1)
        };
        var getSuppliersIdAndCurrencyTypeResult = SupplyChainManagementPreparingData.GetListSupplierId1AndId2AndCurrencyTypeVndResult;
        var price = Money.Create(400m, CurrencyType.VND).Value;
        var surcharge = Money.Create(500m, CurrencyType.VND).Value;

        var materialCost = MaterialSupplierCost.Create(SupplyChainManagementPreparingData.MaterialId1, input, getSuppliersIdAndCurrencyTypeResult).Value!.First();
        var result = materialCost.SetMaterialCost(price!, 15, surcharge!);

        result.IsSuccess.Should().Be(true);
        materialCost.Price.Value.Should().Be(400m);
        materialCost.Price.CurrencyType.Should().Be(CurrencyType.VND);
        materialCost.Surcharge.Value.Should().Be(500m);
        materialCost.Price.CurrencyType.Should().Be(CurrencyType.VND);
        materialCost.MinQuantity.Should().Be(15);
    }
}