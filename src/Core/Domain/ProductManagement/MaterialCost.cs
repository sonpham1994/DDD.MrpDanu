using Domain.SharedKernel.Base;
using Domain.SharedKernel.DomainClasses;

namespace Domain.ProductManagement;

public class MaterialCost : ValueObject
{
    public Guid MaterialId { get; }
    public Guid SupplierId { get; }
    public Money Price { get; }

    private MaterialCost(Guid materialId, Guid supplierId, Money price)
    {
        MaterialId = materialId;
        SupplierId = supplierId;
        Price = price;
    }

    public static Result<MaterialCost> Create(Guid materialId, Guid supplierId, Money price)
    {
        if (materialId == Guid.Empty)
            return DomainErrors.BoMRevisionMaterial.InvalidMaterialId(materialId);
        if (supplierId == Guid.Empty)
            return DomainErrors.BoMRevisionMaterial.InvalidSupplierId(supplierId);

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