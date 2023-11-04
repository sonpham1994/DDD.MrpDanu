using Application.Helpers;
using Domain.SharedKernel.Base;

namespace Api.ApiResponses;

public record ApiResponse(string TraceId, bool IsSuccess, IReadOnlyList<ApiError> Errors, DateTime TimeGenerated)
{
    protected ApiResponse(bool isSuccess, IReadOnlyList<ApiError> errors) : this (Helper.GetTraceId(), isSuccess, errors, DateTime.UtcNow)
    {
    }

    public static ApiResponse Success() => new(true, Array.Empty<ApiError>());

    public static ApiResponse Failure(IReadOnlyList<DomainError> domainErrors)
    {
        IReadOnlyList<ApiError> errors = domainErrors.Select(x => new ApiError(x.Code, x.Message)).ToList();
        return new(false, errors);
    }

    public static ApiResponse Failure(IReadOnlyList<ApiError> errors) => new(false, errors);

    public static ApiResponse Failure(ApiError error) => new(false, new List<ApiError>(1) { error });
}

public sealed record ApiResponse<T> : ApiResponse
{
    public T Result { get; }
    
    private ApiResponse(bool isSuccess, T result) : base(isSuccess, Array.Empty<ApiError>())
    {
        Result = result;
    }
    
    public static ApiResponse<T> Success(T result) => new(true, result);
}