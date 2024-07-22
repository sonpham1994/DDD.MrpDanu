using SharedKernel;

namespace SharedKernel.Extensions;

public static class DomainErrors
{
    public static class ItemDuplication
    {
        public static DomainError NotSupportType(string type) => new("ItemDuplication.NotSupportType", $"DDD.MrpDanu.Fail: Does not support type of '{type}' yet.");
    }
    
    public static DomainError SafeFail => new("SafeFail", "DomainError cannot null if process is fail");
}
