namespace SharedKernel;

public static class DomainErrors
{
    public static DomainError NullRequestBodyParameter => new("General.NullRequestBodyParameter", "Request body parameter should not be null");
    
    public static DomainError EnumerationNull(string typeName, in byte id) => new DomainError("Enumeration.Null", $"Cannot get {typeName} by id '{id}'");
}