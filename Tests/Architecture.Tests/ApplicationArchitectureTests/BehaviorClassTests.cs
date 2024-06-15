using Application;
using FluentAssertions;
using NetArchTest.Rules;

namespace Architecture.Tests.ApplicationArchitectureTests;

public class BehaviorClassTests
{
    private const string InterceptorClass = "Interceptor`2";
    private const string BehaviorClass = "Behavior`2";
    
    [Fact]
    public void Interceptor_classes_should_not_be_public_access_modifiers()
    {
        var result = Types.InAssembly(ApplicationAssembly.Instance)
            .That()
            .HaveNameEndingWith(InterceptorClass)
            .ShouldNot()
            .BePublic()
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Interceptor_classes_should_be_sealed_modifiers()
    {
        var result = Types.InAssembly(ApplicationAssembly.Instance)
            .That()
            .HaveNameEndingWith(InterceptorClass)
            .And()
            .AreNotAbstract()
            .Should()
            .BeSealed()
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Behavior_classes_should_not_be_public_access_modifiers()
    {
        var result = Types.InAssembly(ApplicationAssembly.Instance)
            .That()
            .HaveNameEndingWith(BehaviorClass)
            .And()
            .AreNotAbstract()
            .ShouldNot()
            .BePublic()
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Behavior_classes_should_be_sealed_modifiers()
    {
        var result = Types.InAssembly(ApplicationAssembly.Instance)
            .That()
            .HaveNameEndingWith(BehaviorClass)
            .And()
            .AreNotAbstract()
            .Should()
            .BeSealed()
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }
}