namespace Benchmark.CastingObject;


public abstract class BaseClass
{
    public Guid Id { get; set; }
}
public class MyClass : BaseClass
{
    public string Name { get; set; }
}