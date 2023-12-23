using Domain.MaterialManagement.MaterialAggregate;
using Domain.MaterialManagement.TransactionalPartnerAggregate;
using Domain.SharedKernel.Enumerations;
using Domain.SharedKernel.ValueObjects;
using FluentAssertions;
using DomainErrors = Domain.MaterialManagement.DomainErrors;

namespace Domain.Tests.MaterialManagement.MaterialAggregate;

public class MaterialSupplierCostTests
{
    [Fact]
    public void Cannot_create_material_cost_with_customer()
    {
        var input = new List<(decimal, uint, decimal, Guid)>
        {
            (6m, 5, 8m, MaterialManagementPreparingData.TransactionalPartnerId1),
            (5m, 5, 5m, MaterialManagementPreparingData.TransactionalPartnerId2)
        };
        
        var suppliers = new List<TransactionalPartner>
        {
            MaterialManagementPreparingData.TransactionalPartnerWithCustomerType.WithId(MaterialManagementPreparingData.TransactionalPartnerId1),
            MaterialManagementPreparingData.TransactionalPartnerWithSupplierType.WithId(MaterialManagementPreparingData.TransactionalPartnerId2),
        };

        var materialCosts = MaterialSupplierCost.Create(input, suppliers);

        materialCosts.IsFailure.Should().BeTrue();
        materialCosts.Error.Should().Be(DomainErrors.MaterialSupplierCost.NotSupplier(Guid.Empty));
    }
    
    [Fact]
    public void Cannot_create_material_cost_with_supplierId_input_is_empty()
    {
        var input = new List<(decimal, uint, decimal, Guid)>
        {
            (6m, 5, 8m, Guid.Empty)
        };
        
        var suppliers = new List<TransactionalPartner>
        {
            MaterialManagementPreparingData.TransactionalPartnerWithSupplierType.WithId(MaterialManagementPreparingData.TransactionalPartnerId1)
        };

        var materialCosts = MaterialSupplierCost.Create(input, suppliers);

        materialCosts.IsFailure.Should().BeTrue();
        materialCosts.Error.Should().Be(DomainErrors.TransactionalPartner.NotFoundId(Guid.Empty));
    }
    
    [Fact]
    public void Cannot_create_material_cost_with_not_exist_supplierId_input()
    {
        var input = new List<(decimal, uint, decimal, Guid)>
        {
            (6m, 5, 8m, Guid.NewGuid())
        };
        
        var suppliers = new List<TransactionalPartner>
        {
            MaterialManagementPreparingData.TransactionalPartnerWithSupplierType.WithId(MaterialManagementPreparingData.TransactionalPartnerId1)
        };

        var materialCosts = MaterialSupplierCost.Create(input, suppliers);

        materialCosts.IsFailure.Should().BeTrue();
        materialCosts.Error.Should().Be(DomainErrors.TransactionalPartner.NotFoundId(Guid.Empty));
    }
    
    [Fact]
    public void Cannot_create_material_cost_with_transient_supplier()
    {
        var input = new List<(decimal, uint, decimal, Guid)>
        {
            (6m, 5, 8m, MaterialManagementPreparingData.TransactionalPartnerId2)
        };
        
        var suppliers = new List<TransactionalPartner>
        {
            MaterialManagementPreparingData.TransactionalPartnerWithSupplierType.WithId(Guid.Empty)
        };

        var materialCosts = MaterialSupplierCost.Create(input, suppliers);

        materialCosts.IsFailure.Should().BeTrue();
        materialCosts.Error.Should().Be(DomainErrors.TransactionalPartner.NotFoundId(Guid.Empty));
    }
    
    [Fact]
    public void Cannot_create_material_cost_with_null_supplier()
    {
        var input = new List<(decimal, uint, decimal, Guid)>
        {
            (6m, 5, 8m, MaterialManagementPreparingData.TransactionalPartnerId1),
            (6m, 5, 8m, MaterialManagementPreparingData.TransactionalPartnerId2)
        };
        
        var suppliers = new List<TransactionalPartner>
        {
            MaterialManagementPreparingData.TransactionalPartnerWithSupplierType.WithId(MaterialManagementPreparingData.TransactionalPartnerId1),
            null
        };

        var materialCosts = MaterialSupplierCost.Create(input, suppliers);

        materialCosts.IsFailure.Should().BeTrue();
        materialCosts.Error.Should().Be(DomainErrors.MaterialSupplierCost.NullSupplier);
    }
    
