namespace Domain.SharedKernel.Base;

//https://github.com/vkhorikov/CSharpFunctionalExtensions
public readonly struct Result : IResult
{
    private readonly DomainError _error;
    private readonly bool _isSuccess;
    
    public bool IsFailure => !IsSuccess;

    public bool IsSuccess
    {
        get
        {
            this.CheckSafeFailResult(_isSuccess, _error);
            return _isSuccess;
        }
    }

    public DomainError Error
    {
        get
        {
            this.CheckSafeFailResult(_isSuccess, _error);
            return _error;
        }
    }

    private Result(in bool isSuccess, in DomainError error)
    {
        _isSuccess = isSuccess;
        _error = error;
        this.CheckSafeFailResult(_isSuccess, _error);
    }

    public static Result Success()
        => new(true, DomainError.Empty);

    public static Result<T> Success<T>(T value)
        => new(true, DomainError.Empty, value);

    public static Result Failure(in DomainError error)
        => new(false, error);

    public static implicit operator Result(in DomainError domainError)
        => Failure(domainError);
}