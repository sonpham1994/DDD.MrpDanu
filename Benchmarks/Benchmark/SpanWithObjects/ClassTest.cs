namespace Benchmark.SpanWithObjects;

public class ClassTest
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class ClassTestForSpan : IEquatable<ClassTestForSpan>
{
    public int Id { get; set; }
    public string Name { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is not ClassTestForSpan value)
            return false;

        return Equals(value);
    }
    
    public bool Equals(ClassTestForSpan value)
    {
        if (ReferenceEquals(this, value))
            return true;
        if (Id == value.Id)
            return true;

        return false;
    }
}