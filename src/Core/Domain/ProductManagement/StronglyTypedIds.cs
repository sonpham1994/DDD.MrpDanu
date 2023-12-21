using Domain.SharedKernel.Base;

namespace Domain.ProductManagement;

public record struct BoMId(uint Value);

public record struct ProductId(uint Value);

public record struct BoMRevisionId(ushort Value);

public record struct BoMRevisionMaterialId(Guid Value) : IGuidStronglyTypedId
{
    public static explicit operator BoMRevisionMaterialId(Guid value)
        => new(value);
}