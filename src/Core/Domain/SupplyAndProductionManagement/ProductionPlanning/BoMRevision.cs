using Domain.SharedKernel.Base;

namespace Domain.ProductionPlanning;

public class BoMRevision : Entity<BoMRevisionId>
{
    private readonly List<BoMRevisionMaterial> _boMRevisionMaterials = new();
    
    // use BoMRevisionCode to make sure that the client code should pass BoMCode to create BoMRevisionCode
    // along with BoMId
    public BoMRevisionCode Revision { get; }
    public string Confirmation { get; private set; }
    
    public BoMId BoMId { get; private set; }
    
    public virtual IReadOnlyCollection<BoMRevisionMaterial> BoMRevisionMaterials => _boMRevisionMaterials.AsReadOnly();

    //required EF
    protected BoMRevision() {}
    
    private BoMRevision(string confirmation)
    {
        Confirmation = confirmation;
    }

    public Result<BoMRevision> Create(string confirmation)
    {
        if (string.IsNullOrEmpty(confirmation) || string.IsNullOrWhiteSpace(confirmation))
            return DomainErrors.BoMRevision.EmptyConfirmation;

        return new BoMRevision(confirmation);
    }

    public Result UpdateBoMId(BoMId bomId)
    {
        if (bomId.Value == 0)
            return DomainErrors.BoM.InvalidId(bomId.Value);

        BoMId = bomId;

        return Result.Success();
    }

    public void ReviseMaterials(BoMRevisionMaterial boMRevisionMaterial)
    {
        _boMRevisionMaterials.Add(boMRevisionMaterial);
    }
}