using Application.Interfaces.Messaging;

namespace Application.MaterialManagement.TransactionalPartnerAggregate.Commands.UpdateTransactionalPartner;

public sealed record UpdateTransactionalPartnerCommand : ICommand
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string TaxNo { get; init; }
    public string Website { get; init; }
    
    public string ContactPersonName { get; init; }
    public string TelNo { get; init; }
    public string Email { get; init; }
    public AddressCommand Address { get; init; }
    public byte TransactionalPartnerTypeId { get; init; }
    public byte CurrencyTypeId { get; init; }
    public byte LocationTypeId { get; init; }
}