using Domain.SharedKernel.Base;

namespace Domain.Errors;

public sealed class ProductManagementDomainErrors
{
    public static class BoMRevisionMaterial
    {
        public static DomainError InvalidUnit => new("BoMRevisionMaterial.InvalidUnit", "Unit should be a positive number or equal to 0.3, 0.003, 1.5, 0.002, 0.5, 0.05, or 0.005");
    }
}
