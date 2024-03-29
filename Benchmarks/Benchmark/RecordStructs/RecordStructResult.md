| Method                          | Length | Mean     | Error    | StdDev   | Allocated |
|-------------------------------- |------- |---------:|---------:|---------:|----------:|
| CompareEqualsRecordStruct       | 1      | 890.7 ns | 14.28 ns | 11.15 ns |         - |
| CompareEqualsEqualsRecordStruct | 1      | 881.7 ns |  1.84 ns |  1.44 ns |         - |
| CompareEqualsRecordStruct       | 10     | 885.0 ns |  2.38 ns |  1.86 ns |         - |
| CompareEqualsEqualsRecordStruct | 10     | 885.7 ns |  0.77 ns |  0.65 ns |         - |
| CompareEqualsRecordStruct       | 100    | 926.0 ns |  0.73 ns |  0.65 ns |         - |
| CompareEqualsEqualsRecordStruct | 100    | 927.9 ns |  0.63 ns |  0.56 ns |         - |

the reason why this comparison is not boxing, check here: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-10.0/record-structs
```C#
record struct R1(T1 P1, T2 P2);
```
this will generate like this:
```C#
struct R1 : IEquatable<R1>
{
    public T1 P1 { get; set; }
    public T2 P2 { get; set; }
    public override bool Equals(object? obj) => obj is R1 temp && Equals(temp);
    public bool Equals(R1 other)
    {
        return
            EqualityComparer<T1>.Default.Equals(P1, other.P1) &&
            EqualityComparer<T2>.Default.Equals(P2, other.P2);
    }
    public static bool operator==(R1 r1, R1 r2)
        => r1.Equals(r2);
    public static bool operator!=(R1 r1, R1 r2)
        => !(r1 == r2);    
    public override int GetHashCode()
    {
        return Combine(
            EqualityComparer<T1>.Default.GetHashCode(P1),
            EqualityComparer<T2>.Default.GetHashCode(P2));
    }
}
```