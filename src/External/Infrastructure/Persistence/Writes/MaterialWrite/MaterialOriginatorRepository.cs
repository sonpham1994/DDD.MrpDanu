using Application.Interfaces;
using Application.Interfaces.Writes.MaterialWrite;
using Domain.Extensions;
using Domain.SharedKernel.Base;
using Domain.SharedKernel.ValueObjects;
using Domain.SupplyChainManagement.MaterialAggregate;

namespace Infrastructure.Persistence.Writes.MaterialWrite;

// if you use Lazy loading, this would be a drawback for Memento, because memento need to load all data for rolling back
// we can use Lazy loading if we don't apply only using navigation property, we would lose the foreign key here,
// if we use foreign key and then just store foreign key in spite of navigation property, it would be fine.
internal sealed class MaterialOriginatorRepository : IMaterialRepository, IOriginator
{
    private enum State
    {
        None,
        Add,
        Update,
        Delete
    }
    private KeyValuePair<State, MaterialMemento> _memento;
    private IMaterialRepository _materialRepository;
    private IUnitOfWork _unitOfWork;
    
    public MaterialOriginatorRepository(IMaterialRepository materialRepository, IUnitOfWork unitOfWork)
    {
        _materialRepository = materialRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Material?> GetByIdAsync(MaterialId id, CancellationToken cancellationToken = default)
    {
        var material = await _materialRepository.GetByIdAsync(id, cancellationToken);
        if (material is not null)
        {
            _memento = new KeyValuePair<State, MaterialMemento>(State.Update, MaterialMemento.ReturnCopy(material));
        }

        return material;
    }

    public void Save(Material material)
    {
        _materialRepository.Save(material);

        //create set point (previous state)
        if (material.IsTransient())
        {
            _memento = new KeyValuePair<State, MaterialMemento>(State.Add, MaterialMemento.ReturnCopy(material));
        }
        
    }

    public async Task DeleteAsync(MaterialId id, CancellationToken cancellationToken)
    {
        var material = await _materialRepository.GetByIdAsync(id, cancellationToken);
        _memento = new KeyValuePair<State, MaterialMemento>(State.Delete, MaterialMemento.ReturnCopy(material));
        await _materialRepository.DeleteAsync(id, cancellationToken);
    }

    public async Task RestoreAsync()
    {
        if (_memento.Key == State.None)
            return;
        
        if (_memento.Key == State.Add)
        {
            await _materialRepository.DeleteAsync(_memento.Value.Id, default);
        }
        else if (_memento.Key == State.Delete)
        {
            _materialRepository.Save(_memento.Value);
        }
        else
        {
            var material = await _materialRepository.GetByIdAsync(_memento.Value.Id);
            material!.UpdateMaterial(_memento.Value.Code, _memento.Value.Name, _memento.Value.Attributes,
                _memento.Value.MaterialType, _memento.Value.RegionalMarket, Result.Success());
            material.UpdateCost(_memento.Value.MaterialSupplierCosts);
        }

        await _unitOfWork.SaveChangesAsync(default);
    }
}

public class MaterialMemento : Material
{
    public static MaterialMemento ReturnCopy(Material material)
    {
        if (material is null)
            return null;
        
        var memento = (MaterialMemento)material;
        return memento.ReturnCopy();
    }
    
    private MaterialMemento ReturnCopy()
    {
        return (MaterialMemento)this.MemberwiseClone();
    }
}

public interface IOriginator
{
    Task RestoreAsync();
}