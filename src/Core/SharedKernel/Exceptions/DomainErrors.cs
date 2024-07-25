namespace SharedKernel.Exceptions;

public static class DomainErrors
{
    public static DomainError SafeFail => new("SafeFail", "DomainError cannot null if process is fail");
    public static DomainError SafeFailWithCode(string code) => new($"SafeFail.{code}", $"Should check failure for error code '{code}' before executing operation.");
    public static class ItemDuplication
    {
        public static DomainError NotSupportType(string type) => new("ItemDuplication.NotSupportType", $"DDD.MrpDanu.Fail: Does not support type of '{type}' yet.");
    }
}