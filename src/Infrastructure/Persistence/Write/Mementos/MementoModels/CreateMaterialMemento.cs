using Application.Interfaces;

namespace Infrastructure.Persistence.Write.Mementos.MementoModels;

public class CreateMaterialMemento : BaseMemento
{
    public CreateMaterialMemento(Guid id) : base(id)
    {
    }
}