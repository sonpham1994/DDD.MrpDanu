using Domain.SharedKernel.Base;

namespace Domain.ProductManagement;

public class BoMRevision : Entity<ushort>
{
    private readonly List<BoMRevisionMaterial> _boMRevisionMaterials = new();
    
    public string Code { get; }
    public string Confirmation { get; private set; }
    
    public virtual IReadOnlyCollection<BoMRevisionMaterial> BoMRevisionMaterials => _boMRevisionMaterials.AsReadOnly();

    //required EF
    protected BoMRevision() {}
    
    public BoMRevision(BoM bom, string confirmation)
    {
        Code = Id.ToString($"{bom.Code}-00#");
        Confirmation = confirmation;
    }
}