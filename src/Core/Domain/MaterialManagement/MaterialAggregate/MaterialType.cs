using Domain.SharedKernel.Base;

namespace Domain.MaterialManagement.MaterialAggregate;

public class MaterialType : Enumeration<MaterialType>
{
    public static readonly MaterialType Material = new(1, nameof(Material));
    public static readonly MaterialType Subassemblies = new(2, nameof(Subassemblies));

    protected MaterialType() {}
        
    private MaterialType(in byte id, string name) : base(id, name)
    {
    }

    public new static Result<MaterialType> FromId(in byte id)
    {
        var result = Enumeration<MaterialType>.FromId(id);
        if (result.IsFailure)
            return DomainErrors.MaterialType.NotFoundId(id);

        return result;
    }
}
