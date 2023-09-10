using Application.Behaviors.TransactionalBehaviours.Factories;
using Application.Interfaces.Repositories;
using Domain.SharedKernel.Base;
using MediatR;

namespace Application.MaterialManagement.MaterialAggregate.Commands.CreateMaterial;

public class CreateMaterialCommandCareTaker<TRequest> : CareTakerCommand
{
    public CreateMaterialCommandCareTaker(TRequest obj)
    {
        
    }
    // private readonly IUndoRepository _undoRepository;
    //
    // public CreateMaterialCommandCareTaker(IUndoRepository undoRepository)
    // {
    //     _undoRepository = undoRepository;
    // }

    public async Task SetState()
    {
        
    }
    
    public override Task RollBack(object obj)
    {
        return Task.CompletedTask;
    }
}

