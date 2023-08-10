using Domain.Errors;
using Domain.SharedKernel.Base;

namespace Domain.MaterialManagement.TransactionalPartnerAggregate;

public class TransactionalPartnerType : Enumeration<TransactionalPartnerType>, IEquatable<TransactionalPartnerType>
{
    public static readonly TransactionalPartnerType Customer = new(1, nameof(Customer));
    public static readonly TransactionalPartnerType Supplier = new(2, nameof(Supplier));
    public static readonly TransactionalPartnerType Both = new(3, nameof(Both));

    protected TransactionalPartnerType() { }

    private TransactionalPartnerType(byte id, string name) : base(id, name)
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

    public new static Result<TransactionalPartnerType> FromId(byte id)
    {
        var result = Enumeration<TransactionalPartnerType>.FromId(id);

        if (result.IsFailure)
            return MaterialManagementDomainErrors.TransactionalPartnerType.NotFoundId(id);

        return result;
    }

    public static Span<TransactionalPartnerType> GetSupplierTypes()
    {
        Span<TransactionalPartnerType> result = ((TransactionalPartnerType[])List).AsSpan(1, 2);
        return result;
    }
}