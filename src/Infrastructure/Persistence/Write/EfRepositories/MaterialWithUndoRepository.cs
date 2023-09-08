using Application.Interfaces.Repositories;
using Dapper;
using Domain.MaterialManagement.MaterialAggregate;
using Infrastructure.Persistence.Write.Mementos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Write.EfRepositories;

internal sealed class MaterialWithUndoRepository : IMaterialRepository, IMaterialWithUndoRepository
{
    private readonly IMaterialRepository _materialRepository;
    private readonly IDbConnection _dbConnection;
    private MaterialMemento _materialMemento;

    public MaterialWithUndoRepository(IMaterialRepository materialRepository, IDbConnection dbConnection)
    {
        _materialRepository = materialRepository;
        _dbConnection = dbConnection;
    }

    public async Task<Material?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var material = await _materialRepository.GetByIdAsync(id, cancellationToken);

        if (material is not null)
            _materialMemento = new MaterialMemento(material);

        return material;
    }

    public void Save(Material material)
    {
        _materialRepository.Save(material);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var material = await _materialRepository.GetByIdAsync(id, cancellationToken);

        if (material is not null)
            _materialMemento = new MaterialMemento(material);

        await _materialRepository.DeleteAsync(id, cancellationToken);
    }

    public async Task RestoreMaterial(CancellationToken cancellationToken)
    {
        //We don't use getting material by repository and call material.Update(...) to update the previous state and then persist
        //it through EF Core because it will introduce a new DomainEvent or messageBus. So the best way to get rid of that is
        //using Dapper or Sql Raw to restore material.
        
        bool exists = await _dbConnection.QueryFirstAsync<bool>(@"
                    SELECT 1
                    FROM Material
                    WHERE Id = @Id
                    union select 0",
                    new
                    {
                        Id = _materialMemento.Id
                    });
        if (exists)
        {
            await _dbConnection.ExecuteAsync("""
                UPDATE Material
                SET Code = @Code, Name = @Name, ColorCode = @ColorCode, Width = @Width, Unit = @Unit, Varian = @Varian,
                RegionalMarketId = @RegionalMarketId, MaterialTypeId = @MaterialTypeId
                WHERE Id = @Id
                """,
                new
                {
                    Id = _materialMemento.Id,
                    Code = _materialMemento.Code,
                    Name = _materialMemento.Attributes.Name,
                    ColorCode = _materialMemento.Attributes.ColorCode,
                    Width = _materialMemento.Attributes.Width,
                    Unit = _materialMemento.Attributes.Unit,
                    Varian = _materialMemento.Attributes.Varian,
                    RegionalMarketId = _materialMemento.RegionalMarket.Id,
                    MaterialTypeId = _materialMemento.MaterialType.Id
                });
            await _dbConnection.ExecuteAsync("""
                DELETE MaterialCostManagement
                WHERE MaterialId = @MaterialId
                """,
                new { MaterialId = _materialMemento.Id });

            await _dbConnection.ExecuteAsync("""INSERT INTO""");
        }
    }
}

internal interface IMaterialWithUndoRepository
{
    Task RestoreMaterial(CancellationToken cancellationToken);
}
