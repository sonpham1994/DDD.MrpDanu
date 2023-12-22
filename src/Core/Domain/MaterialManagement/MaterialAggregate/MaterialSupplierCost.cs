using Domain.Extensions;
using Domain.SharedKernel.Base;
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
        IReadOnlyList<(decimal price, uint minQuantity, decimal surcharge, SupplierId supplierId)> inputs,
        IReadOnlyList<(SupplierId supplierId, byte currencyTypeId)> supplierIds)
    {
        // var existNullSupplier = materialCosts.Any(x => x is null);
        // if (existNullSupplier)
        //     return DomainErrors.MaterialCostManagement.NullSupplier;

        // var isNotSupplier = suppliers.AnyFailure(x => x.IsSupplier());
        // if (isNotSupplier.IsFailure)
        //     return isNotSupplier.Error;

        var result = new List<MaterialSupplierCost>(inputs.Count);
        
        foreach ((decimal price, uint minQuantity, decimal surcharge, SupplierId supplierId) in inputs)
        {
            //var materialCostOfSupplier = supplierIds.FirstOrDefault(x => x == materialCost.SupplierId);
            //if (materialCostOfSupplier.IsEmpty())
            //    return DomainErrors.MaterialCostManagement.NotFoundSupplierId(materialCost.SupplierId);

            //var surchargeResult = Money.Create(surcharge, materialCost.Price.CurrencyType);
            //if (surchargeResult.IsFailure)
            //    return DomainErrors.MaterialCostManagement.InvalidSurcharge;

            //if (minQuantity == 0)
            //    return DomainErrors.MaterialCostManagement.InvalidMinQuantity;

            //var materialCostManagement = new MaterialSupplierCost(materialCost, minQuantity, surchargeResult.Value);
            //var duplicateSupplierId = result.FirstOrDefault(x => x.MaterialCost.SupplierId == materialCost.SupplierId);
            //if (duplicateSupplierId is not null)
            //    return DomainErrors.MaterialCostManagement.DuplicationSupplierId(duplicateSupplierId.MaterialCost.SupplierId);
            
            //result.Add(materialCostManagement);
        }

        return result;
    }

    internal Result SetMaterialCost(Money price, uint minQuantity, Money surcharge)
    {
        if (minQuantity == 0)
            return DomainErrors.MaterialCostManagement.InvalidMinQuantity;
        
        // var validityResult = IsValidCurrency(price, surcharge, TransactionalPartner);
        // if (validityResult.IsFailure)
        //     return validityResult;
        
        //if (Surcharge != surcharge)
            Surcharge = surcharge;

        MinQuantity = minQuantity;

        MaterialCost = MaterialCost.Create(MaterialCost.MaterialId, MaterialCost.SupplierId, price).Value;
        
        return Result.Success();
    }

    // private static Result<MaterialCostManagement> Create( 
    //     in uint minQuantity, 
    //     Money surcharge, 
    //     MaterialCost materialCost)
    // {
    //     // if (supplier is null)
    //     //     return DomainErrors.TransactionalPartner.NotFound;
    //     //
    //     // var isSupplier = supplier.IsSupplier();
    //     // if (isSupplier.IsFailure)
    //     //     return isSupplier;
    //
    //     // var validityResult = IsValidCurrency(price, surcharge, supplier);
    //     // if (validityResult.IsFailure)
    //     //     return validityResult;
    //     
    //     return new MaterialCostManagement(price, minQuantity, surcharge, supplier);
    // }
    
    // private static Result IsValidCurrency(Money price, Money surcharge, TransactionalPartner supplier)
    // {
    //     if (price.CurrencyType != supplier.CurrencyType || surcharge.CurrencyType != supplier.CurrencyType)
    //         return DomainErrors.MaterialCostManagement
    //             .DifferentCurrencyBetweenSupplierAndPriceWithSurcharge(price.CurrencyType.Name, surcharge.CurrencyType.Name, supplier.CurrencyType.Name);
    //
    //     return Result.Success();
    // }
}