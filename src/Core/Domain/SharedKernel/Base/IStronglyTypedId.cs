namespace Domain.SharedKernel.Base;

public interface IGuidStronglyTypedId : IStronglyTypedId<Guid>
{
}

public interface IStronglyTypedId<T>
    where T : struct, IEquatable<T>
{
    public T Value { get; }
}