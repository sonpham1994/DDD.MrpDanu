using Architecture.Tests.Helpers;
using Infrastructure;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NetArchTest.Rules;
using Infrastructure.Persistence.Reads;

namespace Architecture.Tests;
using static ArchitecturePreparingData;

public class InfrastructureArchitectureTests
{
    private const string QueryClass = "Query";
    private const string RepositoryClass = "Repository";
    private const string ReadModelClass = "ReadModel";
    private const string InterceptorNamespace = "Interceptors";
    private const string LoggingDefinitionsNamespace = "LoggingDefinitions";
    private const string PersistenceReadsNamespace = "Infrastructure.Persistence.Reads";
    private const string PersistenceWritesNamespace = "Infrastructure.Persistence.Writes";

    [Fact]
    public void Infrastructure_should_not_have_dependencies_on_other_projects()
    {
        var infrastructureAssembly = InfrastructureAssembly.Instance;

        var result = Types.InAssembly(infrastructureAssembly).ShouldNot().HaveDependencyOn(ApiNamespace).GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Query_should_not_have_dependencies_on_domain()
    {
        var infrastructureAssembly = InfrastructureAssembly.Instance;

        var result = Types.InAssembly(infrastructureAssembly)
            .That()
            .HaveNameEndingWith(QueryClass)
            .ShouldNot()
            .HaveDependencyOn(DomainNamespace)
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

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

    [Fact]
    public void Repository_classes_should_not_be_public_access_modifiers()
    {
        var result = Types.InAssembly(InfrastructureAssembly.Instance)
        .That()
        .HaveNameEndingWith(RepositoryClass)
        .ShouldNot()
        .BePublic()
        .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Repository_classes_should_be_sealed_modifiers()
    {
        var result = Types.InAssembly(InfrastructureAssembly.Instance)
        .That()
        .HaveNameEndingWith(RepositoryClass)
        .Should()
        .BeSealed()
        .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

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

    [Fact]
    public void Entity_type_configuration_classes_should_not_be_public_access_modifiers()
    {
        var result = Types.InAssembly(InfrastructureAssembly.Instance)
        .That()
        .ImplementInterface(typeof(IEntityTypeConfiguration<>))
        .ShouldNot()
        .BePublic()
        .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Entity_type_configuration_classes_should_be_sealed_modifiers()
    {
        var result = Types.InAssembly(InfrastructureAssembly.Instance)
        .That()
        .ImplementInterface(typeof(IEntityTypeConfiguration<>))
        .Should()
        .BeSealed()
        .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void DbContext_classes_should_be_sealed_modifiers()
    {
        var result = Types.InAssembly(InfrastructureAssembly.Instance)
        .That()
        .Inherit(typeof(DbContext))
        .Should()
        .BeSealed()
        .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void DbContext_classes_should_not_be_public_access_modifiers()
    {
       var result = Types.InAssembly(InfrastructureAssembly.Instance)
        .That()
        .Inherit(typeof(DbContext))
        .ShouldNot()
        .BePublic()
        .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Interceptor_classes_should_not_be_public_access_modifiers()
    {
        var result = Types.InAssembly(InfrastructureAssembly.Instance)
        .That()
        .ResideInNamespaceEndingWith(InterceptorNamespace)
        .ShouldNot()
        .BePublic()
        .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Interceptor_classes_should_be_sealed_modifiers()
    {
        var result = Types.InAssembly(InfrastructureAssembly.Instance)
        .That()
        .ResideInNamespaceEndingWith(InterceptorNamespace)
        .Should()
        .BeSealed()
        .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void LoggingDefinition_classes_should_not_be_public_access_modifiers()
    {
        var result = Types.InAssembly(InfrastructureAssembly.Instance)
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
        var result = Types.InAssembly(InfrastructureAssembly.Instance)
        .That()
        .ResideInNamespaceEndingWith(LoggingDefinitionsNamespace)
        .Should()
        .BeStatic()
        .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Persistence_reads_should_not_depend_on_writes()
    {
        var result = Types.InAssembly(InfrastructureAssembly.Instance)
            .That()
            .ResideInNamespaceContaining(PersistenceReadsNamespace)
            .ShouldNot()
            .HaveDependencyOn(PersistenceWritesNamespace)
            .GetResult();

       result.IsSuccessful.Should().BeTrue();
    }
}