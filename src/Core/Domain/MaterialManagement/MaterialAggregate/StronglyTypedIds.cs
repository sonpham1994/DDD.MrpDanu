using Domain.SharedKernel.Base;

namespace Domain.MaterialManagement.MaterialAggregate;

public record struct MaterialSupplierCostId(Guid Value) : IGuidStronglyTypedId;