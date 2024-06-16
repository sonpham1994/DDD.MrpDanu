using Application.Interfaces.Messaging;

namespace Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Queries.GetMaterials;

public sealed record GetMaterialsQuery : IQueryList<MaterialsResponse>;