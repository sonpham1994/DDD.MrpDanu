namespace Application.LoggingDefinitions;

public static class SharedLoggingDefinition
{
    public const string Prefix = "LoggingType: {LoggingType}";
    public const string Suffix = " - Time: {@DateTimeUtc}";
    public const string Start = " start";
    public const string Completed = " completed";
    public const string FailedWithDomainError = " failed with error: {@Error}";
    public const string FailedWithError = " failed with error: {Error}";
}