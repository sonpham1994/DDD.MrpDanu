using Application;
using Application.Interfaces.Messaging;
using Architecture.Tests.Helpers;
using FluentAssertions;
using FluentValidation;
using NetArchTest.Rules;

namespace Architecture.Tests.ApplicationArchitectureTests;
using static ArchitecturePreparingData;

public class CommandTests
{
    private const string InterfaceQueriesNamespace = $"{ApplicationNamespace}.Interfaces.Reads";
    
    [Fact]
    public void Command_handlers_should_have_dependency_on_domain()
    {
        var applicationAssembly = ApplicationAssembly.Instance;

        var result = Types.InAssembly(applicationAssembly)
            .That()
            .ImplementInterface(typeof(ICommandHandler<>))
            .Or()
            .ImplementInterface(typeof(ICommandHandler<,>))
            .Should()
            .HaveDependencyOnAny(DomainNamespace, ApplicationNamespace)
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Command_handlers_should_be_not_public_access_modifier()
    {
        var result = Types.InAssembly(ApplicationAssembly.Instance)
        .That()
        .ImplementInterface(typeof(ICommandHandler<>))
        .Or()
        .ImplementInterface(typeof(ICommandHandler<,>))
        .ShouldNot()
        .BePublic()
        .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Command_handlers_should_be_sealed_modifiers()
    {
        var result = Types.InAssembly(ApplicationAssembly.Instance)
        .That()
        .ImplementInterface(typeof(ICommandHandler<>))
        .Or()
        .ImplementInterface(typeof(ICommandHandler<,>))
        .Should()
        .BeSealed()
        .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Command_handlers_should_not_depend_on_reads_interface()
    {
        var result = Types.InAssembly(ApplicationAssembly.Instance)
        .That()
        .ImplementInterface(typeof(ICommandHandler<>))
        .Or()
        .ImplementInterface(typeof(ICommandHandler<,>))
        .ShouldNot()
        .HaveDependencyOn(InterfaceQueriesNamespace)
        .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Command_properties_should_be_immutable()
    {
        var commandClasses = Types.InAssembly(ApplicationAssembly.Instance)
        .That()
        .ImplementInterface(typeof(ICommand))
        .Or()
        .ImplementInterface(typeof(ICommand<>))
        .GetTypes()
        .ToList();

        var properties = commandClasses.SelectMany(x => x.GetProperties());
        var result = properties.Any(x => !x.IsInitOnlySetter());

        result.Should().BeFalse();
    }

    [Fact]
    public void Command_should_be_sealed_modifiers()
    {
        var result = Types.InAssembly(ApplicationAssembly.Instance)
        .That()
        .ImplementInterface(typeof(ICommand))
        .Or()
        .ImplementInterface(typeof(ICommand<>))
        .Should()
        .BeSealed()
        .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }
    
    [Fact]
    public void Command_validators_should_not_be_public_access_modifier()
    {
        var result = Types.InAssembly(ApplicationAssembly.Instance)
            .That()
            .Inherit(typeof(AbstractValidator<>))
            .ShouldNot()
            .BePublic()
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Command_validators_should_be_sealed_modifiers()
    {
        var result = Types.InAssembly(ApplicationAssembly.Instance)
            .That()
            .Inherit(typeof(AbstractValidator<>))
            .Should()
            .BeSealed()
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }
}