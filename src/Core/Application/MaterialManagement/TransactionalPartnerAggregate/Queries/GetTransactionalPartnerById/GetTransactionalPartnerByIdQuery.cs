using Application.Interfaces.Messaging;

namespace Application.MaterialManagement.TransactionalPartnerAggregate.Queries.GetTransactionalPartnerById;

public sealed record GetTransactionalPartnerByIdQuery(Guid Id) : IQuery<TransactionalPartnerResponse>;