using SharedKernel.Enumerations;
using SharedKernel.ValueObjects;

namespace Domain.Services.GetListSupplierIdAndCurrencyTypeIdServices;

public class SupplierIdWithCurrencyTypeId
{
    public Guid SupplierId { get; init; }
    public byte CurrencyTypeId { get; init; }
}

public readonly struct SupplierIdWithCurrencyType
{
    public SupplierId SupplierId { get; init; }
    public CurrencyType CurrencyType { get; init; }
    
    public static explicit operator SupplierIdWithCurrencyType(SupplierIdWithCurrencyTypeId data)
        => new() { SupplierId = (SupplierId)data.SupplierId, CurrencyType = CurrencyType.FromId(data.CurrencyTypeId).Value! };
}