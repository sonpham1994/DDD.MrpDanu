namespace Benchmark.PassStructAsObjectParameter;

public class PersonClass
{
    public string Name { get; }
    public int Age { get; }

    public PersonClass(string name, int age)
    {
        Name = name;
        Age = age;
    }
}