using Domain.SharedKernel.Base;

namespace Infrastructure.Errors;

internal sealed class DomainErrors
{
    public static class AuditData
    {
        public static DomainError InvalidStateAuditData(string state) => new("AuditData.InvalidState", $"Cannot set AuditData to invalid entity state {state}");
        public static readonly DomainError Empty = new(string.Empty, string.Empty);
        public static DomainError NotSupportEntityAuditTypeYet(string entityType) => new("AuditData.NotSupportEntityAuditTypeYet", $"The entity type '{entityType}' does not support yet.");
    }
}