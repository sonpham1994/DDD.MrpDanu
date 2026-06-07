using Domain;
using Shouldly;
using NetArchTest.Rules;

//https://www.youtube.com/watch?v=_D6Kai4RdGY&t=536s
namespace Architecture.Tests.DomainArchitectureTests;
using static Constants;

public class DomainTests
{
    [Fact]
    public void Domain_should_not_have_dependencies_on_other_projects()
    {
        var domainAssembly = DomainAssembly.Instance;
        string[] otherProjects = [ApplicationNamespace, InfrastructureNamespace, ApiNamespace];

        var result = Types.InAssembly(domainAssembly).ShouldNot().HaveDependencyOnAll(otherProjects).GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }
}