using Domain.SharedKernel.Base;

namespace Domain.MaterialManagement.TransactionalPartnerAggregate;

public record struct ContactPersonInformationId(Guid Value) : IGuidStronglyTypedId
{
    public static explicit operator ContactPersonInformationId(Guid value)
        => new(value);
}