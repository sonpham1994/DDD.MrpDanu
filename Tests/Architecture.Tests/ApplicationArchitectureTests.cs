using Application;
using Application.Interfaces.Messaging;
using Architecture.Tests.Helpers;
using FluentAssertions;
using FluentValidation;
using NetArchTest.Rules;

//https://www.youtube.com/watch?v=_D6Kai4RdGY&t=536s
namespace Architecture.Tests;
using static ArchitecturePreparingData;

public class ApplicationArchitectureTests
{
    private const string InterfaceQueriesNamespace = $"{ApplicationNamespace}.Interfaces.Reads";
    private const string InterfaceWritesNamespace = $"{ApplicationNamespace}.Interfaces.Writes";
    private const string QueryHandlerClass = "QueryHandler";
    private const string CommandHandlerClass = "CommandHandler";
    private const string CommandClass = "Command";
    private const string ResponseClass = "Response";
    private const string InterceptorClass = "Interceptor`2";
    private const string BehaviorClass = "Behavior`2";
    private const string LoggingDefinitionsNamespace = "LoggingDefinitions";

    [Fact]
    public void Application_should_not_have_dependencies_on_other_projects()
    {
        var applicationAssembly = ApplicationAssembly.Instance;
        var otherProjects = new[]
        {
            InfrastructureNamespace,
            ApiNamespace,
        };

        var result = Types.InAssembly(applicationAssembly).ShouldNot().HaveDependencyOnAll(otherProjects).GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Command_handlers_should_have_dependency_on_domain()
    {
        var applicationAssembly = ApplicationAssembly.Instance;

        var result = Types.InAssembly(applicationAssembly)
            .That()
            .HaveNameEndingWith(CommandHandlerClass)
            .And()
            .AreClasses()
            .Should()
            .HaveDependencyOn(DomainNamespace)
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Command_handlers_should_be_not_public_access_modifier()
    {
        var result = Types.InAssembly(ApplicationAssembly.Instance)
        .That()
        .HaveNameEndingWith(CommandHandlerClass)
        .And()
        .AreClasses()
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
        .HaveNameEndingWith(CommandHandlerClass)
        .And()
        .AreClasses()
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
        .HaveNameEndingWith(CommandHandlerClass)
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
        .HaveNameEndingWith(CommandClass)
        .And()
        .AreClasses()
        .Should()
        .BeSealed()
        .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Query_handlers_should_have_dependency_on_query()
    {
        var applicationAssembly = ApplicationAssembly.Instance;

        var result = Types.InAssembly(applicationAssembly)
            .That()
            .HaveNameEndingWith(QueryHandlerClass)
            .Should()
            .HaveDependencyOn(InterfaceQueriesNamespace)
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Query_handlers_should_not_have_dependency_on_writes_interface()
    {
        var applicationAssembly = ApplicationAssembly.Instance;

        var result = Types.InAssembly(applicationAssembly)
            .That()
            .HaveNameEndingWith(QueryHandlerClass)
            .ShouldNot()
            .HaveDependencyOn(InterfaceWritesNamespace)
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Query_handlers_should_not_be_public_access_modifier()
    {
        var result = Types.InAssembly(ApplicationAssembly.Instance)
        .That()
        .ImplementInterface(typeof(ICommandHandler<>))
        .ShouldNot()
        .BePublic()
        .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Query_handlers_should_be_sealed_modifiers()
    {
        var result = Types.InAssembly(ApplicationAssembly.Instance)
        .That()
        .ImplementInterface(typeof(ICommandHandler<>))
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

    [Fact]
    public void Response_data_type_should_not_have_dependency_on_domain()
    {
        var applicationAssembly = ApplicationAssembly.Instance;

        var result = Types.InAssembly(applicationAssembly)
            .That()
            .HaveNameEndingWith(ResponseClass)
            .ShouldNot()
            .HaveDependencyOn(DomainNamespace)
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Response_data_type_properties_should_be_immutable()
    {
        var responseClasses = ApplicationAssembly.Instance
            .GetTypes()
            .Where(x => x.Name.EndsWith(ResponseClass))
            .ToList();

        var properties = responseClasses.SelectMany(x => x.GetProperties());
        var result = properties.Any(x => !x.IsInitOnlySetter());

        result.Should().BeFalse();
    }

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

    [Fact]
    public void LoggingDefinition_classes_should_not_be_public_access_modifiers()
    {
        var result = Types.InAssembly(ApplicationAssembly.Instance)
        .That()
        .ResideInNamespaceEndingWith(LoggingDefinitionsNamespace)
        .ShouldNot()
        .BePublic()
        .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void LoggingDefinition_classes_should_be_static()
    {
        var result = Types.InAssembly(ApplicationAssembly.Instance)
        .That()
        .ResideInNamespaceEndingWith(LoggingDefinitionsNamespace)
        .Should()
        .BeStatic()
        .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }
}