using Application.Interfaces.Messaging;
using Domain.SupplyChainManagement.MaterialAggregate;

namespace Application.SupplyChainManagement.MaterialAggregate.Queries.GetMaterialById;

public sealed record GetMaterialByIdQuery(Guid Id) : IQuery<MaterialResponse>
{
}