using System.Reflection;
using Architecture.Tests.Helpers;
using Domain;
using Domain.SharedKernel.Base;
using FluentAssertions;
using NetArchTest.Rules;

namespace Architecture.Tests.DomainArchitectureTests;

public class ValueObjectTests
{
    [Fact]
    public void ValueObject_should_be_no_setter()
    {
        // var valueObjects = DomainAssembly.Instance
        //     .GetTypes()
        //     .Where(x => x.BaseType is not null
        //                 && x.BaseType == typeof(ValueObject)).ToList();
        var valueObjects = Types.InAssembly(DomainAssembly.Instance).That().Inherit(typeof(ValueObject)).GetTypes();
        var properties = valueObjects.SelectMany(x => x.GetProperties()).ToList();

        var result = properties.Any(x => !x.IsNoSetter());

        result.Should().BeFalse();
    }
    
    [Fact]
    public void ValueObject_should_be_no_public_constructor()
    {
        // var valueObjects = DomainAssembly.Instance
        //     .GetTypes()
        //     .Where(x => x.BaseType is not null
        //                 && x.BaseType == typeof(ValueObject)).ToList();
        var publicConstructorValueObjects = Types.InAssembly(DomainAssembly.Instance)
            .That()
            .Inherit(typeof(ValueObject))
            .GetTypes()
            .SelectMany(x=>x.GetConstructors(BindingFlags.Public | BindingFlags.Instance))
            .ToList();
        var result = publicConstructorValueObjects.Any();

        result.Should().BeFalse();
    }
}