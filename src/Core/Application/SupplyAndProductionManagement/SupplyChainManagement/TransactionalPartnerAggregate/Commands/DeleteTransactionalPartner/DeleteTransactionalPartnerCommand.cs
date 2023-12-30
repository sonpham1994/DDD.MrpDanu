using Application.Interfaces.Messaging;

namespace Application.SupplyChainManagement.TransactionalPartnerAggregate.Commands.DeleteTransactionalPartner;

public sealed record DeleteTransactionalPartnerCommand(Guid Id) : ICommand;