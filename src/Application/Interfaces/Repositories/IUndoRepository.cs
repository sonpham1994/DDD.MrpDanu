namespace Application.Interfaces.Repositories;

public interface IUndoRepository
{
    Task RestoreAsync(Guid id, string tableName);
    Task RestoreAsync(CancellationToken cancellationToken = default);
}