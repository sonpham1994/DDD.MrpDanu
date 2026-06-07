using Application;
using Shouldly;
using NetArchTest.Rules;

//https://www.youtube.com/watch?v=_D6Kai4RdGY&t=536s
namespace Architecture.Tests.ApplicationArchitectureTests;
using static Constants;

public class ApplicationTests
{
    [Fact]
    public void Application_should_not_have_dependencies_on_other_projects()
    {
        var applicationAssembly = ApplicationAssembly.Instance;
        string[] otherProjects = [InfrastructureNamespace, ApiNamespace];

        var result = Types.InAssembly(applicationAssembly).ShouldNot().HaveDependencyOnAll(otherProjects).GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }
}
