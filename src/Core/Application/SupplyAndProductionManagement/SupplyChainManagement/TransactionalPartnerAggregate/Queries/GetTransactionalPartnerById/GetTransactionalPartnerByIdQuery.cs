using Application.Interfaces.Messaging;

namespace Application.SupplyChainManagement.TransactionalPartnerAggregate.Queries.GetTransactionalPartnerById;

public sealed record GetTransactionalPartnerByIdQuery(Guid Id) : IQuery<TransactionalPartnerResponse>;