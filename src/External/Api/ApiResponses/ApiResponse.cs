namespace Api.ApiResponses;

public record ApiResponse(bool IsSuccess, IReadOnlyList<ApiError> Errors);

public record ApiResponse<T>(
    bool IsSuccess, 
    IReadOnlyList<ApiError> Errors, 
    T Result) : ApiResponse(IsSuccess, Errors);