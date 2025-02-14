using Application.Interfaces;
using Application.Interfaces.Writes;
using Shouldly;
using Infrastructure;
using NetArchTest.Rules;

namespace Architecture.Tests.InfrastructureArchitectureTests;

public class RepositoryTests
{
    private const string RepositoryClass = "Repository";
    private const string ReadonlyRepositoryClass = "ReadonlyRepository";
    
    [Fact]
    public void ReadonlyRepository_classes_should_not_be_public_access_modifiers()
    {
        var result = Types.InAssembly(InfrastructureAssembly.Instance)
        .That()
        .ImplementInterface(typeof(IReadonlyRepository))
        .And()
        .AreClasses()
        .ShouldNot()
        .BePublic()
        .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }

    [Fact]
    public void ReadonlyRepository_classes_should_be_sealed_modifiers()
    {
        var result = Types.InAssembly(InfrastructureAssembly.Instance)
        .That()
        .ImplementInterface(typeof(IReadonlyRepository))
        .And()
        .AreClasses()
        .Should()
        .BeSealed()
        .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }

    [Fact]
    public void ReadonlyRepository_classes_should_have_end_name_Repository()
    {
        var result = Types.InAssembly(InfrastructureAssembly.Instance)
        .That()
        .ImplementInterface(typeof(IReadonlyRepository))
        .Should()
        .HaveNameEndingWith(RepositoryClass)
        .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }

    [Fact]
    public void ReadonlyRepository_classes_should_implement_interface_IRepository()
    {
        var result = Types.InAssembly(InfrastructureAssembly.Instance)
        .That()
        .HaveNameEndingWith(ReadonlyRepositoryClass)
        .And()
        .AreClasses()
        .And()
        .DoNotImplementInterface(typeof(IRepository))
        .Should()
        .ImplementInterface(typeof(IReadonlyRepository))
        .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }

    [Fact]
    public void Repository_classes_should_not_be_public_access_modifiers()
    {
        var result = Types.InAssembly(InfrastructureAssembly.Instance)
        .That()
        .ImplementInterface(typeof(IRepository))
        .And()
        .AreClasses()
        .ShouldNot()
        .BePublic()
        .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }

    [Fact]
    public void Repository_classes_should_be_sealed_modifiers()
    {
        var result = Types.InAssembly(InfrastructureAssembly.Instance)
        .That()
        .ImplementInterface(typeof(IRepository))
        .And()
        .AreClasses()
        .Should()
        .BeSealed()
        .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }

    [Fact]
    public void Repository_classes_should_have_end_name_Repository()
    {
        var result = Types.InAssembly(InfrastructureAssembly.Instance)
        .That()
        .ImplementInterface(typeof(IRepository))
        .Should()
        .HaveNameEndingWith(RepositoryClass)
        .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }

    [Fact]
    public void Repository_classes_should_implement_interface_IRepository()
    {
        var result = Types.InAssembly(InfrastructureAssembly.Instance)
        .That()
        .HaveNameEndingWith(RepositoryClass)
        .And()
        .AreClasses()
        .And()
        .DoNotImplementInterface(typeof(IReadonlyRepository))
        .Should()
        .ImplementInterface(typeof(IRepository))
        .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }
}