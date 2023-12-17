using Domain.SharedKernel.Base;

namespace Domain.ProductManagement;

public class Product : AggregateRoot<ProductId>
{
    public string Code { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    
    public BoMId? BoMId { get; private set; }
    //public uint? BoMId { get; private set; }
    //public virtual BoM BoM { get; private set; }

    //required EF
    protected Product(){}
    
    public Product(string code, string name, string description)
    {
        Code = code;
        Name = name;
        Description = description;
    }

    // //we need to use ubiquitous language for this method like ImportBoM?
    // public void ReviseBoM(BoM bom)
    // {
    //     BoM = bom;
    // }
}
