using Domain.Extensions;
using Domain.Extensions.LinqExtensions;
using Domain.SharedKernel.Base;
using Domain.SharedKernel.Enumerations;
using Domain.SharedKernel.ValueObjects;

namespace Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;

public class MaterialSupplierCost : EntityGuidStronglyTypedId<MaterialSupplierCostId>
{
    public Money Surcharge { get; private set; } = default!;
    public uint MinQuantity { get; private set; }
    public Money Price { get; private set; } = default!;

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

    public MaterialSupplierIdentity MaterialSupplierIdentity { get; private set; } = default!;

    //required EF Proxies
    protected MaterialSupplierCost() { }

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

    public static Result<IReadOnlyList<MaterialSupplierCost>?> Create(MaterialId materialId,
        IReadOnlyList<(decimal price, uint minQuantity, decimal surcharge, SupplierId supplierId)> request,
        IReadOnlyList<(SupplierId SupplierId, byte CurrencyTypeId)> supplierIdWithCurrencyTypeIds)
    {
        var dupSupplierId = supplierIdWithCurrencyTypeIds.GetItemDuplication(x => x.SupplierId);
        if (!dupSupplierId.IsEmpty())
            return DomainErrors.MaterialSupplierCost.DuplicationSupplierIdFromDataStorage(dupSupplierId);
        
        dupSupplierId = request.GetItemDuplication(x => x.supplierId);
        if (!dupSupplierId.IsEmpty())
            return DomainErrors.MaterialSupplierCost.DuplicationSupplierIdFromRequest(dupSupplierId);
        
        var result = new List<MaterialSupplierCost>(request.Count);

        foreach ((decimal price, uint minQuantity, decimal surcharge, SupplierId supplierId) in request)
        {
            // in this Aggregate Root (Material), the MaterialSupplierCost cannot duplicate by the same supplierId and this
            // is an invariant in the Material aggregate root, the MaterialSupplierCost just exists in Material
            // aggregate root, no other aggregates. So we put this invariant here instead of putting this in Material.
            var materialSupplierIdentity = MaterialSupplierIdentity.Create(materialId, supplierId);
            if (!materialSupplierIdentity.IsSuccess)
                return materialSupplierIdentity.Error;

            if (minQuantity == 0)
                return DomainErrors.MaterialSupplierCost.InvalidMinQuantity;
            if (price <= 0)
                return DomainErrors.MaterialSupplierCost.InvalidPrice;

            var (existSupplierId, currencyTypeId) = supplierIdWithCurrencyTypeIds.FirstOrDefault(x => x.SupplierId == supplierId);
            if (existSupplierId.IsEmpty())
                return DomainErrors.MaterialSupplierCost.NotFoundSupplierId(supplierId);

            var currencyType = CurrencyType.FromId(currencyTypeId);
            if (!currencyType.IsSuccess)
                return currencyType.Error;

            var priceResult = Money.Create(price, currencyType.Value!);
            if (!priceResult.IsSuccess)
                return priceResult.Error;

            var surchargeResult = Money.Create(surcharge, currencyType.Value!);
            if (!surchargeResult.IsSuccess)
                return DomainErrors.MaterialSupplierCost.InvalidSurcharge;

            var isValidPriceAndSurcharge = IsValidPriceAndSurcharge(priceResult.Value!, surchargeResult.Value!);
            if (!isValidPriceAndSurcharge.IsSuccess)
                return isValidPriceAndSurcharge.Error;

            var materialSupplierCost = new MaterialSupplierCost(materialSupplierIdentity.Value!, priceResult.Value!, minQuantity, surchargeResult.Value!);

            result.Add(materialSupplierCost);
        }

        return result;
    }

    internal Result SetMaterialCost(Money price, uint minQuantity, Money surcharge)
    {
        if (minQuantity == 0)
            return DomainErrors.MaterialSupplierCost.InvalidMinQuantity;

        var validityPriceAndSurchargeResult = IsValidPriceAndSurcharge(price, surcharge);
        if (!validityPriceAndSurchargeResult.IsSuccess)
            return validityPriceAndSurchargeResult.Error;

        //if (Surcharge != surcharge)
        Surcharge = surcharge;

        MinQuantity = minQuantity;
        Price = price;

        return Result.Success();
    }

    private static Result IsValidPriceAndSurcharge(Money price, Money surcharge)
    {
        if (price.CurrencyType != surcharge.CurrencyType)
            return DomainErrors.MaterialSupplierCost
                .DifferentCurrencyBetweenSupplierAndPriceWithSurcharge(price.CurrencyType, surcharge.CurrencyType);

        return Result.Success();
    }
}