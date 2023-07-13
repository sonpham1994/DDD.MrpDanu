using BenchmarkDotNet.Attributes;

namespace Benchmark.PassStructAsObjectParameter;

[MemoryDiagnoser()]
public class PassStructAsObjectParameterBenchmark
{
    [Params(1)]
    public int Length { get; set; }
    
    // [Benchmark]
    // public void CreateStructAsInterfaceWithAssigningValueType()
    // {
    //     for (int i = 0; i < Length; i++)
    //     {
    //         IPerson person = new Person("Son", 30);
    //         int age = person.Age;
    //     }
    // }
    //
    // [Benchmark]
    // public void CreateStructAsStructWithAssigningValueType()
    // {
    //     for (int i = 0; i < Length; i++)
    //     {
    //         Person person = new Person("Son", 30);
    //         int age = person.Age;
    //     }
    // }
    //
    // [Benchmark]
    // public void CreateStructAsInterfaceWithAssigningReferenceType()
    // {
    //     for (int i = 0; i < Length; i++)
    //     {
    //         IPerson person = new Person("Son", 30);
    //         string name = person.Name;
    //     }
    // }
    //
    // [Benchmark]
    // public void CreateStructAsStructWithAssigningReferenceType()
    // {
    //     for (int i = 0; i < Length; i++)
    //     {
    //         Person person = new Person("Son", 30);
    //         string name = person.Name;
    //     }
    // }
    //
    // [Benchmark]
    // public void CreateStructAsStructWithAssigningValueAndReferenceType()
    // {
    //     for (int i = 0; i < Length; i++)
    //     {
    //         Person person = new Person("Son", 30);
    //         int age = person.Age;
    //         string name = person.Name;
    //     }
    // }
    //
    // [Benchmark]
    // public void CreateStructAsInterfaceWithAssigningValueAndReferenceType()
    // {
    //     for (int i = 0; i < Length; i++)
    //     {
    //         IPerson person = new Person("Son", 30);
    //         int age = person.Age;
    //         string name = person.Name;
    //     }
    // }
    //
    // [Benchmark]
    // public void PassStructAsStructAndDoNothing()
    // {
    //     for (int i = 0; i < Length; i++)
    //     {
    //         var person = new Person("Son", 30);
    //         DoNothingWithStruct(person);
    //     }
    //     
    // }
    //
    // [Benchmark]
    // public void PassClassAsClassAndDoNothing()
    // {
    //     for (int i = 0; i < Length; i++)
    //     {
    //         var person = new PersonClass("Son", 30);
    //         DoNothingWithClass(person);
    //     }
    //     
    // }
    //
    // [Benchmark]
    // public void PassStructAsObjectAndDoNothing()
    // {
    //     for (int i = 0; i < Length; i++)
    //     {
    //         var person = new Person("Son", 30);
    //         DoNothingWithObject(person);
    //     }
    // }
    //
    // [Benchmark]
    // public void PassStructAsObjectAndAssigningAgeStructPerson()
    // {
    //     for (int i = 0; i < Length; i++)
    //     {
    //         var person = new Person("Son", 30);
    //         PassObjectAndAssigningAgeStructPerson(person);
    //     }
    // }
    //
    // [Benchmark]
    // public void PassStructAsObjectAndAssigningNameStructPerson()
    // {
    //     for (int i = 0; i < Length; i++)
    //     {
    //         var person = new Person("Son", 30);
    //         PassObjectAndAssigningNameStructPerson(person);
    //     }
    // }
    //
    // [Benchmark]
    // public void PassStructAsObjectAndAssigningAgeInterfaceStructPerson()
    // {
    //     for (int i = 0; i < Length; i++)
    //     {
    //         var person = new Person("Son", 30);
    //         PassObjectAndAssigningAgeInterfaceStructPerson(person);
    //     }
    // }
    //
    // [Benchmark]
    // public void PassStructAsObjectAndAssigningNameInterfaceStructPerson()
    // {
    //     for (int i = 0; i < Length; i++)
    //     {
    //         var person = new Person("Son", 30);
    //         PassObjectAndAssigningNameInterfaceStructPerson(person);
    //     }
    // }
    
    [Benchmark]
    public void PassStructAsGenericTypeAndAssigningNameAndAgeStructPerson()
    {
        for (int i = 0; i < Length; i++)
        {
            var person = new Person("Son", 30);
            PassGenericTypeAndAssigningAgeAndNameStructPerson(person);
        }
    }
    
    [Benchmark]
    public void PassStructAsGenericTypeAndAssigningNameAndAgeInterfaceStructPerson()
    {
        for (int i = 0; i < Length; i++)
        {
            var person = new Person("Son", 30);
            PassGenericTypeAndAssigningAgeAndNameInterfaceStructPerson(person);
        }
    }
    
    // [Benchmark]
    // public void PassClassAsObjectAndDoNothing()
    // {
    //     for (int i = 0; i < Length; i++)
    //     {
    //         var person = new PersonClass("Son", 30);
    //         DoNothingWithObject(person);
    //     }
    // }
    //
    // [Benchmark]
    // public void PassGuidAsObjectAndDoNothing()
    // {
    //     for (int i = 0; i < Length; i++)
    //     {
    //         var guid = Guid.NewGuid();
    //         DoNothingWithObject(guid);
    //     }
    // }
    //
    // [Benchmark]
    // public void PassGuidAsObjectAndAssigningGuid()
    // {
    //     for (int i = 0; i < Length; i++)
    //     {
    //         var guid = Guid.NewGuid();
    //         PassObjectAndAssigningGuid(guid);
    //     }
    // }
    //
    // [Benchmark]
    // public void PassIntAsObjectAndDoNothing()
    // {
    //     for (int i = 0; i < Length; i++)
    //     {
    //         DoNothingWithObject(i);
    //     }
    // }
    //
    // [Benchmark]
    // public void PassIntAsObjectAndAssigningInt()
    // {
    //     for (int i = 0; i < Length; i++)
    //     {
    //         PassObjectAndAssigningInt(i);
    //     }
    // }

    private void DoNothingWithStruct(Person person)
    {
    }
    
    private void DoNothingWithClass(PersonClass person)
    {
    }
    
    private void DoNothingWithObject(object person)
    {
    }
    
    private void PassObjectAndAssigningAgeStructPerson(object obj)
    {
        if (obj is Person person)
        {
            var a = person.Age;
        }
    }
    
    private void PassObjectAndAssigningNameStructPerson(object obj)
    {
        if (obj is Person person)
        {
            var a = person.Name;
        }
    }
    
    private void PassObjectAndAssigningAgeInterfaceStructPerson(object obj)
    {
        if (obj is IPerson person)
        {
            var a = person.Age;
        }
    }
    
    private void PassGenericTypeAndAssigningAgeAndNameInterfaceStructPerson<T>(T obj)
    {
        if (obj is IPerson person)
        {
            var a = person.Age;
            var b = person.Name;
        }
    }
    
    private void PassGenericTypeAndAssigningAgeAndNameStructPerson<T>(T obj)
    {
        if (obj is Person person)
        {
            var a = person.Age;
            var b = person.Name;
        }
    }
    
    private void PassObjectAndAssigningNameInterfaceStructPerson(object obj)
    {
        if (obj is IPerson person)
        {
            var a = person.Name;
        }
    }
    
    private void PassObjectAndAssigningGuid(object obj)
    {
        if (obj is Guid guid)
        {
            var a = guid;
        }
    }
    
    private void PassObjectAndAssigningInt(object obj)
    {
        if (obj is int i)
        {
            var a = i;
        }
    }
}