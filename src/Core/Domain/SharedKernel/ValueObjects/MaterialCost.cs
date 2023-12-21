using Domain.Extensions;
using Domain.SharedKernel.Base;

namespace Domain.SharedKernel.ValueObjects;

public class MaterialCost : ValueObject
{
    private readonly TransactionalPartnerId _transactionalPartnerId;
    public MaterialId MaterialId { get; }
    public SupplierId SupplierId { get; }
    
    public Money Price { get; }

    //required EF
    protected MaterialCost() {}
    
    private MaterialCost(MaterialId materialId, SupplierId supplierId, Money price)
    {
        MaterialId = materialId;
        SupplierId = SupplierId;
        _transactionalPartnerId = new(supplierId.Value);
        Price = price;
    }

    public static Result<MaterialCost> Create(MaterialId materialId, SupplierId supplierId, Money price)
    {
        if (materialId.IsEmpty())
            return DomainErrors.MaterialCost.InvalidMaterialId(materialId);
        if (supplierId.IsEmpty())
            return DomainErrors.MaterialCost.InvalidSupplierId(supplierId);

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