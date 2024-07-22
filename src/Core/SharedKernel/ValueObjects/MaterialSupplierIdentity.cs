using SharedKernel.Extensions;

namespace SharedKernel.ValueObjects;

//whenever we compare the MaterialSupplierCost entity, we always compare the MaterialId and SupplierId whether
//MaterialSupplierCost exist the same MaterialId and SupplierId or not. Hence I decide to make 2 these properties as
// value object
public class MaterialSupplierIdentity : ValueObject<MaterialSupplierIdentity>
{
    private readonly TransactionalPartnerId _transactionalPartnerId;
    public MaterialId MaterialId { get; }
    public SupplierId SupplierId => new(_transactionalPartnerId.Value);

    //required EF
    protected MaterialSupplierIdentity() { }

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

    protected override bool EqualComponents(MaterialSupplierIdentity other)
        => MaterialId == other.MaterialId 
            && _transactionalPartnerId == other._transactionalPartnerId;
}