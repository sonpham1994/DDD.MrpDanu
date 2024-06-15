using FluentAssertions;
using Infrastructure;
using Infrastructure.Persistence.Reads;
using NetArchTest.Rules;

namespace Architecture.Tests.InfrastructureArchitectureTests;

public class ReadModelTests
{
    private const string ReadModelClass = "ReadModel";
    
    [Fact]
    public void Read_model_classes_should_not_be_public_access_modifiers()
    {
        var result = Types.InAssembly(InfrastructureAssembly.Instance)
            .That()
            .ImplementInterface(typeof(IReadModel))
            .ShouldNot()
            .BePublic()
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Read_model_classes_should_be_sealed_modifiers()
    {
        var result = Types.InAssembly(InfrastructureAssembly.Instance)
            .That()
            .ImplementInterface(typeof(IReadModel))
            .Should()
            .BeSealed()
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Read_model_classes_should_have_end_name_ReadModel()
    {
        var result = Types.InAssembly(InfrastructureAssembly.Instance)
            .That()
            .ImplementInterface(typeof(IReadModel))
            .Should()
            .HaveNameEndingWith(ReadModelClass)
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Read_model_classes_should_implement_interface_IReadModel()
    {
        var result = Types.InAssembly(InfrastructureAssembly.Instance)
            .That()
            .HaveNameEndingWith(ReadModelClass)
            .And()
            .AreClasses()
            .Should()
            .ImplementInterface(typeof(IReadModel))
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }
}