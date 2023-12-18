using Domain.SharedKernel.Base;

namespace Domain.ProductManagement;

public sealed class DomainErrors
{
    public static class BoM
    {
        public static DomainError InvalidId(in uint bomId) => new("BoM.InvalidId", $"BoM id '{bomId}' is invalid.");
    }
    
    public static class BoMRevision
    {
        public static DomainError InvalidId(in ushort bomRevisionId) => new("BoMRevision.InvalidId", $"BoM revision id '{bomRevisionId}' is invalid.");
    }
    
    public static class BoMRevisionMaterial
    {
        public static DomainError InvalidUnit => new("BoMRevisionMaterial.InvalidUnit", "Unit should be a positive number or equal to 0.3, 0.003, 1.5, 0.002, 0.5, 0.05, or 0.005");
        public static DomainError InvalidMaterialId(in Guid materialId) => new("BoMRevisionMaterial.InvalidMaterialId", $"Material id '{materialId}' is invalid.");
        public static DomainError InvalidSupplierId(in Guid supplierId) => new("BoMRevisionMaterial.InvalidSupplierId", $"Supplier id '{supplierId}' is invalid.");
    }
}
