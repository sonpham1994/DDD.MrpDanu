using Domain.SharedKernel.DomainClasses;
using Domain.MaterialManagement.MaterialAggregate;
using Domain.MaterialManagement.TransactionalPartnerAggregate;
using Domain.SharedKernel.Base;

namespace Domain.ProductManagement;

public class BoMRevisionMaterial : Entity<Guid>
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
    public virtual TransactionalPartner TransactionalPartner { get; }
    public virtual Material Material { get; }
    
    //required EF
    protected BoMRevisionMaterial() {}

    private BoMRevisionMaterial(Unit unit, Money price, Material material, TransactionalPartner supplier)
    {
        Unit = unit;
        Price = price;
        TransactionalPartner = supplier;
        Material = material;
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