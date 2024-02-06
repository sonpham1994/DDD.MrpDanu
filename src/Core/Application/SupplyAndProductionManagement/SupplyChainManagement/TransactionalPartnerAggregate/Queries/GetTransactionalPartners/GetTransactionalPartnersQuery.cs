using Application.Interfaces.Messaging;

namespace Application.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate.Queries.GetTransactionalPartners;

public sealed record GetTransactionalPartnersQuery : IQuery<IReadOnlyList<TransactionalPartnersResponse>>;