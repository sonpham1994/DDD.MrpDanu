namespace Application.MaterialManagement.MaterialAggregate.Commands.Models;

public class SupplierIdWithCurrencyTypeId
{
    public Guid Id { get; init; }
    public byte CurrencyTypeId { get; init; }
}