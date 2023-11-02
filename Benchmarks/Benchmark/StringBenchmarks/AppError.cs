namespace Benchmark.StringBenchmarks;

public sealed record AppError(string Code, string Message)
{
}

public static class AppErrors
{
    private const string InternalServerErrorCode = "InternalServerError";

    public static AppError CacheInternalServerCode(string message) => new(InternalServerErrorCode, message);

    public static AppError InternalServerCode(string message) => new("InternalServerError", message);
}