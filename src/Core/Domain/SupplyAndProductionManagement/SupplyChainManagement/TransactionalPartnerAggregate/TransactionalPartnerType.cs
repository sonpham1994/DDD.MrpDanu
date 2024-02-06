using Domain.SharedKernel.Base;
using Domain.SupplyAndProductionManagement.SupplyChainManagement;

namespace Domain.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate;

public class TransactionalPartnerType : Enumeration<TransactionalPartnerType>
    , IEquatable<TransactionalPartnerType> // for span using IndexOf
{
    public static readonly TransactionalPartnerType Customer = new(1, nameof(Customer));
    public static readonly TransactionalPartnerType Supplier = new(2, nameof(Supplier));
    public static readonly TransactionalPartnerType Both = new(3, nameof(Both));

    protected TransactionalPartnerType() { }

    private TransactionalPartnerType(in byte id, string name) : base(id, name)
    {
    }

    public bool Equals(TransactionalPartnerType? value)
    {
        if (value is null)
            return false;
        if (ReferenceEquals(this, value))
            return true;
        if (Id == value.Id)
            return true;

        return false;
    }

    public new static Result<TransactionalPartnerType?> FromId(in byte id)
    {
        var result = Enumeration<TransactionalPartnerType>.FromId(id);

        if (result.IsFailure)
            return DomainErrors.TransactionalPartnerType.NotFoundId(id);

        return result;
    }

    public static Span<TransactionalPartnerType> GetSupplierTypes()
    {
        //https://www.youtube.com/watch?v=FM5dpxJMULY&ab_channel=NickChapsas
        Span<TransactionalPartnerType> result = list.AsSpan(1, 2);
        return result;
    }

    public static bool IsSupplierType(TransactionalPartnerType value)
    {
        var suppliers = GetSupplierTypes();
        var indexOfSupplierType = suppliers.IndexOf(value);

        if (indexOfSupplierType == -1)
            return false;

        return true;
    }
}