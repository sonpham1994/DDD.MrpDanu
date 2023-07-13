using Application.Helpers;
using Domain.SharedKernel.Base;

namespace Web.ApiModels.BaseResponses;

public sealed record AppResponse
{
    public string TraceId { get; }
    public bool IsSuccess { get; }
    public IReadOnlyList<AppError> Errors { get; }
    public DateTime TimeGenerated { get; }
    public object Result { get; }
    
    private AppResponse(bool isSuccess, IReadOnlyList<AppError> errors)
    {
        TraceId = Helper.GetTraceId();
        IsSuccess = isSuccess;
        Errors = errors;
        TimeGenerated = DateTime.UtcNow;
    }

    private AppResponse(bool isSuccess, object result) : this(isSuccess, Array.Empty<AppError>())
    {
        Result = result;
    }
    
    public static AppResponse Success()
    {
        return new AppResponse(true, Array.Empty<AppError>());
    }
    
    public static AppResponse Success(object result)
    {
        return new AppResponse(true, result);
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