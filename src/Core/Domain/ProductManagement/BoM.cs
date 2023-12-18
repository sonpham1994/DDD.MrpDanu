using Domain.SharedKernel.Base;

namespace Domain.ProductManagement;

public class BoM : AggregateRoot<BoMId>
{
    private readonly List<BoMRevision> _boMRevisions = new();

    // use BoMCode to make sure that, the client code don't pass any invalid string to BoMRevision, and it has some
    // business to create a new BoMCode
    public BoMCode Code { get; }
    
    public ProductId? ProductId { get; private set; }
    //public uint ProductId { get; private set; }
    //public virtual Product Product { get; private set; }
    
    public virtual IReadOnlyCollection<BoMRevision> BoMRevisions => _boMRevisions.AsReadOnly();
    
    //required EF
    //protected BoM() {}
    
    // public BoM(string confirmation, Guid materialId, Guid supplierId, ProductId? productId = null)
    // {
    //     ProductId = productId;
    // }
}
