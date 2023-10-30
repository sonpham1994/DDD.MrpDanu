using Application.Helpers;
using Domain.SharedKernel.Base;

namespace Web.ApiModels.BaseResponses;

public record AppResponse
{
    public string TraceId { get; }
    public bool IsSuccess { get; }
    public IReadOnlyList<AppError> Errors { get; }
    public DateTime TimeGenerated { get; }

    protected AppResponse(bool isSuccess, IReadOnlyList<AppError> errors)
    {
        TraceId = Helper.GetTraceId();
        IsSuccess = isSuccess;
        Errors = errors;
        TimeGenerated = DateTime.UtcNow;
    }

    public static AppResponse Success()
    {
        return new AppResponse(true, Array.Empty<AppError>());
    }

    public static AppResponse Error(IReadOnlyList<DomainError> domainErrors)
    {
        IReadOnlyList<AppError> errors = domainErrors.Select(x => new AppError(x.Code, x.Message)).ToList();
        return new AppResponse(false, errors);
    }

    public static AppResponse Error(IReadOnlyList<AppError> errors)
    {
        return new AppResponse(false, errors);
    }

    public static AppResponse Error(AppError error)
    {
        IReadOnlyList<AppError> errors = new List<AppError> { error };
        return new AppResponse(false, errors);
    }
}

public sealed record AppResponse<T> : AppResponse
{
    public T Result { get; }
    
    private AppResponse(bool isSuccess, IReadOnlyList<AppError> errors) : base(isSuccess, errors)
    {
    }

    private AppResponse(bool isSuccess, T result) : this(isSuccess, Array.Empty<AppError>())
    {
        Result = result;
    }
    
    public static AppResponse<T> Success(T result)
    {
        return new AppResponse<T>(true, result);
    }
}