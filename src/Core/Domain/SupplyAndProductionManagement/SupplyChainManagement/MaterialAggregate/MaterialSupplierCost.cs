using Domain.Extensions;
using Domain.SharedKernel.Base;
using Domain.SharedKernel.Enumerations;
using Domain.SharedKernel.ValueObjects;

namespace Domain.SupplyChainManagement.MaterialAggregate;

public class MaterialSupplierCost : EntityGuidStronglyTypedId<MaterialSupplierCostId>
{
    public Money Surcharge { get; private set; }
    public uint MinQuantity { get; private set; }
    public Money Price { get; private set; }

    /*
        * DDDAndEFCore_Myexercise/3.WorkingWithManyToOneRelationships/3.5.TypesOfRelationShipses
        * In fact, the fewer relationships you have in your domain classes, the lower is the coupling between classes 
        *  and the simpler your domain model becomes, as a result. Despite having two relationships in the database,
        *  in our domain model, we only have one of them, the many‑to‑one relationship from Material to
        *  MaterialCostManagement. There  is no inverse relationship, which would take form of a material entity in
        *  the MaterialCostManagement class.All this  tries to reduce the number of relationships in your domain model
        *  to the absolute minimum in order to minimize code complexity.
        */
    //public Material Material { get; }
    //public virtual TransactionalPartner TransactionalPartner { get; private set; }
    
    public MaterialSupplierIdentity MaterialSupplierIdentity { get; private set; }

    //required EF Proxies
    protected MaterialSupplierCost() {}
    
    private MaterialSupplierCost(
        MaterialSupplierIdentity materialSupplierIdentity,
        Money price,
        in uint minQuantity, 
        Money surcharge)
    {
        MaterialSupplierIdentity = materialSupplierIdentity;
        Price = price;
        MinQuantity = minQuantity;
        Surcharge = surcharge;
    }
    
    public static Result<IReadOnlyList<MaterialSupplierCost>> Create(MaterialId materialId,
        IReadOnlyList<(decimal price, uint minQuantity, decimal surcharge, SupplierId supplierId)> inputs,
        IReadOnlyList<(SupplierId SupplierId, byte CurrencyTypeId)> supplierIdWithCurrencyTypeIds)
    {
        var result = new List<MaterialSupplierCost>(inputs.Count);
        
        foreach ((decimal price, uint minQuantity, decimal surcharge, SupplierId supplierId) in inputs)
        {
            // in this Aggregate Root (Material), the MaterialSupplierCost cannot duplicate by the same supplierId and this
            // is an invariant in the Material aggregate root, the MaterialSupplierCost just exists in Material
            // aggregate root, no other aggregates. So we put this invariant here instead of putting this in Material.
            var materialSupplierIdentity = MaterialSupplierIdentity.Create(materialId, supplierId);
            if (materialSupplierIdentity.IsFailure)
                return materialSupplierIdentity.Error;
            
            var duplicateSupplierId = result.FirstOrDefault(x => x.MaterialSupplierIdentity == materialSupplierIdentity.Value);
            if (duplicateSupplierId is not null)
                return DomainErrors.MaterialCostManagement.DuplicationSupplierId(duplicateSupplierId.MaterialSupplierIdentity.SupplierId);
            
            if (minQuantity == 0)
                return DomainErrors.MaterialCostManagement.InvalidMinQuantity;
            if (price <= 0)
                return DomainErrors.MaterialCostManagement.InvalidPrice;
            
            var (existSupplierId, currencyTypeId) = supplierIdWithCurrencyTypeIds.FirstOrDefault(x => x.SupplierId == supplierId);
            if (existSupplierId.IsEmpty())
                return DomainErrors.MaterialCostManagement.NotFoundSupplierId(supplierId);
            
            var currencyType = CurrencyType.FromId(currencyTypeId);
            if (currencyType.IsFailure)
                return currencyType.Error;
            
            var priceResult = Money.Create(price, currencyType.Value);
            if (priceResult.IsFailure)
                return priceResult.Error;
            
            var surchargeResult = Money.Create(surcharge, currencyType.Value);
            if (surchargeResult.IsFailure)
                return DomainErrors.MaterialCostManagement.InvalidSurcharge;
            
            var isValidPriceAndSurcharge = IsValidPriceAndSurcharge(priceResult.Value, surchargeResult.Value);
            if (isValidPriceAndSurcharge.IsFailure)
                return isValidPriceAndSurcharge.Error;
            
            var materialSupplierCost = new MaterialSupplierCost(materialSupplierIdentity.Value, priceResult.Value, minQuantity, surchargeResult.Value);

            result.Add(materialSupplierCost);
        }

        return result;
    }
    
    internal Result SetMaterialCost(Money price, uint minQuantity, Money surcharge)
    {
        if (minQuantity == 0)
            return DomainErrors.MaterialCostManagement.InvalidMinQuantity;
        
        var validityPriceAndSurchargeResult = IsValidPriceAndSurcharge(price, surcharge);
         if (validityPriceAndSurchargeResult.IsFailure)
             return validityPriceAndSurchargeResult.Error;
        
        //if (Surcharge != surcharge)
            Surcharge = surcharge;

        MinQuantity = minQuantity;
        Price = price;
        
        return Result.Success();
    }
    
    private static Result IsValidPriceAndSurcharge(Money price, Money surcharge)
    {
        if (price.CurrencyType !=  surcharge.CurrencyType)
            return DomainErrors.MaterialCostManagement
                .DifferentCurrencyBetweenSupplierAndPriceWithSurcharge(price.CurrencyType, surcharge.CurrencyType);
    
        return Result.Success();
    }
}