    [Fact]
    public void Cannot_create_material_cost_with_zero_min_quantity()
    {
        var input = new List<(decimal, uint, decimal, Guid)>
        {
            (6m, 0, 8m, MaterialManagementPreparingData.TransactionalPartnerId1),
        };
        
        var suppliers = new List<TransactionalPartner>
        {
            MaterialManagementPreparingData.TransactionalPartnerWithSupplierType.WithId(MaterialManagementPreparingData.TransactionalPartnerId1),
        };

        var materialCosts = MaterialSupplierCost.Create(input, suppliers);

        materialCosts.IsFailure.Should().BeTrue();
        materialCosts.Error.Should().Be(DomainErrors.MaterialSupplierCost.InvalidMinQuantity);
    }

    [Fact]
    public void Cannot_create_material_cost_with_zero_price()
    {
        var input = new List<(decimal, uint, decimal, Guid)>
        {
            (0m, 5, 8m, MaterialManagementPreparingData.TransactionalPartnerId1),
        };

        var suppliers = new List<TransactionalPartner>
        {
            MaterialManagementPreparingData.TransactionalPartnerWithSupplierType.WithId(MaterialManagementPreparingData.TransactionalPartnerId1),
        };

        var materialCosts = MaterialSupplierCost.Create(input, suppliers);

        materialCosts.IsFailure.Should().BeTrue();
        materialCosts.Error.Should().Be(DomainErrors.MaterialSupplierCost.InvalidPrice);
    }

    [Fact]
    public void Cannot_create_material_cost_with_negative_price()
    {
        var input = new List<(decimal, uint, decimal, Guid)>
        {
            (-5m, 5, 8m, MaterialManagementPreparingData.TransactionalPartnerId1),
        };

        var suppliers = new List<TransactionalPartner>
        {
            MaterialManagementPreparingData.TransactionalPartnerWithSupplierType.WithId(MaterialManagementPreparingData.TransactionalPartnerId1),
        };

        var materialCosts = MaterialSupplierCost.Create(input, suppliers);

        materialCosts.IsFailure.Should().BeTrue();
        materialCosts.Error.Should().Be(DomainErrors.MaterialSupplierCost.InvalidPrice);
    }

    [Fact]
    public void Cannot_create_material_cost_with_zero_surcharge()
    {
        var input = new List<(decimal, uint, decimal, Guid)>
        {
            (5m, 5, 0m, MaterialManagementPreparingData.TransactionalPartnerId1),
        };

        var suppliers = new List<TransactionalPartner>
        {
            MaterialManagementPreparingData.TransactionalPartnerWithSupplierType.WithId(MaterialManagementPreparingData.TransactionalPartnerId1),
        };

        var materialCosts = MaterialSupplierCost.Create(input, suppliers);

        materialCosts.IsFailure.Should().BeTrue();
        materialCosts.Error.Should().Be(DomainErrors.MaterialSupplierCost.InvalidSurcharge);
    }

    [Fact]
    public void Cannot_create_material_cost_with_negative_surcharge()
    {
        var input = new List<(decimal, uint, decimal, Guid)>
        {
            (5m, 5, -4m, MaterialManagementPreparingData.TransactionalPartnerId1),
        };

        var suppliers = new List<TransactionalPartner>
        {
            MaterialManagementPreparingData.TransactionalPartnerWithSupplierType.WithId(MaterialManagementPreparingData.TransactionalPartnerId1),
        };

        var materialCosts = MaterialSupplierCost.Create(input, suppliers);

        materialCosts.IsFailure.Should().BeTrue();
        materialCosts.Error.Should().Be(DomainErrors.MaterialSupplierCost.InvalidSurcharge);
    }

    [Fact]
    public void Create_material_cost_successfully()
    {
        var input = new List<(decimal, uint, decimal, Guid)>
        {
            (6m, 5, 8m, MaterialManagementPreparingData.TransactionalPartnerId1)
        };
        var supplier =
            MaterialManagementPreparingData.TransactionalPartnerWithSupplierType.WithId(MaterialManagementPreparingData
                .TransactionalPartnerId1);
        var suppliers = new List<TransactionalPartner>
        {
            supplier
        };

        var materialCosts = MaterialSupplierCost.Create(input, suppliers);

        materialCosts.IsSuccess.Should().BeTrue();
        materialCosts.Value.Should().HaveCount(1);
        materialCosts.Value.First().Price.CurrencyType.Should().Be(supplier.CurrencyType);
        materialCosts.Value.First().Price.Value.Should().Be(6m);
        materialCosts.Value.First().Surcharge.Value.Should().Be(8m);
        materialCosts.Value.First().Surcharge.CurrencyType.Should().Be(supplier.CurrencyType);
    }

