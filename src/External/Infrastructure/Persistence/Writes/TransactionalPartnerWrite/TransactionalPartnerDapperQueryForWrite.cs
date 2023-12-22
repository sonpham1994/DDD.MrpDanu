using Application.Interfaces.Writes.TransactionalPartnerWrite;
using Application.MaterialManagement.MaterialAggregate.Commands.Models;
using System.Data;

namespace Infrastructure.Persistence.Writes.TransactionalPartnerWrite;

internal sealed class TransactionalPartnerDapperQueryForWrite : ITransactionalPartnerQueryForWrite
{
    private readonly IDbConnection _dbConnection;
    public TransactionalPartnerDapperQueryForWrite(IDbConnection dbConnection)
        => _dbConnection = dbConnection;

    public async Task<IReadOnlyList<SupplierIdWithCurrencyTypeId>> GetSupplierIdsWithCurrencyTypeIdBySupplierIdsAsync(IReadOnlyList<Guid> ids, CancellationToken cancellationToken)
    {
        var suppliers = await _dbConnection.GetSupplierIdsWithCurrencyTypeIdByBySupplierIdsAsync(ids, cancellationToken);

        return suppliers;
    }
}
