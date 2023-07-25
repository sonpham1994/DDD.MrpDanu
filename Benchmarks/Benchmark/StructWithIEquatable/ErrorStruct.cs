using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmark.StructWithIEquatable;

public readonly struct ErrorStruct
{
    public string Code { get; }
    public string Message { get; }

    public ErrorStruct(string code, string message)
    {
        Code = code;
        Message = message;
    }


    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;

        if (obj is ErrorStruct error)
        {
            return Equals(error);
        }

        return false;
    }

    public bool Equals(ErrorStruct? obj)
    {
        if (obj is null)
            return false;

        return Code == obj.Value.Code;
    }

    public override int GetHashCode()
    {
        return Code.GetHashCode();
    }

    public static bool operator ==(ErrorStruct a, ErrorStruct b) => a.Equals(b);
    public static bool operator !=(ErrorStruct a, ErrorStruct b) => !a.Equals(b);

}

public readonly struct ErrorStructWithIEquatable : IEquatable<ErrorStructWithIEquatable>
{
    public string Code { get; }
    public string Message { get; }

    public ErrorStructWithIEquatable(string code, string message)
    {
        Code = code;
        Message = message;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;

        if (obj is ErrorStructWithIEquatable error)
        {
            return Equals(error);
        }

        return false;
    }

    public bool Equals(ErrorStructWithIEquatable obj)
    {
        return Code == obj.Code;
    }

    public override int GetHashCode()
    {
        return Code.GetHashCode();
    }

    public static bool operator ==(ErrorStructWithIEquatable a, ErrorStructWithIEquatable b) => a.Equals(b);
    public static bool operator !=(ErrorStructWithIEquatable a, ErrorStructWithIEquatable b) => !a.Equals(b);

}
