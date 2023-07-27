namespace Domain.SharedKernel.Base;

public readonly struct DomainError : IEquatable<DomainError>
{
    internal static DomainError Empty => new(string.Empty, string.Empty);

    public string Code { get; }
    public string Message { get; }

    public DomainError(string code, string message)
    {
        Code = code;
        Message = message;
    }

    public bool IsEmpty()
    {
        return this == Empty || Code is null;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;
        
        if (obj is DomainError error)
        {
            return Equals(error);
        }

        return false;
    }

    public bool Equals(DomainError other)
    {
        return Code == other.Code;
    }

    public static bool operator ==(DomainError a, DomainError b) => a.Equals(b);
    public static bool operator !=(DomainError a, DomainError b) => !a.Equals(b);

    public override int GetHashCode()
    {
        return (this.GetType().Name.GetHashCode() + Code.GetHashCode()).GetHashCode();
    }
}
