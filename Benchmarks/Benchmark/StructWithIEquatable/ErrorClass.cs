namespace Benchmark.StructWithIEquatable;

public class ErrorClass
{
    public string Code { get; }
    public string Message { get; }

    public ErrorClass(string code, string message)
    {
        Code = code;
        Message = message;
    }


    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;

        if (ReferenceEquals(this, obj))
            return true;
        
        if (obj is ErrorClass error)
        {
            return Equals(error);
        }

        return false;
    }

    public bool Equals(ErrorClass obj)
    {
        if (obj is null)
            return false;

        return Code == obj.Code;
    }

    public override int GetHashCode()
    {
        return Code.GetHashCode();
    }

    public static bool operator ==(ErrorClass a, ErrorClass b) => a.Equals(b);
    public static bool operator !=(ErrorClass a, ErrorClass b) => !a.Equals(b);
}