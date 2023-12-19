using Domain.SharedKernel.Base;
using Domain.SharedKernel.ValueObjects;

namespace Domain.SharedKernel.ValueObjects;

public class MaterialCost : ValueObject
{
    // we can use strongly typed ids for MaterialId and SupplierId, but the problem is that MaterialAggregate
    //use another approach so we leave this Guid aside for the time being.
    public MaterialId MaterialId { get; }
    public SupplierId SupplierId { get; }
    
    public Money Price { get; }

    //required EF
    protected MaterialCost() {}
    
    private MaterialCost(MaterialId materialId, SupplierId supplierId, Money price)
    {
        MaterialId = materialId;
        SupplierId = supplierId;
        Price = price;
    }

    public static Result<MaterialCost> Create(MaterialId materialId, SupplierId supplierId, Money price)
    {
        if (supplierId.Value == Guid.Empty)
            return DomainErrors.MaterialCost.InvalidSupplierId(supplierId.Value);

        return new MaterialCost(materialId, supplierId, price);
    }
    
    protected override IEnumerable<int> GetHashCodeComponents()
    {
        yield return MaterialId.GetHashCode();
        yield return SupplierId.GetHashCode();
        yield return Price.GetHashCode();
    }

    protected override bool EqualComponents(ValueObject valueObject)
    {
        if (valueObject is not MaterialCost materialCost)
            return false;
        if (MaterialId != materialCost.MaterialId)
            return false;
        if (SupplierId != materialCost.SupplierId)
            return false;
        if (Price != materialCost.Price)
            return false;

        return true;
    }
}