using Domain.SharedKernel.Base;
using Domain.SupplyAndProductionManagement.SupplyChainManagement;

namespace Domain.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate;

public class Country : Enumeration<Country>
{
    public static readonly Country VietNam = new(1, "VN", "Vietnam");
    public static readonly Country US = new(2, "US", "the United States");
    public static readonly Country UK = new(3, "UK", "the United Kingdom");
    public static readonly Country China = new(4, "CN", "China");
    public static readonly Country Korean = new(5, "KR", "Korean");
    public static readonly Country Malaysia = new(6, "MY", "Malaysia");

    public string Code { get; }

    private Country(in byte id, string code, string name) : base(id, name)
    {
        Code = code;
    }

    protected Country() { }

    public new static Result<Country> FromId(in byte id)
    {
        var result = Enumeration<Country>.FromId(id);
        if (result.IsFailure)
            return DomainErrors.Country.NotFoundId(id);

        return result;
    }
}