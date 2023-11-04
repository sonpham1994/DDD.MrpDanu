using Architecture.Tests.Helpers;
using Domain;
using Domain.SharedKernel.Base;
using FluentAssertions;
using NetArchTest.Rules;

//https://www.youtube.com/watch?v=_D6Kai4RdGY&t=536s
namespace Architecture.Tests;
using static ArchitecturePreparingData;

public class DomainArchitectureTests
{
    [Fact]
    public void Domain_should_not_have_dependencies_on_other_projects()
    {
        var domainAssembly = DomainAssembly.Instance;
        var otherProjects = new[]
        {
            ApplicationNamespace,
            InfrastructureNamespace,
            ApiNamespace,
        };

        var result = Types.InAssembly(domainAssembly).ShouldNot().HaveDependencyOnAll(otherProjects).GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void ValueObject_should_be_no_setter()
    {
        var valueObjects = DomainAssembly.Instance
            .GetTypes()
            .Where(x => x.BaseType is not null
                        && x.BaseType == typeof(ValueObject)).ToList();
        var properties = valueObjects.SelectMany(x => x.GetProperties()).ToList();

        var result = properties.Any(x => !x.IsNoSetter());

        result.Should().BeFalse();
    }
    
    [Fact]
    public void Enumeration_properties_should_be_no_setter()
    {
        var enumerations = DomainAssembly.Instance
            .GetTypes()
            .Where(x => x.BaseType is not null
                        && x.BaseType.IsGenericType 
                        && x.BaseType.GetGenericTypeDefinition() == typeof(Enumeration<>)).ToList();
        var properties = enumerations.SelectMany(x => x.GetProperties()).ToList();

        var result = properties.Any(x => !x.IsNoSetter());

        result.Should().BeFalse();
    }
    
    [Fact]
    public void Enumeration_fields_should_be_readonly()
    {
        var enumerations = DomainAssembly.Instance
            .GetTypes()
            .Where(x => x.BaseType is not null
                        && x.BaseType.IsGenericType 
                        && x.BaseType.GetGenericTypeDefinition() == typeof(Enumeration<>)).ToList();
        var fields = enumerations.SelectMany(x => x.GetFields()).ToList();

        var result = fields.Any(x => !x.IsInitOnly);

        result.Should().BeFalse();
    }
    
    [Fact]
    public void Enumeration_fields_should_be_static()
    {
        var enumerations = DomainAssembly.Instance
            .GetTypes()
            .Where(x => x.BaseType is not null
                        && x.BaseType.IsGenericType 
                        && x.BaseType.GetGenericTypeDefinition() == typeof(Enumeration<>)).ToList();
        var fields = enumerations.SelectMany(x => x.GetFields()).ToList();

        var result = fields.Any(x => !x.IsStatic);

        result.Should().BeFalse();
    }

    [Fact]
    public void Entity_setter_access_modifier_should_not_be_public()
    {
        var entitiesType = DomainAssembly.Instance
            .GetTypes()
            .Where(GetEntitiesType()).ToList();
        var properties = entitiesType.SelectMany(x => x.GetProperties()).ToList();
        
        var result = properties.Any(x => x.CanWrite && x.IsPublicSetter());

        result.Should().BeFalse();
    }

    private Func<Type, bool> GetEntitiesType()
    {
        return x => !x.IsAbstract 
                    && ((x.BaseType!.IsGenericType && x.BaseType!.GetGenericTypeDefinition() == typeof(Entity<>))
                        || x.BaseType! == typeof(Entity) 
                        || (x.BaseType!.IsGenericType && x.BaseType!.GetGenericTypeDefinition() == typeof(AggregateRoot<>))
                        || x.BaseType! == typeof(AggregateRoot));
    }
    
}