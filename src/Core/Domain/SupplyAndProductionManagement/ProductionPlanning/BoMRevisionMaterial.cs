using Domain.SharedKernel.Base;
using Domain.SharedKernel.ValueObjects;

namespace Domain.SupplyAndProductionManagement.ProductionPlanning;

public class BoMRevisionMaterial : Entity<BoMRevisionMaterialId>
{
    public Unit Unit { get; private set; }
    
    /*these two lines may be a record of MaterialCostManagement, but the problem is that this entity reside in another
     * bounded context. So if we want to get it, we should get it through the Aggregate Root (Material). In addition,
     * if we delete materialCost, it means we also need to delete this record (if that is a domain business)
     */
    /*
     * If PurchaseOrder has data in BoMRevisionMaterial, if MaterialCostManagement change Price, Surcharge, MinQty, it
     * will make an adverse impact on PurchaseOrder. So when we perform purchase Order and status is done or inprogress.
     * we need to store Price, Surcharge, MinQty in PurchaseOrder
     */
    
    /*
     * Entity can belong to single aggregate, but value object can belong to multiple aggregate. So we need to
     * replace TransactionalPartner and Material here because those entities belonged to Material aggregate.
     * And also, the Material and TransactionalPartner are the aggregate root, we cannot nest the aggregate root
     * inside another aggregate.
     * And also, we need to define which properties in those entities this aggregate can use, and we just use
     * those properties.
     * Maybe we will build the Modular architecture, and in this Product bounded context, we don't use Material and
     * TransactionalPartner from Material bounded context, Material bounded context will publish message and
     * Product bounded context will consume message with properties needed, and then the Product bounded context
     * will use data from this with Product's schema.
     */
    // public virtual TransactionalPartner TransactionalPartner { get; }
    // public virtual Material Material { get; }
    
    //we will use this to achieve separate bounded context, bounded contexts don't depend on each other
    // the Material and Transactional partner belong to a different bounded context, so this one would be better.
    // public Guid SupplierId { get; private set; }
    // public Guid MaterialId { get; private set; }
    
    // we notice that when creating BoMRevisionMaterial, MaterialId, SupplierId, Price should go together. we cannot
    // create BoMRevisionMaterial without MaterialId, or cannot create it without SupplierId pr Price. So we put
    // these 3 properties as Value object. In SupplyChainManagement bounded context, we can use this MaterialCost value
    // object, but the problem is that the SupplyChainManagement bounded context is designed as different approach
    // we cannot do this, but if we implement the same approach, this MaterialCost value object can live in there.
    public MaterialSupplierIdentity MaterialSupplierIdentity { get; private set; }
    public Money Price { get; private set; }
    
    public BoMRevisionId BoMRevisionId { get; private set; }
    
    //required EF
    protected BoMRevisionMaterial() {}

    public BoMRevisionMaterial(Unit unit, MaterialSupplierIdentity materialSupplierIdentity, Money price)
    {
        Unit = unit;
        MaterialSupplierIdentity = materialSupplierIdentity;
        Price = price;
    }

    // public Result<IReadOnlyList<BoMRevisionMaterial>> Create(
    //     IReadOnlyList<(decimal unit, MaterialId materialId, SupplierId supplierId)> input, 
    //     IReadOnlyList<MaterialCost> materialCosts)
    // {
    //     foreach (var (unit, materialId, supplierId) in input)
    //     {
    //         var unitResult = Unit.Create(unit);
    //         if (unitResult.IsFailure)
    //             return unitResult.Error;
    //         
    //         var existsMaterialId = materialIds.Any(x => x == materialId);
    //         if (!existsMaterialId)
    //             return DomainErrors.BoMRevisionMaterial.InvalidMaterialId(materialId.Value);
    //
    //         var existsSupplierId = supplierIds.Any(x => x == supplierId);
    //         if (!existsSupplierId)
    //             return DomainErrors.BoMRevisionMaterial.InvalidSupplierId(supplierId.Value);
    //         
    //         var materialCost = MaterialCost.Create(materialId, supplierId, )
    //     }
    // }

    public Result UpdateBoMRevisionId(BoMRevisionId boMRevisionId)
    {
        if (boMRevisionId.Value == 0)
            return DomainErrors.BoMRevision.InvalidId(boMRevisionId.Value);
        
        BoMRevisionId = boMRevisionId;

        return Result.Success();
    }
}