using Domain.SharedKernel.Base;

namespace Domain.SharedKernel.ValueObjects;

public record struct MaterialId(Guid Value) : IGuidStronglyTypedId;

public record struct SupplierId(Guid Value) : IGuidStronglyTypedId;

public record struct TransactionalPartnerId(Guid Value) : IGuidStronglyTypedId;