using Domain.Extensions.LinqExtensions;
using Domain.SharedKernel.Base;

namespace Domain.Extensions;

public static class DomainErrors
{
    public static class ItemDuplication
    {
        public static DomainError NotSupportType(string type) => new("ItemDuplication.NotSupportType", $"DDD.MrpDanu.Fail: Does not support type of '{type}' yet.");
    }
}
