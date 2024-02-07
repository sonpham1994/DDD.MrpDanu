using Domain.SharedKernel.Base;

namespace Domain.SupplyAndProductionManagement.ProductionPlanning;

public readonly record struct BoMId(uint Value) : IStronglyTypedId<uint>;

public readonly record struct ProductId(uint Value) : IStronglyTypedId<uint>;

public readonly record struct BoMRevisionId(ushort Value) : IStronglyTypedId<ushort>;

public readonly record struct BoMRevisionMaterialId(Guid Value) : IGuidStronglyTypedId
{
    public static explicit operator BoMRevisionMaterialId(Guid value)
        => new(value);
}