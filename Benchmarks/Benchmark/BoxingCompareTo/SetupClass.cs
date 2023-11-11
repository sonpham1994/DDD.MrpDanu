namespace Benchmark.BoxingCompareTo;

public class MyClassGenericWithComparable<T> : IComparable
    where T : IComparable
{
    public T Id { get; set; }
    
    public int CompareTo(object value)
    {
        return Id.CompareTo(value);
    }
}

public class MyClassGenericWithComparableGeneric<T> : IComparable<T>
    where T : IComparable<T>
{
    public T Id { get; set; }
    
    public int CompareTo(T value)
    {
        return Id.CompareTo(value);
    }
}

public class MyClassGenericWithComparableInt : IComparable<int>
{
    public int Id { get; set; }
    
    public int CompareTo(int value)
    {
        return Id.CompareTo(value);
    }
}