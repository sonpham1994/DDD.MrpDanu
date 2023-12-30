using Domain.SharedKernel.Base;

namespace Domain.SupplyChainManagement.MaterialAggregate;

public class RegionalMarket : Enumeration<RegionalMarket>
{
    public static readonly RegionalMarket None = new(1, nameof(None), nameof(None));
    public static readonly RegionalMarket NewYork = new(2, nameof(NewYork), "New York city");
    public static readonly RegionalMarket CN = new(3, nameof(CN), "China");
    public static readonly RegionalMarket NA = new(4, nameof(NA), "North America");
    public static readonly RegionalMarket EU = new(5, nameof(EU), "Europe");
    public static readonly RegionalMarket KR = new(6, nameof(KR), "Korean");
    public static readonly RegionalMarket UK = new(7, nameof(UK), "the United Kingdom");
    public static readonly RegionalMarket Florida = new(8, nameof(Florida), "Florida");

    public string Code { get; }
    
    protected RegionalMarket() {}
    
    private RegionalMarket(in byte id, string code, string name) : base(id, name) 
    { 
        Code = code;
    }

    public new static Result<RegionalMarket> FromId(in byte id)
    {
        var result = Enumeration<RegionalMarket>.FromId(id);

        if (result.IsFailure)
            return DomainErrors.RegionalMarket.NotFoundId(id);

        return result;
    }
}
