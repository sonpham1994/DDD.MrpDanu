using Application.MaterialManagement.MaterialAggregate.Commands.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Writes.TransactionalPartnerWrite;

public interface ITransactionalPartnerQueryForWrite
{
    Task<IReadOnlyList<SupplierIdWithCurrencyTypeId>> GetSupplierIdsWithCurrencyTypeIdBySupplierIdsAsync(IReadOnlyList<Guid> ids, CancellationToken cancellationToken);
}
