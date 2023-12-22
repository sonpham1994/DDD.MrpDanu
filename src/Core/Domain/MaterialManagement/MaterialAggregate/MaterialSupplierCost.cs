using Domain.Extensions;
using Domain.SharedKernel.Base;
using Domain.SharedKernel.Enumerations;
using Domain.SharedKernel.ValueObjects;

namespace Domain.MaterialManagement.MaterialAggregate;

public class MaterialSupplierCost : EntityGuidStronglyTypedId<MaterialSupplierCostId>
{
    public Money Surcharge { get; private set; }
    public uint MinQuantity { get; private set; }

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
    
    public MaterialCost MaterialCost { get; private set; }

    //required EF Proxies
    protected MaterialSupplierCost() {}
    
    private MaterialSupplierCost(
        MaterialCost materialCost, 
        in uint minQuantity, 
        Money surcharge)
    {
        MaterialCost = materialCost;
        MinQuantity = minQuantity;
        Surcharge = surcharge;
    }
    
    public static Result<IReadOnlyList<MaterialSupplierCost>> Create(MaterialId materialId,
        IReadOnlyList<(decimal Price, uint MinQuantity, decimal Surcharge, SupplierId SupplierId)> inputs,
        IReadOnlyList<(SupplierId SupplierId, byte CurrencyTypeId)> supplierIdWithCurrencyTypeIds)
    {
        var result = new List<MaterialSupplierCost>(inputs.Count);
        
        foreach ((decimal price, uint minQuantity, decimal surcharge, SupplierId supplierId) in inputs)
        {
            // in this Aggregate Root (Material), the MaterialSupplierCost cannot duplicate by the same supplierId. So
            // we put this invariant here instead of putting this in Material.
            var duplicateSupplierId = result.FirstOrDefault(x => x.MaterialCost.SupplierId == supplierId);
            if (duplicateSupplierId is not null)
                return DomainErrors.MaterialCostManagement.DuplicationSupplierId(duplicateSupplierId.MaterialCost.SupplierId);
            
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
            
            var materialCost = MaterialCost.Create(materialId, supplierId, priceResult.Value);
            var materialSupplierCost = new MaterialSupplierCost(materialCost.Value, minQuantity, surchargeResult.Value);

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

        MaterialCost = MaterialCost.Create(MaterialCost.MaterialId, MaterialCost.SupplierId, price).Value;
        
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