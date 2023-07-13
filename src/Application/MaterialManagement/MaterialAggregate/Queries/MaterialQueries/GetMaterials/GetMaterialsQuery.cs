using Application.Interfaces.Messaging;

namespace Application.MaterialManagement.MaterialAggregate.Queries.MaterialQueries.GetMaterials;

public sealed record GetMaterialsQuery : IQuery<IReadOnlyList<MaterialsResponse>>
{
}