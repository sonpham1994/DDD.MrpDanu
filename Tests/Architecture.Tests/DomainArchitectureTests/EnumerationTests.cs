using Architecture.Tests.Helpers;
using Domain;
using Domain.SharedKernel.Base;
using FluentAssertions;

namespace Architecture.Tests.DomainArchitectureTests;

public class EnumerationTests
{
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
}