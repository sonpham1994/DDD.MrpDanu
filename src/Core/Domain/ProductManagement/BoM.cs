using Domain.SharedKernel.Base;

namespace Domain.ProductManagement;

public class BoM : AggregateRoot<BoMId>
{
    private readonly List<BoMRevision> _boMRevisions = new();

    public string Code { get; }
    
    public ProductId? ProductId { get; private set; }
    //public uint ProductId { get; private set; }
    //public virtual Product Product { get; private set; }
    
    public virtual IReadOnlyCollection<BoMRevision> BoMRevisions => _boMRevisions.AsReadOnly();
    
    //required EF
    //protected BoM() {}
    
    public BoM()
    {
        //Code = Id.Value.ToString("BOM000000#");
    }
}
