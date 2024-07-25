namespace SharedKernel.ValueObjects;

public static class DomainErrors
{
    public static class Money
    {
        public static DomainError InvalidMoney => new("Money.InvalidMoney", "Money should not be less than or equal to 0");
        public static DomainError InvalidVndCurrencyMoney => new("Money.InvalidVNDCurrencyMoney", "Money should be integer for VND currency");
    }

    public static class MaterialSupplierIdentity
    {
        public static DomainError InvalidMaterialId(in MaterialId materialId) => new("MaterialSupplierIdentity.InvalidMaterialId", $"Material id '{materialId.Value}' is invalid.");
        public static DomainError InvalidSupplierId(in SupplierId supplierId) => new("MaterialSupplierIdentity.InvalidSupplierId", $"Supplier id '{supplierId.Value}' is invalid.");
    }
}