using Domain.SharedKernel.Base;

namespace Domain.MaterialManagement.TransactionalPartnerAggregate;

public record struct ContactPersonInformationId(Guid Value) : IGuidStronglyTypedId;