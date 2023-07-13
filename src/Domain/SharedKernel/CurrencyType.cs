using Domain.Errors;
using Domain.SharedKernel.Base;

namespace Domain.SharedKernel;

public class CurrencyType : Enumeration<CurrencyType>
{
    public static readonly CurrencyType USD = new(1, nameof(USD));
    public static readonly CurrencyType SGD = new(2, nameof(SGD));
    public static readonly CurrencyType VND = new(3, nameof(VND));

    //EF Proxies requires
    protected CurrencyType() { }

    private CurrencyType(byte id, string name) : base(id, name) { }

    public new static Result<CurrencyType> FromId(byte id)
    {
        var result = Enumeration<CurrencyType>.FromId(id);
        if (result.IsFailure)
            return DomainErrors.CurrencyType.NotFoundId(id);

        return result;
    }
}