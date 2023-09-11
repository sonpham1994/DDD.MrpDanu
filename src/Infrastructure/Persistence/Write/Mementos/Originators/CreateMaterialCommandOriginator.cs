using Application.Interfaces;
using Infrastructure.Persistence.Write.Mementos.MementoModels;

namespace Infrastructure.Persistence.Write.Mementos.Originators;

public class CreateMaterialCommandOriginator : IOriginatorCommand
{
    private CreateMaterialMemento _memento;
    // private readonly IUndoRepository _undoRepository;
    //
    // public CreateMaterialCommandCareTaker(IUndoRepository undoRepository)
    // {
    //     _undoRepository = undoRepository;
    // }

    public async Task SetState(BaseMemento obj)
    {
        _memento = new(obj.Id);
    }
    
    public Task RollBack()
    {
        return Task.CompletedTask;
    }
}

