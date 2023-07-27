using Domain.Errors;
using Domain.SharedKernel.Base;

namespace Domain.MaterialManagement.TransactionalPartnerAggregate;

public class TransactionalPartnerType : Enumeration<TransactionalPartnerType>
{
    public static readonly TransactionalPartnerType Customer = new(1, nameof(Customer));
    public static readonly TransactionalPartnerType Supplier = new(2, nameof(Supplier));
    public static readonly TransactionalPartnerType Both = new(3, nameof(Both));

    protected TransactionalPartnerType() { }

    private TransactionalPartnerType(byte id, string name) : base(id, name)
    {
    }

    public new static Result<TransactionalPartnerType> FromId(byte id)
    {
        var result = Enumeration<TransactionalPartnerType>.FromId(id);

        if (result.IsFailure)
            return MaterialManagementDomainErrors.TransactionalPartnerType.NotFoundId(id);

        return result;
    }

    public static IReadOnlyList<TransactionalPartnerType> GetSupplierTypes()
    {
        const byte countSuppliers = 2;
        return new TransactionalPartnerType[countSuppliers]
        {
            Supplier,
            Both
        };
    }
}