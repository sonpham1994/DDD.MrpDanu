namespace SharedKernel.Enumerations;

public class CurrencyType : Enumeration<CurrencyType>
{
    public static readonly CurrencyType USD = new(1, nameof(USD));
    public static readonly CurrencyType SGD = new(2, nameof(SGD));
    public static readonly CurrencyType VND = new(3, nameof(VND));

    //EF Proxies requires
    protected CurrencyType() { }

    private CurrencyType(in byte id, string name) : base(id, name) { }

    public new static Result<CurrencyType?> FromId(in byte id)
    {
        var result = Enumeration<CurrencyType>.FromId(id);
        if (!result.IsSuccess)
            return DomainErrors.CurrencyType.NotFoundId(id);

        return result;
    }
}

public class CurrencyType2 : Enumeration<CurrencyType2>
{
    public static readonly CurrencyType2 USD = new(1, nameof(USD));
    public static readonly CurrencyType2 SGD = new(2, nameof(SGD));
    public static readonly CurrencyType2 VND = new(3, nameof(VND));

    //EF Proxies requires
    protected CurrencyType2() { }

    private CurrencyType2(in byte id, string name) : base(id, name) { }

    public new static Result<CurrencyType?> FromId(in byte id)
    {
        var result = Enumeration<CurrencyType>.FromId(id);
        if (!result.IsSuccess)
            return DomainErrors.CurrencyType.NotFoundId(id);

        return result;
    }
}