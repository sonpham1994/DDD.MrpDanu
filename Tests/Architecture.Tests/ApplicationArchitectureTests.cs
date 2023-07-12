using Application;
using Application.Extensions;
using Architecture.Tests.Helpers;
using FluentAssertions;
using Infrastructure;
using NetArchTest.Rules;

//https://www.youtube.com/watch?v=_D6Kai4RdGY&t=536s
namespace Architecture.Tests;
using static ArchitecturePreparingData;

public class ApplicationArchitectureTests
{
    private const string InterfaceQueriesNamespace = $"{ApplicationNamespace}.Interfaces.Queries";
    private const string QueryHandlerClass = "QueryHandler";
    private const string CommandHandlerClass = "CommandHandler";
    private const string CommandClass = "Command";
    private const string ResponseClass = "Response";
    private const string CommandValidatorClass = "CommandValidator";
    private const string InterceptorClass = "Interceptor`2";
    private const string BehaviorClass = "Behavior`2";
    private const string LoggingDefinitionClass = "LoggingDefinition";

    [Fact]
    public void Application_should_not_have_dependencies_on_other_projects()
    {
        var applicationAssembly = ApplicationAssembly.Instance;
        var otherProjects = new[]
        {
            InfrastructureNamespace,
            WebNamespace,
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
            .Should()
            .HaveDependencyOn(DomainNamespace)
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }
    
    [Fact]
    public void Command_handlers_should_be_not_public_access_modifier()
    {
        var commandHandlerClasses = ApplicationAssembly.Instance
            .GetTypes()
            .Where(x => x.Name.EndsWith(CommandHandlerClass))
            .ToList();

        var result = commandHandlerClasses.Any(x => x.IsPublic);
        
        result.Should().BeFalse();
    }
    
    [Fact]
    public void Command_handlers_should_be_sealed_modifiers()
    {
        var commandHandlerClasses = ApplicationAssembly.Instance
            .GetTypes()
            .Where(x => x.Name.EndsWith(CommandHandlerClass))
            .ToList();

        var result = commandHandlerClasses.Any(x => !x.IsSealed && !x.IsAbstract);
        
        result.Should().BeFalse();
    }
    
    [Fact]
    public void Command_properties_should_be_init_only_setter()
    {
        var responseClasses = ApplicationAssembly.Instance
            .GetTypes()
            .Where(x => x.Name.EndsWith(CommandClass))
            .ToList();
        
        var properties = responseClasses.SelectMany(x => x.GetProperties());
        var result = properties.Any(x => !x.IsInitOnlySetter());

        result.Should().BeFalse();
    }
    
    [Fact]
    public void Command_should_be_sealed_modifiers()
    {
        var commandClasses = ApplicationAssembly.Instance
            .GetTypes()
            .Where(x => x.Name.EndsWith(CommandClass))
            .ToList();

        var result = commandClasses.Any(x => !x.IsSealed && !x.IsAbstract);
        
        result.Should().BeFalse();
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
    public void Query_handlers_should_be_not_public_access_modifier()
    {
        var queryHandlerClasses = ApplicationAssembly.Instance
            .GetTypes()
            .Where(x => x.Name.EndsWith(QueryHandlerClass))
            .ToList();

        var result = queryHandlerClasses.Any(x => x.IsPublic);
        
        result.Should().BeFalse();
    }
    
    [Fact]
    public void Query_handlers_should_be_sealed_modifiers()
    {
        var queryHandlerClasses = ApplicationAssembly.Instance
            .GetTypes()
            .Where(x => x.Name.EndsWith(QueryHandlerClass))
            .ToList();

        var result = queryHandlerClasses.Any(x => !x.IsSealed && !x.IsAbstract);
        
        result.Should().BeFalse();
    }
    
    [Fact]
    public void Command_validators_should_be_not_public_access_modifier()
    {
        var commandValidatorClasses = ApplicationAssembly.Instance
            .GetTypes()
            .Where(x => x.Name.EndsWith(CommandValidatorClass))
            .ToList();

        var result = commandValidatorClasses.Any(x => x.IsPublic);
        
        result.Should().BeFalse();
    }
    
    [Fact]
    public void Command_validators_should_be_sealed_modifiers()
    {
        var commandValidatorClasses = ApplicationAssembly.Instance
            .GetTypes()
            .Where(x => x.Name.EndsWith(CommandValidatorClass))
            .ToList();

        var result = commandValidatorClasses.Any(x => !x.IsSealed && !x.IsAbstract);
        
        result.Should().BeFalse();
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
    public void Response_data_type_properties_should_be_init_only_setter()
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
        var interceptorClasses = ApplicationAssembly.Instance
           .GetTypes()
           .Where(x => x.Name.EndsWith(InterceptorClass))
           .ToList();

        var result = interceptorClasses.Any(x => x.IsPublic);
        result.Should().BeFalse();
    }

    [Fact]
    public void Interceptor_classes_should_be_sealed_modifiers()
    {
        var interceptorClasses = ApplicationAssembly.Instance
            .GetTypes()
            .Where(x => x.Name.EndsWith(InterceptorClass))
            .ToList();

        var result = interceptorClasses.Any(x => !x.IsSealed && !x.IsAbstract);

        result.Should().BeFalse();
    }

    [Fact]
    public void Behavior_classes_should_not_be_public_access_modifiers()
    {
        var behaviorClasses = ApplicationAssembly.Instance
            .GetTypes()
            .Where(x => x.Name.EndsWith(BehaviorClass))
            .ToList();

        var result = behaviorClasses.Any(x => x.IsPublic);
        result.Should().BeFalse();
    }

    [Fact]
    public void Behavior_classes_should_be_sealed_modifiers()
    {
        var behaviorClasses = ApplicationAssembly.Instance
            .GetTypes()
            .Where(x => x.Name.EndsWith(BehaviorClass))
            .ToList();

        var result = behaviorClasses.Any(x => !x.IsSealed && !x.IsAbstract);

        result.Should().BeFalse();
    }

    [Fact]
    public void LoggingDefinition_classes_should_not_be_public_access_modifiers()
    {
        var loggingDefinitionClasses = ApplicationAssembly.Instance
            .GetTypes()
            .Where(x => x.Name.EndsWith(LoggingDefinitionClass))
            .ToList();

        var result = loggingDefinitionClasses.Any(x => x.IsPublic);
        result.Should().BeFalse();
    }

    [Fact]
    public void LoggingDefinition_classes_should_be_static()
    {
        var repositoryClasses = ApplicationAssembly.Instance
            .GetTypes()
            .Where(x => x.Name.EndsWith(LoggingDefinitionClass))
            .ToList();

        var result = repositoryClasses.Any(x => !x.IsSealed && !x.IsAbstract);

        result.Should().BeFalse();
    }
}