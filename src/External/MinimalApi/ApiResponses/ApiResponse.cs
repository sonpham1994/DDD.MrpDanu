namespace MinimalApi.ApiResponses;

public record ApiResponse(bool IsSuccess, IReadOnlyList<ApiError> Errors);

public record ApiResponse<T>(
    bool IsSuccess, 
    IReadOnlyList<ApiError> Errors, 
    T Result) : ApiResponse(IsSuccess, Errors);
    
public record ApiCollectionResponse<T>(
    bool IsSuccess, 
    IReadOnlyList<ApiError> Errors, 
    IReadOnlyList<T> Result) : ApiResponse(IsSuccess, Errors);