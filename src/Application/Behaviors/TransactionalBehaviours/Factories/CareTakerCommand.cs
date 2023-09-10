using Application.MaterialManagement.MaterialAggregate.Commands.CreateMaterial;

namespace Application.Behaviors.TransactionalBehaviours.Factories;

public abstract class CareTakerCommand
{
    public static CareTakerCommand GetRollBackCommand<TRequest>(TRequest obj)
    {
        if (obj is CreateMaterialCommand)
            return new CreateMaterialCommandCareTaker<TRequest>(obj);

        return null;
    }
    public abstract Task RollBack(object obj);
}