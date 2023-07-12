using Application.Interfaces.Messaging;
using Domain.MaterialManagement.MaterialAggregate;

namespace Application.MaterialManagement.MaterialAggregate.Queries.MaterialQueries.GetMaterialById;

public sealed record GetMaterialByIdQuery(Guid Id) : IQuery<MaterialResponse>
{
}