using Application.Interfaces.Repositories;
using Dapper;
using Domain.MaterialManagement.MaterialAggregate;
using System.Data;
using Infrastructure.Persistence.Write.Mementos.MementoModels;

namespace Infrastructure.Persistence.Write.Mementos.Originators;

internal sealed class MaterialWithUndoRepository : IMaterialRepository, IUndoRepository
{
    private readonly IMaterialRepository _materialRepository;
    private readonly IDbConnection _dbConnection;
    private MaterialMemento? _materialMemento;

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

    public async Task RestoreAsync(Guid id, string tableName)
    {
        await _dbConnection.ExecuteAsync(@$"
                DELETE {tableName}
                WHERE Id = @Id
                ",
            new { Id = id });
    }
    
    public async Task RestoreAsync(CancellationToken cancellationToken = default)
    {
        //We don't use getting material by repository and call material.Update(...) to update the previous state and then persist
        //it through EF Core because it will introduce a new DomainEvent or messageBus. So the best way to get rid of that is
        //using Dapper or Sql Raw to restore material.

        if (_materialMemento is null)
            return;
        
        bool exists = await _dbConnection.QueryFirstAsync<bool>(@"
                    SELECT 1
                    FROM Material
                    WHERE Id = @Id
                    union select 0",
                    new
                    {
                        Id = _materialMemento.Id
                    });
        var updateMaterialParam = new
        {
            _materialMemento.Id,
            _materialMemento.Code,
            _materialMemento.Name,
            _materialMemento.ColorCode,
            _materialMemento.CodeUnique,
            _materialMemento.Width,
            _materialMemento.Unit,
            _materialMemento.Varian,
            _materialMemento.Weight,
            _materialMemento.RegionalMarketId,
            _materialMemento.MaterialTypeId
        };
        
        if (exists)
        {
            await _dbConnection.ExecuteAsync("""
                UPDATE Material
                SET Code = @Code, Name = @Name, ColorCode = @ColorCode, CodeUnique = @CodeUnique, Width = @Width, 
                Unit = @Unit, Varian = @Varian, Weight = @Weight, RegionalMarketId = @RegionalMarketId, MaterialTypeId = @MaterialTypeId
                WHERE Id = @Id
                """,
                updateMaterialParam);
        }
        else
        {
            await _dbConnection.ExecuteAsync(@"
                INSERT INTO 
                Material (Id, Code, Name, ColorCode, CodeUnique, Width, Unit, Varian, Weight, RegionalMarketId, MaterialTypeId)
                VALUES (@Id, @Code, @Name, @ColorCode, @CodeUnique, @Width, @Unit, @Varian, @Weight, @RegionalMarketId, @MaterialTypeId)
                ",
                updateMaterialParam);
        }

        await _dbConnection.ExecuteAsync("""
                DELETE MaterialCostManagement
                WHERE MaterialId = @MaterialId
                """,
                new { MaterialId = _materialMemento.Id });

        if (_materialMemento.MaterialCostManagements.Count > 0)
        {
            var insertMaterialCostCommand = @"
                    INSERT INTO 
                    MaterialCostManagement (Id, MaterialId, TransactionalPartnerId, CurrencyTypeId, Surcharge, MinQuantity, Price)
                    VALUES(@Id, @MaterialId, @TransactionalPartnerId, @CurrencyTypeId, @Surcharge, @MinQuantity, @Price)
                    ";

            var insertMaterialCostCommandParams = _materialMemento
                .MaterialCostManagements
                .Select(x => new
                {
                    x.Id,
                    x.MaterialId,
                    x.TransactionalPartnerId,
                    x.CurrencyTypeId,
                    x.Surcharge,
                    x.MinQuantity,
                    x.Price,
                })
                .ToList();
            
            await _dbConnection.ExecuteAsync(insertMaterialCostCommand, insertMaterialCostCommandParams);
        }
    }
}


