namespace Domain.SharedKernel.Base;

public interface IResult
{
    bool IsSuccess { get; }
    DomainError Error { get; }
}

public interface IResult<T> : IResult
{
    T Value { get; }
}