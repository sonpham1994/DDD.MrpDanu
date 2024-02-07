using Domain.SharedKernel.Base;

namespace Domain.SharedKernel.ValueObjects;

public readonly record struct MaterialId(Guid Value) : IGuidStronglyTypedId
{
    public static explicit operator MaterialId(Guid value)
        => new(value);
}

public readonly record struct SupplierId(Guid Value) : IGuidStronglyTypedId
{
    public static explicit operator SupplierId(Guid value)
        => new(value);
    // supplierId is also transactionalPartnerId, but transactionalPartnerId is
    // not sure it's supplierId's yet. So we don't introduce the converting from
    // transactionalPartnerId to supplierId here.
}

public readonly record struct TransactionalPartnerId(Guid Value) : IGuidStronglyTypedId
{
    public static explicit operator TransactionalPartnerId(Guid value)
        => new(value);
    // supplierId is also transactionalPartnerId, but transactionalPartnerId is
    // not sure it's supplierId's yet. So we don't introduce the convert from
    // transactionalPartnerId to supplierId here.
    public static explicit operator TransactionalPartnerId(SupplierId supplierId)
        => new(supplierId.Value);
}