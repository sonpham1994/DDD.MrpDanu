using Shouldly;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using NetArchTest.Rules;

namespace Architecture.Tests.InfrastructureArchitectureTests;

public class DbContextTests
{
    private const string InterceptorNamespace = "Interceptors";
    
    [Fact]
    public void Entity_type_configuration_classes_should_not_be_public_access_modifiers()
    {
        var result = Types.InAssembly(InfrastructureAssembly.Instance)
        .That()
        .ImplementInterface(typeof(IEntityTypeConfiguration<>))
        .ShouldNot()
        .BePublic()
        .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }

    [Fact]
    public void Entity_type_configuration_classes_should_be_sealed_modifiers()
    {
        var result = Types.InAssembly(InfrastructureAssembly.Instance)
        .That()
        .ImplementInterface(typeof(IEntityTypeConfiguration<>))
        .And()
        .AreNotAbstract()
        .Should()
        .BeSealed()
        .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }

    [Fact]
    public void DbContext_classes_should_be_sealed_modifiers()
    {
        var result = Types.InAssembly(InfrastructureAssembly.Instance)
        .That()
        .Inherit(typeof(DbContext))
        .Should()
        .BeSealed()
        .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }

    [Fact]
    public void DbContext_classes_should_not_be_public_access_modifiers()
    {
       var result = Types.InAssembly(InfrastructureAssembly.Instance)
        .That()
        .Inherit(typeof(DbContext))
        .ShouldNot()
        .BePublic()
        .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }

    [Fact]
    public void Interceptor_classes_should_not_be_public_access_modifiers()
    {
        var result = Types.InAssembly(InfrastructureAssembly.Instance)
        .That()
        .ResideInNamespaceEndingWith(InterceptorNamespace)
        .ShouldNot()
        .BePublic()
        .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }

    [Fact]
    public void Interceptor_classes_should_be_sealed_modifiers()
    {
        var result = Types.InAssembly(InfrastructureAssembly.Instance)
        .That()
        .ResideInNamespaceEndingWith(InterceptorNamespace)
        .Should()
        .BeSealed()
        .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }
}