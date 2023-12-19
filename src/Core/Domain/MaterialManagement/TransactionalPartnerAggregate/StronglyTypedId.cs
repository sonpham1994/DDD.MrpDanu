using Domain.SharedKernel.Base;

namespace Domain.MaterialManagement.TransactionalPartnerAggregate;

public record struct TransactionalPartnerId(Guid Value) : IGuidStronglyTypedId;