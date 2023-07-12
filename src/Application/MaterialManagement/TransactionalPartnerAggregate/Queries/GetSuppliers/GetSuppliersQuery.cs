using Application.Interfaces.Messaging;
using Application.MaterialManagement.Shared;

namespace Application.MaterialManagement.TransactionalPartnerAggregate.Queries.GetSuppliers;

public sealed record GetSuppliersQuery : IQuery<IReadOnlyList<SuppliersResponse>>;
