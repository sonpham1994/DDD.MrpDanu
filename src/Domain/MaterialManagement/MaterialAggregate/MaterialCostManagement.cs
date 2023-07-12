using Domain.SharedKernel;
using Domain.Errors;
using Domain.Extensions;
using Domain.MaterialManagement.TransactionalPartnerAggregate;
using Domain.SharedKernel.Base;

namespace Domain.MaterialManagement.MaterialAggregate;

public class MaterialCostManagement : Entity
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

    public virtual TransactionalPartner TransactionalPartner { get; private set; }

    //required EF Proxies
    protected MaterialCostManagement() {}
    
    private MaterialCostManagement(Money price, 
        uint minQuantity, 
        Money surcharge,  
        TransactionalPartner transactionalPartner)
    {
        Price = price;
        MinQuantity = minQuantity;
        Surcharge = surcharge;
        TransactionalPartner = transactionalPartner;
    }
    
    public static Result<IReadOnlyList<MaterialCostManagement>> Create(
        IEnumerable<(decimal price, uint minQuantity, decimal surcharge, Guid supplierId)> input
        , IEnumerable<TransactionalPartner> suppliers)
    {
        var existNullSupplier = suppliers.Any(x => x is null);
        if (existNullSupplier)
            return DomainErrors.MaterialCostManagement.NullSupplier;

        var transientSupplier = suppliers.FirstOrDefault(x => x.IsTransient());
        if (transientSupplier is not null)
            return DomainErrors.TransactionalPartner.NotFoundId(transientSupplier.Id);

        var isNotSupplier = suppliers.AnyFailure(x => x.IsSupplier());
        if (isNotSupplier.IsFailure)
            return isNotSupplier.Error;

        var result = new List<MaterialCostManagement>(input.Count());
        
        foreach ((decimal price, uint minQuantity, decimal surcharge, Guid supplierId) in input)
        {
            var supplier = suppliers.FirstOrDefault(x => x.Id == supplierId);
            if (supplier is null)
                return DomainErrors.TransactionalPartner.NotFoundId(supplierId);

            var priceResult = Money.Create(price, supplier.CurrencyType);
            if (priceResult.IsFailure)
                return DomainErrors.MaterialCostManagement.InvalidPrice;

            var surchargeResult = Money.Create(surcharge, supplier.CurrencyType);
            if (surchargeResult.IsFailure)
                return DomainErrors.MaterialCostManagement.InvalidSurcharge;

            if (minQuantity == 0)
                return DomainErrors.MaterialCostManagement.InvalidMinQuantity;

            var materialCost = Create(priceResult.Value, minQuantity, surchargeResult.Value, supplier);
            if (materialCost.IsFailure)
                return materialCost.Error;

            result.Add(materialCost.Value);
        }

        return result;
    }

    internal Result SetMaterialCost(Money price, uint minQuantity, Money surcharge)
    {
        if (minQuantity == 0)
            return DomainErrors.MaterialCostManagement.InvalidMinQuantity;
        
        var validityResult = IsValidCurrency(price, surcharge, TransactionalPartner);
        if (validityResult.IsFailure)
            return validityResult;
        
        //if (Price != price)
            Price = price;
        //if (Surcharge != surcharge)
            Surcharge = surcharge;

        MinQuantity = minQuantity;

        return Result.Success();
    }

    private static Result<MaterialCostManagement> Create(Money price, 
        uint minQuantity, 
        Money surcharge, 
        TransactionalPartner? supplier)
    {
        if (supplier is null)
            return DomainErrors.TransactionalPartner.NotFound;

        var isSupplier = supplier.IsSupplier();
        if (isSupplier.IsFailure)
            return isSupplier;

        var validityResult = IsValidCurrency(price, surcharge, supplier);
        if (validityResult.IsFailure)
            return validityResult;
        
        return new MaterialCostManagement(price, minQuantity, surcharge, supplier);
    }
    
    private static Result IsValidCurrency(Money price, Money surcharge, TransactionalPartner supplier)
    {
        if (price.CurrencyType != supplier.CurrencyType || surcharge.CurrencyType != supplier.CurrencyType)
            return DomainErrors.MaterialCostManagement
                .DifferentCurrencyBetweenSupplierAndPriceWithSurcharge(price.CurrencyType.Name, surcharge.CurrencyType.Name, supplier.CurrencyType.Name);

        return Result.Success();
    }
}