    [Fact]
    public void Cannot_set_material_cost_with_different_surcharge_currency_from_supplier()
    {
        var input = new List<(decimal, uint, decimal, Guid)>
        {
            (6m, 5, 8m, MaterialManagementPreparingData.TransactionalPartnerId1)
        };
        var supplier =
            MaterialManagementPreparingData.TransactionalPartnerWithSupplierType.WithId(MaterialManagementPreparingData
                .TransactionalPartnerId1);
        var suppliers = new List<TransactionalPartner>
        {
            supplier
        };
        var price = Money.Create(500m, CurrencyType.VND).Value;
        var surcharge = Money.Create(400m, CurrencyType.USD).Value;

        var materialCost = MaterialSupplierCost.Create(input, suppliers).Value.First();
        var result = materialCost.SetMaterialCost(price, 12, surcharge);
        
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.MaterialSupplierCost.DifferentCurrencyBetweenSupplierAndPriceWithSurcharge(string.Empty, string.Empty, string.Empty));
    }
    
    [Fact]
    public void Cannot_set_material_cost_with_different_price_currency_from_supplier()
    {
        var input = new List<(decimal, uint, decimal, Guid)>
        {
            (6m, 5, 8m, MaterialManagementPreparingData.TransactionalPartnerId1),
        };
        var supplier =
            MaterialManagementPreparingData.TransactionalPartnerWithSupplierType.WithId(MaterialManagementPreparingData
                .TransactionalPartnerId1);
        var suppliers = new List<TransactionalPartner>
        {
            supplier
        };
        var price = Money.Create(500m, CurrencyType.USD).Value;
        var surcharge = Money.Create(400m, CurrencyType.VND).Value;

        var materialCost = MaterialSupplierCost.Create(input, suppliers).Value.First();
        var result = materialCost.SetMaterialCost(price, 12, surcharge);
        
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.MaterialSupplierCost.DifferentCurrencyBetweenSupplierAndPriceWithSurcharge(string.Empty, string.Empty, string.Empty));
    }
    
    [Fact]
    public void Cannot_set_material_cost_with_zero_min_quantity()
    {
        var input = new List<(decimal, uint, decimal, Guid)>
        {
            (6m, 5, 8m, MaterialManagementPreparingData.TransactionalPartnerId1)
        };
        var supplier =
            MaterialManagementPreparingData.TransactionalPartnerWithSupplierType.WithId(MaterialManagementPreparingData
                .TransactionalPartnerId1);
        var suppliers = new List<TransactionalPartner>
        {
            supplier
        };
        var price = Money.Create(500m, CurrencyType.VND).Value;
        var surcharge = Money.Create(400m, CurrencyType.VND).Value;

        var materialCost = MaterialSupplierCost.Create(input, suppliers).Value.First();
        var result = materialCost.SetMaterialCost(price, 0, surcharge);
        
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.MaterialSupplierCost.InvalidMinQuantity);
    }

    [Fact]
    public void Set_material_cost_successfully()
    {
        var input = new List<(decimal, uint, decimal, Guid)>
        {
            (6m, 5, 8m, MaterialManagementPreparingData.TransactionalPartnerId1)
        };
        var supplier =
            MaterialManagementPreparingData.TransactionalPartnerWithSupplierType.WithId(MaterialManagementPreparingData
                .TransactionalPartnerId1);
        var suppliers = new List<TransactionalPartner>
        {
            supplier
        };
        var price = Money.Create(400m, CurrencyType.VND).Value;
        var surcharge = Money.Create(500m, CurrencyType.VND).Value;

        var materialCost = MaterialSupplierCost.Create(input, suppliers).Value.First();
        var result = materialCost.SetMaterialCost(price, 15, surcharge);
        
        result.IsSuccess.Should().Be(true);
        materialCost.Price.Value.Should().Be(400m);
        materialCost.Price.CurrencyType.Should().Be(supplier.CurrencyType);
        materialCost.Surcharge.Value.Should().Be(500m);
        materialCost.Price.CurrencyType.Should().Be(supplier.CurrencyType);
        materialCost.MinQuantity.Should().Be(15);
    }
}


