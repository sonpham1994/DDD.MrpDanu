using SharedKernel;

namespace MinimalApi.ApiResponses;

public static class ApiResponseExtensions
{
    private static ApiError ToApiError(this in DomainError error) => new(error.Code, error.Message);
    public static ApiResponse ToApiSuccess() => new(true, Array.Empty<ApiError>());

    public static ApiResponse<T> ToApiSuccess<T>(this T result) 
        => ApiResponseExtensions<T>.ToApiSuccess(result);
    
    public static ApiResponse ToApiFailure(this IReadOnlyList<ApiError> errors) => new(false, errors);
    public static ApiResponse ToApiFailure(this ApiError error) => new(false, new List<ApiError>(1) { error });
    public static ApiResponse ToApiFailure(this in DomainError error) => new(false, new List<ApiError>(1) { error.ToApiError() });

    public static ApiResponse ToApiFailure(this IReadOnlyList<DomainError> errors)
    {
        var apiErrors = errors.Select(x => x.ToApiError()).ToList();
        return new(false, apiErrors);
    }
}

public static class ApiResponseExtensions<T>
{
    public static ApiResponse<T> ToApiFailure(IReadOnlyList<ApiError> errors) => new(false, errors, default!);

    public static ApiResponse<T> ToApiFailure(ApiError error) => new(false, new List<ApiError>(1) { error }, default!);
    public static ApiResponse<T> ToApiSuccess(T result) => new(true, Array.Empty<ApiError>(), result);
}
