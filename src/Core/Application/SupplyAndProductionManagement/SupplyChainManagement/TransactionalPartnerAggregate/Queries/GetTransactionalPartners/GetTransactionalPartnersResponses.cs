namespace Application.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate.Queries.GetTransactionalPartners;

public sealed record TransactionalPartnersResponse(Guid Id, string Name, string TaxNo, string Website, string Type, string Currency);