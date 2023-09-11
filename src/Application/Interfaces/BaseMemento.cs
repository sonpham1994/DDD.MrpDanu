namespace Application.Interfaces;

public abstract class BaseMemento
{
    public Guid Id { get; }
    public BaseMemento(Guid id) => Id = id;
}