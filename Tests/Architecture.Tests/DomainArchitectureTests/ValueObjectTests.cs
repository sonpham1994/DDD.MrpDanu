using System.Reflection;
using Architecture.Tests.Helpers;
using Domain;
using SharedKernel;
using Shouldly;
using NetArchTest.Rules;

namespace Architecture.Tests.DomainArchitectureTests;

public class ValueObjectTests
{
    [Fact]
    public void ValueObject_should_be_no_setter_or_init_setter()
    {
        // var valueObjects = DomainAssembly.Instance
        //     .GetTypes()
        //     .Where(x => x.BaseType is not null
        //                 && x.BaseType == typeof(ValueObject)).ToList();
        var valueObjects = Types.InAssembly(DomainAssembly.Instance).That().ImplementInterface(typeof(IValueObject)).GetTypes();
        var properties = valueObjects.SelectMany(x => x.GetProperties()).ToList();

        var result = properties.Any(x => !x.IsNoSetterOrInitSetter());

        result.ShouldBeFalse();
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
            .ImplementInterface(typeof(IValueObject))
            .GetTypes()
            .SelectMany(x=>x.GetConstructors(BindingFlags.Public | BindingFlags.Instance))
            .ToList();
        var result = publicConstructorValueObjects.Any();

        result.ShouldBeFalse();
    }
}