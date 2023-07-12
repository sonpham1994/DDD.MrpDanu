using Application.Interfaces.Messaging;

namespace Application.MaterialManagement.TransactionalPartnerAggregate.Commands.DeleteTransactionalPartner;

public sealed record DeleteTransactionalPartnerCommand(Guid Id) : ICommand;