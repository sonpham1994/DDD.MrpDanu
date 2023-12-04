using Application.Interfaces.Messaging;

namespace Application.MaterialManagement.MaterialAggregate.Queries.GetMaterials;

public sealed record GetMaterialsQuery : IQuery<IReadOnlyList<MaterialsResponse>>
{
}