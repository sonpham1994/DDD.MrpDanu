using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedKernel.Tests;

public sealed class ValueObjectTests
{
    [Fact]
    public void two_different_valueobject_types_should_not_be_equal()
    {
        var valueObject1 = new ValueObjectTest1 { Code = "Code1", Name = "Name2"};
        var valueObject2 = new ValueObjectTest2 { Code = "Code1", Name = "Name2" };

        var result = valueObject1.Equals(valueObject2);

        result.ShouldBeFalse();
    }

    [Fact]
    public void two_different_valueobject_types_should_be_different_hashcode()
    {
        var valueObject1 = new ValueObjectTest1 { Code = "Code1", Name = "Name2" };
        var valueObject2 = new ValueObjectTest2 { Code = "Code1", Name = "Name2" };

        var result = valueObject1.GetHashCode() == valueObject2.GetHashCode();

        result.ShouldBeFalse();
    }

    [Fact]
    public void the_same_valueobject_type_should_be_equal()
    {
        var valueObject1 = new ValueObjectTest1 { Code = "Code1", Name = "Name2" };
        var valueObject2 = new ValueObjectTest1 { Code = "Code1", Name = "Name2" };

        var result1 = valueObject1.Equals(valueObject2);
        var result2 = valueObject1 == valueObject2;

        result1.ShouldBeTrue();
        result2.ShouldBeTrue();
    }

    [Fact]
    public void the_same_valueobject_should_return_the_same_hashcode()
    {
        var valueObject1 = new ValueObjectTest1 { Code = "Code1", Name = "Name2" };
        var valueObject2 = new ValueObjectTest1 { Code = "Code1", Name = "Name2" };

        var result = valueObject1.GetHashCode() == valueObject2.GetHashCode();

        result.ShouldBeTrue();
    }

    [Fact]
    public void the_different_valueobject_should_return_different_hashcode()
    {
        var valueObject1 = new ValueObjectTest1 { Code = "Code1", Name = "Name2" };
        var valueObject2 = new ValueObjectTest1 { Code = "Code2", Name = "Name3" };

        var result = valueObject1.GetHashCode() != valueObject2.GetHashCode();

        result.ShouldBeTrue();
    }
}

file class ValueObjectTest1 : ValueObject<ValueObjectTest1>
{
    public string Code { get; set; }
    public string Name { get; set; }

    protected override bool EqualComponents(ValueObjectTest1 valueObject)
    {
        return Code == valueObject.Code && Name == valueObject.Name;
    }

    protected override IEnumerable<int> GetHashCodeComponents()
    {
        yield return Code.GetHashCode();
        yield return Name.GetHashCode();
    }
}

file class ValueObjectTest2 : ValueObject<ValueObjectTest2>
{
    public string Code { get; set; }
    public string Name { get; set; }

    protected override bool EqualComponents(ValueObjectTest2 valueObject)
    {
        return Code == valueObject.Code && Name == valueObject.Name;
    }

    protected override IEnumerable<int> GetHashCodeComponents()
    {
        yield return Code.GetHashCode();
        yield return Name.GetHashCode();
    }
}