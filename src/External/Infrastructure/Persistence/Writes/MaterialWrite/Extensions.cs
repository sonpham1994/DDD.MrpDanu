using Domain.MaterialManagement.MaterialAggregate.Services.UniqueMaterialCodeServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Writes.MaterialWrite;

internal static class Extensions
{
    public static async Task<List<MaterialIdWithCode>> GetByCodeAsync(this IDbConnection dbConnection, string code, CancellationToken cancellationToken)
    {
        var material = await dbConnection
            .QueryAsync<MaterialIdWithCode>(
                @"SELECT material.Id, material.Code
                    FROM Material
                    WHERE Code = @Code;",
                new { Code = code });

        return material.ToList();
    }
}
