namespace Domain.SharedKernel.Base;

public sealed class DomainError : ValueObject
{
    public string Code { get; }
    public string Message { get; }

    public DomainError(string code, string message)
    {
        Code = code;
        Message = message;
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Code;
    }
}
