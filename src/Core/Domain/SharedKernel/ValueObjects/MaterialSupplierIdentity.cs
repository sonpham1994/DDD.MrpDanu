using Domain.Extensions;
using Domain.SharedKernel.Base;

namespace Domain.SharedKernel.ValueObjects;

//whenever we compare the MaterialSupplierCost entity, we always compare the MaterialId and SupplierId whether
//MaterialSupplierCost exist the same MaterialId and SupplierId or not. Hence I decide to make 2 these properties as
// value object
public class MaterialSupplierIdentity : ValueObject
{
    private readonly TransactionalPartnerId _transactionalPartnerId;
    public MaterialId MaterialId { get; }
    public SupplierId SupplierId => new(_transactionalPartnerId.Value);

    //required EF
    protected MaterialSupplierIdentity() {}
    
    private MaterialSupplierIdentity(MaterialId materialId, SupplierId supplierId)
    {
        MaterialId = materialId;
        _transactionalPartnerId = (TransactionalPartnerId)supplierId;
    }

    public static Result<MaterialSupplierIdentity?> Create(MaterialId materialId, SupplierId supplierId)
    {
        if (materialId.IsEmpty())
            return DomainErrors.MaterialSupplierIdentity.InvalidMaterialId(materialId);
        if (supplierId.IsEmpty())
            return DomainErrors.MaterialSupplierIdentity.InvalidSupplierId(supplierId);

        return new MaterialSupplierIdentity(materialId, supplierId);
    }
    
    protected override IEnumerable<int> GetHashCodeComponents()
    {
        yield return MaterialId.GetHashCode();
        yield return _transactionalPartnerId.GetHashCode();
    }

    protected override bool EqualComponents(ValueObject valueObject)
    {
        if (valueObject is not MaterialSupplierIdentity other)
            return false;
        if (MaterialId != other.MaterialId)
            return false;
        if (_transactionalPartnerId != other._transactionalPartnerId)
            return false;

        return true;
    }
}