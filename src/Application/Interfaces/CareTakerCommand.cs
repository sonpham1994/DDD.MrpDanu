namespace Application.Interfaces;

public interface IOriginatorCommand
{
    Task SetState(BaseMemento obj);
    Task RollBack();
}