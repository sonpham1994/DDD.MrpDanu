using Api;
using Architecture.Tests.Helpers;
using FluentAssertions;
using NetArchTest.Rules;

//https://www.youtube.com/watch?v=_D6Kai4RdGY&t=536s
namespace Architecture.Tests;
using static ArchitecturePreparingData;

public class ApiArchitectureTests
{
    private static readonly string[] ApplicationInterfaces = new string[]
    {
        $"{ApplicationNamespace}.Interfaces",
        $"{ApplicationNamespace}.Interfaces.Messaging",
        $"{ApplicationNamespace}.Interfaces.Queries",
        $"{ApplicationNamespace}.Interfaces.Repositories",
        $"{ApplicationNamespace}.Interfaces.Services"
    };

    private const string ControllerClass = "Controller";
    
    [Fact]
    public void Controllers_should_have_dependency_on_MediatR()
    {
        var webAssembly = ApiAssembly.Instance;

        var result = Types.InAssembly(webAssembly)
            .That()
            .HaveNameEndingWith("Controller")
            .Should()
            .HaveDependencyOn("MediatR")
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }
    
    [Fact]
    public void Controllers_should_have_not_dependency_on_Application_interfaces()
    {
        var webAssembly = ApiAssembly.Instance;

        var result = Types.InAssembly(webAssembly)
            .That()
            .HaveNameEndingWith(ControllerClass)
            .ShouldNot()
            .HaveDependencyOnAny(ApplicationInterfaces)
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }
}
