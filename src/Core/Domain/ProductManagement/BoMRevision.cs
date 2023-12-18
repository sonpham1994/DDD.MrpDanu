using Domain.SharedKernel.Base;

namespace Domain.ProductManagement;

public class BoMRevision : Entity<BoMRevisionId>
{
    private readonly List<BoMRevisionMaterial> _boMRevisionMaterials = new();
    
    // use BoMRevisionCode to make sure that the client code should pass BoMCode to create BoMRevisionCode
    // along with BoMId
    public BoMRevisionCode Code { get; }
    public string Confirmation { get; private set; }
    
    public BoMId BoMId { get; private set; }
    
    public virtual IReadOnlyCollection<BoMRevisionMaterial> BoMRevisionMaterials => _boMRevisionMaterials.AsReadOnly();

    //required EF
    protected BoMRevision() {}
    
    private BoMRevision(BoMId bomId, BoMCode bomCode, string confirmation)
    {
        //Code = Id.Value.ToString($"{bom.Code}-00#");
        Confirmation = confirmation;
    }
}