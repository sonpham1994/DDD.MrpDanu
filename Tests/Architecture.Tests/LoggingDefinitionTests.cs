using System.Reflection;
using Application;
using Shouldly;
using Infrastructure;
using NetArchTest.Rules;

namespace Architecture.Tests;

public class LoggingDefinitionTests
{
    private const string LoggingDefinitionsNamespace = "LoggingDefinitions";
    private readonly Assembly[] _assemblies = [InfrastructureAssembly.Instance, ApplicationAssembly.Instance];
    
    [Fact]
    public void LoggingDefinition_classes_should_not_be_public_access_modifiers()
    {
        var result = Types.InAssemblies(_assemblies)
            .That()
            .ResideInNamespaceEndingWith(LoggingDefinitionsNamespace)
            .ShouldNot()
            .BePublic()
            .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }

    [Fact]
    public void LoggingDefinition_classes_should_be_static()
    {
        var result = Types.InAssemblies(_assemblies)
            .That()
            .ResideInNamespaceEndingWith(LoggingDefinitionsNamespace)
            .Should()
            .BeStatic()
            .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }
}