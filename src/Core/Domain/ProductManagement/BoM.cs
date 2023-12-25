using Domain.SharedKernel.Base;

namespace Domain.ProductManagement;

public class BoM : AggregateRoot<BoMId>
{
    private readonly List<BoMRevision> _boMRevisions = new();

    // use BoMCode to make sure that, the client code don't pass any invalid string to BoMRevision, and it has some
    // business to create a new BoMCode
    //from UI on original MRP Danu, this is what the system call Revision, not Code. Hence we need to follow this
    //ubiquitous language
    public BoMCode Revision { get; private set; }
    
    public ProductId? ProductId { get; private set; }
    //public uint ProductId { get; private set; }
    //public virtual Product Product { get; private set; }
    
    public virtual IReadOnlyCollection<BoMRevision> BoMRevisions => _boMRevisions.AsReadOnly();
    
    //required EF
    protected BoM() {}
    
    public BoM(ProductId? productId = null)
    {
        ProductId = productId;
    }

    public Result IncreaseRevision()
    {
        Revision = BoMCode.Create(Id).Value;

        return Result.Success();
    }

    public Result ReviseBoM(BoMRevision boMRevision)
    {
        _boMRevisions.Add(boMRevision);

        return Result.Success();
    }
}
