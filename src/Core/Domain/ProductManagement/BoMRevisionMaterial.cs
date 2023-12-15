using Domain.SharedKernel.DomainClasses;
using Domain.MaterialManagement.MaterialAggregate;
using Domain.MaterialManagement.TransactionalPartnerAggregate;
using Domain.SharedKernel.Base;

namespace Domain.ProductManagement;

public class BoMRevisionMaterial : Entity
{
    public Unit Unit { get; }
    public Money Price { get; }
    
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
    public virtual TransactionalPartner TransactionalPartner { get; }
    public virtual Material Material { get; }
    
    //Note: we will use this to achieve separate bounded context, bounded contexts don't depend on each other
    // the Material and Transactional partner belong to a different bounded context, so this one would be better.
    //public virtual Guid SupplierId { get; }
    //public virtual Guid MaterialId { get; }
    
    //required EF
    protected BoMRevisionMaterial() {}

    private BoMRevisionMaterial(Unit unit, Money price, Material material, TransactionalPartner supplier)
    {
        Unit = unit;
        Price = price;
        TransactionalPartner = supplier;
        Material = material;
        
        // SupplierId = supplier;
        // MaterialId = material;
    }

    public static Result<BoMRevisionMaterial> Create(Unit unit, Material material, TransactionalPartner supplier) 
    {
        var isSupplier = supplier.IsSupplier();
        if (isSupplier.IsFailure)
            return isSupplier.Error;

        var materialCost = material.GetMaterialCost(supplier);
        if (materialCost.IsFailure)
            return materialCost.Error;

        //maybe Price ValueObject need to GetCopy method due to shortcoming of EF Core ORM
        return new BoMRevisionMaterial(unit, materialCost.Value!.Price, material, supplier);
    }
}