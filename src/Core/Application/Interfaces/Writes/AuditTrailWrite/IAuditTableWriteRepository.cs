using Domain.AuditTrail.SharedKernel;

namespace Application.Interfaces.Writes.AuditTrailWrite;

public interface IAuditTableWriteRepository<T> where T : AuditEntity
{
    Task InsertAsync(T entity);
}