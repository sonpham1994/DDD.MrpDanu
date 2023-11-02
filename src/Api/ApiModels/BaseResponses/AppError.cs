using Domain.SharedKernel.Base;

namespace Web.ApiModels.BaseResponses;

public sealed record AppError(string Code, string Message)
{
    public static implicit operator AppError(in DomainError domainError) => new(domainError.Code, domainError.Message);
}

public static class AppErrors
{

    //This is the way we think each request the program will allocate "InternalServerError" value, but it's not correct. The .Net core will cache
    //this data for us
    //public static AppError InternalServerError(string message) => new("InternalServerError", message);

    /*basically, we cache the "InternalServerError" to reuse for others and don't allocate more memory. But in .Net, these static string which
     * do not any parameter to modify these string for each request will be reused from value in complie-time which improved from .Net Core.
     * Hence if we don't cache the value like this "InternalServerErrorCode", because the .Net core will cache data in the string intern pool 
     * and it resue that data from string intern pool. But we can use this cache data technique for clean code. 
     * Please check Benchmark.StringBenchmarks
     */

    private const string InternalServerErrorCode = "InternalServerError";

    public static readonly AppError InternalServerErrorOnProduction = new(InternalServerErrorCode, "Internal server error. Please contact admin to support.");

    public static AppError InternalServerError(string message) => new(InternalServerErrorCode, message);

    
}