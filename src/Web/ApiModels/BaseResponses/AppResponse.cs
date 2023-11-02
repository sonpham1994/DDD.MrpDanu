using Application.Helpers;
using Domain.SharedKernel.Base;

namespace Web.ApiModels.BaseResponses;

public record AppResponse(string TraceId, bool IsSuccess, IReadOnlyList<AppError> Errors, DateTime TimeGenerated)
{
    protected AppResponse(bool isSuccess, IReadOnlyList<AppError> errors) : this (Helper.GetTraceId(), isSuccess, errors, DateTime.UtcNow)
    {
    }

    public static AppResponse Success() => new(true, Array.Empty<AppError>());

    public static AppResponse Failure(IReadOnlyList<DomainError> domainErrors)
    {
        IReadOnlyList<AppError> errors = domainErrors.Select(x => new AppError(x.Code, x.Message)).ToList();
        return new(false, errors);
    }

    public static AppResponse Failure(IReadOnlyList<AppError> errors) => new(false, errors);

    public static AppResponse Failure(AppError error) => new(false, new List<AppError>(1) { error });
}

public sealed record AppResponse<T> : AppResponse
{
    public T Result { get; }
    
    private AppResponse(bool isSuccess, T result) : base(isSuccess, Array.Empty<AppError>())
    {
        Result = result;
    }
    
    public static AppResponse<T> Success(T result) => new(true, result);
}