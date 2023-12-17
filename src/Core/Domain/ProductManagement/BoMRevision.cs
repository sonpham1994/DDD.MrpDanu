using Domain.SharedKernel.Base;

namespace Domain.ProductManagement;

public class BoMRevision : Entity<BoMRevisionId>
{
    private readonly List<BoMRevisionMaterial> _boMRevisionMaterials = new();
    
    public string Code { get; }
    public string Confirmation { get; private set; }
    
    public BoMId BoMId { get; private set; }
    
    public virtual IReadOnlyCollection<BoMRevisionMaterial> BoMRevisionMaterials => _boMRevisionMaterials.AsReadOnly();

    //required EF
    protected BoMRevision() {}
    
    public BoMRevision(BoM bom, string confirmation)
    {
        Code = Id.Value.ToString($"{bom.Code}-00#");
        Confirmation = confirmation;
    }
}