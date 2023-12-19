using Domain.SharedKernel.Base;

namespace Domain.MaterialManagement.MaterialAggregate;

public record struct MaterialCostManagementId(Guid Value) : IGuidStronglyTypedId;