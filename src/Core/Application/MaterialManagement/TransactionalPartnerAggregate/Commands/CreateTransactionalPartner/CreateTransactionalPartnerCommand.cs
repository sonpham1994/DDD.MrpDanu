using Application.Interfaces.Messaging;

namespace Application.MaterialManagement.TransactionalPartnerAggregate.Commands.CreateTransactionalPartner;

public sealed record CreateTransactionalPartnerCommand(
    string Name,
    string TaxNo,
    string Website,
    string ContactPersonName,
    string TelNo,
    string Email,
    AddressCommand Address,
    byte TransactionalPartnerTypeId,
    byte CurrencyTypeId,
    byte LocationTypeId) : ICommand;