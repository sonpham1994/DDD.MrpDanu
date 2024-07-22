using Architecture.Tests.Helpers;
using Domain;
using SharedKernel;
using FluentAssertions;

namespace Architecture.Tests.DomainArchitectureTests;

public class EntityTests
{
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