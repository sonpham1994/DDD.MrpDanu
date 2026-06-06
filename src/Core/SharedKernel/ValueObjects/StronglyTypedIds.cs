using SharedKernel.Exceptions;

namespace SharedKernel.ValueObjects;

public readonly record struct MaterialId(in Guid Value) : IGuidStronglyTypedId
{
    public static explicit operator MaterialId(in Guid value)
        => new(value);
}

public readonly record struct SupplierId(in Guid Value) : IGuidStronglyTypedId
{
    public static explicit operator SupplierId(in Guid value)
        => new(value);
    // supplierId is also transactionalPartnerId, but transactionalPartnerId is
    // not sure it's supplierId's yet. So we don't introduce the converting from
    // transactionalPartnerId to supplierId here.
}

public readonly record struct TransactionalPartnerId(in Guid Value) : IGuidStronglyTypedId
{
    public static explicit operator TransactionalPartnerId(in Guid value)
        => new(value);
    // supplierId is also transactionalPartnerId, but transactionalPartnerId is
    // not sure it's supplierId's yet. So we don't introduce the convert from
    // transactionalPartnerId to supplierId here.
    public static explicit operator TransactionalPartnerId(in SupplierId supplierId)
        => new(supplierId.Value);
}