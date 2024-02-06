using Application.Interfaces.Messaging;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;

namespace Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Queries.GetMaterialById;

public sealed record GetMaterialByIdQuery(Guid Id) : IQuery<MaterialResponse>
{
}