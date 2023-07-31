namespace Domain.SharedKernel.Base;

/*
 * Good design for struct:
 *  ✔️ CONSIDER defining a struct instead of a class if instances of the type are small and commonly short-lived or are commonly embedded in other objects.
 *  ❌ AVOID defining a struct unless the type has all of the following characteristics:
 *      - It logically represents a single value, similar to primitive types (int, double, etc.).
 *      - It has an instance size under 16 bytes.
 *      - It is immutable.
 *      - It will not have to be boxed frequently.
 *
 * https://learn.microsoft.com/en-us/dotnet/standard/design-guidelines/struct?redirectedfrom=MSDN
 * https://learn.microsoft.com/en-us/dotnet/standard/design-guidelines/choosing-between-class-and-struct?redirectedfrom=MSDN
 * https://stackoverflow.com/questions/34353330/c-sharp-struct-vs-class-performace-design-focus
 */
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
