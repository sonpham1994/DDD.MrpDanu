namespace SharedKernel;

public interface IGuidStronglyTypedId : IStronglyTypedId<Guid>;

public interface IStronglyTypedId<T>
    where T : IEquatable<T>
{
    public T Value { get; }
}