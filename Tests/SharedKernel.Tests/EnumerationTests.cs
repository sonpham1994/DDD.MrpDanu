using Shouldly;

namespace SharedKernel.Tests;

public sealed class EnumerationTests
{
    [Fact]
    public void two_different_enumeration_types_should_not_be_equal()
    {
        var enumerationTest1 = EnumerationTest1.Test1;
        var enumerationTest2 = EnumerationTest2.Test1;

        var result = enumerationTest1.Equals(enumerationTest2);
        
        result.ShouldBeFalse();
    }
    
    [Fact]
    public void two_different_enumeration_types_should_be_different_hashcode()
    {
        var enumerationTest1 = EnumerationTest1.Test1.GetHashCode();
        var enumerationTest2 = EnumerationTest2.Test1.GetHashCode();
        
        var result = enumerationTest1 == enumerationTest2;
        
        result.ShouldBeFalse();
    }
    
    [Fact]
    public void the_same_enumeration_type_should_be_equal()
    {
        var enumerationTest1_1 = EnumerationTest1.Test1;
        var enumerationTest1_2 = EnumerationTest1.Test1;

        var result1 = enumerationTest1_1.Equals(enumerationTest1_2);
        var result2 = enumerationTest1_1 == enumerationTest1_2;
        
        result1.ShouldBeTrue();
        result2.ShouldBeTrue();
    }
    
    [Fact]
    public void the_same_enumeration_should_return_the_same_hashcode()
    {
        var hashCodeEnumerationTest1_1 = EnumerationTest1.Test1.GetHashCode();
        var hashCodeEnumerationTest1_2 = EnumerationTest1.Test1.GetHashCode();

        var result = hashCodeEnumerationTest1_1 == hashCodeEnumerationTest1_2;
        
        result.ShouldBeTrue();
    }
    
    [Fact]
    public void the_different_enumeration_should_return_different_hashcode()
    {
        var hashCodeEnumerationTest1_1 = EnumerationTest1.Test1.GetHashCode();
        var hashCodeEnumerationTest1_2 = EnumerationTest1.Test2.GetHashCode();

        var result = hashCodeEnumerationTest1_1 != hashCodeEnumerationTest1_2;
        
        result.ShouldBeTrue();
    }
    
    [Fact]
    public void fromId_method_should_return_exact_data()
    {
       byte id = 2;
       var enumeration = EnumerationTest1.FromId(id);

       enumeration.Value!.Id.ShouldBe(id);
    }
    
    [Fact]
    public void fromId_method_should_return_error_when_id_not_exist()
    {
        byte id = 0;
        var enumeration = EnumerationTest1.FromId(id);

        enumeration.Error.ShouldBe(EnumerationTestError.NotFoundId);
    }
}

file static class EnumerationTestError
{
    public static DomainError NotFoundId = new("Not found id", "Not found id");
}

file class EnumerationTest1 : Enumeration<EnumerationTest1>
{
    public static readonly EnumerationTest1 Test1 = new(1, nameof(Test1));
    public static readonly EnumerationTest1 Test3 = new(3, nameof(Test3));
    public static readonly EnumerationTest1 Test2 = new(2, nameof(Test2));

    private EnumerationTest1(in byte id, string name) : base(id, name) { }

    public new static Result<EnumerationTest1?> FromId(in byte id)
    {
        var result = Enumeration<EnumerationTest1>.FromId(id);
        if (!result.IsSuccess)
            return EnumerationTestError.NotFoundId;

        return result;
    }
}

file class EnumerationTest2 : Enumeration<EnumerationTest2>
{
    public static readonly EnumerationTest2 Test1 = new(1, nameof(Test1));

    private EnumerationTest2(in byte id, string name) : base(id, name) { }

    public new static Result<EnumerationTest2?> FromId(in byte id)
    {
        var result = Enumeration<EnumerationTest2>.FromId(id);
        if (!result.IsSuccess)
            return EnumerationTestError.NotFoundId;

        return result;
    }
}