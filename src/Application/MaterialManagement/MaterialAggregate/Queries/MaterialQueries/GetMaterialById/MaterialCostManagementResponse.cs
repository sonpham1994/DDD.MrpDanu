using Application.MaterialManagement.Shared;

namespace Application.MaterialManagement.MaterialAggregate.Queries.MaterialQueries.GetMaterialById;

public sealed record MaterialCostManagementResponse(decimal Price, uint MinQuantity, decimal Surcharge, SuppliersResponse Supplier);