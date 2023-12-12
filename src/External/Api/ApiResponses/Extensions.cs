using Domain.SharedKernel.Base;

namespace Api.ApiResponses;

public static class ApiResponseExtensions
{
    private static ApiError ToApiError(this DomainError error) => new(error.Code, error.Message);
    public static ApiResponse Success() => new(true, Array.Empty<ApiError>());
    public static ApiResponse Failure(IReadOnlyList<ApiError> errors) => new(false, errors);
    public static ApiResponse Failure(ApiError error) => new(false, new List<ApiError>(1) { error });
    public static ApiResponse Failure(DomainError error) => new(false, new List<ApiError>(1) { error.ToApiError() });
    
    public static ApiResponse Failure(IReadOnlyList<DomainError> errors)
    {
        var apiErrors = errors.Select(x => x.ToApiError()).ToList();
        return new(false, apiErrors);
    }
}

public static class ApiResponseExtensions<T>
{
    public static ApiResponse<T> Failure(IReadOnlyList<ApiError> errors) => new(false, errors, default);

    public static ApiResponse<T> Failure(ApiError error) => new(false, new List<ApiError>(1) { error }, default);
    public static ApiResponse<T> Success(T result) => new(true, Array.Empty<ApiError>(), result);
}
