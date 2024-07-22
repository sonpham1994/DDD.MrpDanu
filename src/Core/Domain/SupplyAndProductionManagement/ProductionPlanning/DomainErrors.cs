using SharedKernel;

namespace Domain.SupplyAndProductionManagement.ProductionPlanning;

public sealed class DomainErrors
{
    public static class BoM
    {
        public static DomainError InvalidId(in uint bomId) => new($"{nameof(BoM)}.{nameof(InvalidId)}", $"BoM id '{bomId}' is invalid.");
    }
    
    public static class BoMRevision
    {
        public static DomainError InvalidId(in ushort bomRevisionId) => new($"{nameof(BoMRevision)}.{nameof(InvalidId)}", $"BoM revision id '{bomRevisionId}' is invalid.");

        public static DomainError EmptyConfirmation => new($"{nameof(BoMRevision)}.{nameof(EmptyConfirmation)}", $"Confirmation should not be empty.");
    }
    
    public static class BoMRevisionMaterial
    {
        public static DomainError InvalidUnit => new($"{nameof(BoMRevisionMaterial)}.{nameof(InvalidUnit)}", "Unit should be a positive number or equal to 0.3, 0.003, 1.5, 0.002, 0.5, 0.05, or 0.005");
    }
}
