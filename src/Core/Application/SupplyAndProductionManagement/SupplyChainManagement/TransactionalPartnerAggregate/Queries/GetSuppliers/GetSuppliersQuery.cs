using Application.Interfaces.Messaging;
using Application.SupplyChainManagement.Shared;

namespace Application.SupplyChainManagement.TransactionalPartnerAggregate.Queries.GetSuppliers;

public sealed record GetSuppliersQuery : IQuery<IReadOnlyList<SuppliersResponse>>;
