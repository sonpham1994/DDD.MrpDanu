using FluentAssertions;
using Infrastructure;
using NetArchTest.Rules;

namespace Architecture.Tests.InfrastructureArchitectureTests;

public class QueryTests
{
    private const string QueryClass = "Query";
    
    [Fact]
    public void Query_classes_should_not_be_public_access_modifiers()
    {
        var result = Types.InAssembly(InfrastructureAssembly.Instance)
            .That()
            .HaveNameEndingWith(QueryClass)
            .ShouldNot()
            .BePublic()
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Query_classes_should_be_sealed_modifiers()
    {
        var result = Types.InAssembly(InfrastructureAssembly.Instance)
            .That()
            .HaveNameEndingWith(QueryClass)
            .Should()
            .BeSealed()
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }
}