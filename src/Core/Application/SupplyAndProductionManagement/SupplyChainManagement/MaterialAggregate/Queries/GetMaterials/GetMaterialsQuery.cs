using Application.Interfaces.Messaging;

namespace Application.SupplyChainManagement.MaterialAggregate.Queries.GetMaterials;

public sealed record GetMaterialsQuery : IQuery<IReadOnlyList<MaterialsResponse>>
{
}