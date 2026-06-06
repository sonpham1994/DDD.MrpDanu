using Shouldly;

namespace SharedKernel.Tests;

public class DomainErrorTests
{
    [Fact]
    public void two_different_domain_error_should_not_be_equal()
    {
        var domainError1 = new DomainError("Test", "Test");
        var domainError2 = new DomainError("Test2", "Test2");

        var result = domainError1.Equals(domainError2);
        var result2 = domainError1 == domainError2;
        
        result.ShouldBeFalse();
        result2.ShouldBeFalse();
    }
    
    [Fact]
    public void the_same_domain_error_should_be_equal()
    {
        var domainError1 = new DomainError("Test", "Test");
        var domainError2 = new DomainError("Test", "Test");

        var result1 = domainError1.Equals(domainError2);
        var result2 = domainError1 == domainError2;
        
        result1.ShouldBeTrue();
        result2.ShouldBeTrue();
    }
    
    [Fact]
    public void the_same_domain_error_should_return_the_same_hashcode()
    {
        var domainErrorHashCode1 = new DomainError("Test", "Test").GetHashCode();
        var domainErrorHashCode2 = new DomainError("Test", "Test").GetHashCode();

        var result = domainErrorHashCode1 == domainErrorHashCode2;
        
        result.ShouldBeTrue();
    }
    
    [Fact]
    public void the_different_domain_error_should_return_different_hashcode()
    {
        var domainErrorHashCode1 = new DomainError("Test", "Test").GetHashCode();
        var domainErrorHashCode2 = new DomainError("Test2", "Test2").GetHashCode();

        var result = domainErrorHashCode1 != domainErrorHashCode2;
        
        result.ShouldBeTrue(); 
    }
}