namespace Domain.SharedKernel.Base;

public interface IResult
{
    bool IsFailure { get; }
    bool IsSuccess { get; }
    DomainError Error { get; }
}

public interface IResult<T> : IResult
{
    T Value { get; }
}