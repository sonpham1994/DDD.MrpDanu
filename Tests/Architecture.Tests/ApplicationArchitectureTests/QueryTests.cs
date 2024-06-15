using Application;
using Application.Interfaces.Messaging;
using Architecture.Tests.Helpers;
using FluentAssertions;
using NetArchTest.Rules;

namespace Architecture.Tests.ApplicationArchitectureTests;

using static ArchitecturePreparingData;

public class QueryTests
{
    private const string InterfaceWritesNamespace = $"{ApplicationNamespace}.Interfaces.Writes";
    private const string InterfaceQueriesNamespace = $"{ApplicationNamespace}.Interfaces.Reads";
    private const string ResponseClass = "Response";
    
    [Fact]
    public void Query_handlers_should_have_dependency_on_query()
    {
        var applicationAssembly = ApplicationAssembly.Instance;

        var result = Types.InAssembly(applicationAssembly)
            .That()
            .ImplementInterface(typeof(IQueryHandler<,>))
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
            .ImplementInterface(typeof(IQueryHandler<,>))
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
            .ImplementInterface(typeof(IQueryHandler<,>))
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
            .ImplementInterface(typeof(IQueryHandler<,>))
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
}