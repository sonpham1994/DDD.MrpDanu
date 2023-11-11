using Application.Interfaces.Messaging;

namespace Application.MaterialManagement.TransactionalPartnerAggregate.Commands.UpdateTransactionalPartner;

public sealed record UpdateTransactionalPartnerCommand(
    Guid Id,
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