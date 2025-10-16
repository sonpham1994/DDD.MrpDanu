namespace SharedKernel;

public static class LoggingDefinition
{
    public const string Prefix = "LoggingType: {LoggingType}";
    public const string Suffix = " - Time: {@DateTimeUtc}";
    public const string SuffixWithElapsedSecond = " in {ElapsedMs}ms" + Suffix;
    public const string Start = " start";
    public const string Completed = " completed";
    public const string FailedWithDomainError = " failed with error: {@Error}";
    public const string FailedWithError = " failed with error: {Error}";
}