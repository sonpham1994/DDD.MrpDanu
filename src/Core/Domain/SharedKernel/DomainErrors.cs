using Domain.SharedKernel.Base;
using Domain.SharedKernel.ValueObjects;

namespace Domain.SharedKernel;

public sealed class DomainErrors
{
    public static class CurrencyType
    {
        public static DomainError NotFoundId(in byte id) => new("CurrencyType.NotFoundId", $"Currency type id '{id}' does not exist.");
    }

    public static class Money
    {
        public static DomainError InvalidMoney => new("Money.InvalidMoney", "Money should not be less than or equal to 0");
        public static DomainError InvalidVNDCurrencyMoney => new("Money.InvalidVNDCurrencyMoney", "Money should be integer for VND currency");
    }

    public static class MaterialCost
    {
        public static DomainError InvalidMaterialId(in MaterialId materialId) => new("BoMRevisionMaterial.InvalidMaterialId", $"Material id '{materialId.Value}' is invalid.");
        public static DomainError InvalidSupplierId(in SupplierId supplierId) => new("BoMRevisionMaterial.InvalidSupplierId", $"Supplier id '{supplierId.Value}' is invalid.");
    }
    
    public static DomainError NullRequestBodyParameter => new("General.NullRequestBodyParameter", "Request body parameter should not be null");

    public static DomainError SafeFail => new("SafeFail", "DomainError cannot null if process is fail");

    public static DomainError SafeFailWithCode(string code) => new($"SafeFail.{code}", $"Should check failure for {code} before executing operation.");
    
    public static DomainError EnumerationNull(string typeName, in byte id) => new DomainError("Enumeration.Null", $"Cannot get {typeName} by id '{id}'");
}