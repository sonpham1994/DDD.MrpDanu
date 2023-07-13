using Architecture.Tests.Helpers;
using Infrastructure;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NetArchTest.Rules;

namespace Architecture.Tests;
using static ArchitecturePreparingData;

public class InfrastructureArchitectureTests
{
    private const string QueryClass = "Query";
    private const string RepositoryClass = "Repository";
    private const string ReadModelClass = "ReadModel";
    private const string EntityTypeConfigurationClass = "EntityTypeConfiguration";
    private const string InterceptorClass = "Interceptor";
    private const string LoggingDefinitionClass = "LoggingDefinition";

    [Fact]
    public void Infrastructure_should_not_have_dependencies_on_other_projects()
    {
        var infrastructureAssembly = InfrastructureAssembly.Instance;

        var result = Types.InAssembly(infrastructureAssembly).ShouldNot().HaveDependencyOn(WebNamespace).GetResult();

        result.IsSuccessful.Should().BeTrue();
    }
    
    [Fact]
    public void Query_should_not_have_dependencies_on_domain()
    {
        var infrastructureAssembly = InfrastructureAssembly.Instance;

        var result = Types.InAssembly(infrastructureAssembly)
            .That()
            .HaveNameEndingWith(QueryClass)
            .ShouldNot().HaveDependencyOn(DomainNamespace).GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Query_classes_should_not_be_public_access_modifiers()
    {
        var queryClasses = InfrastructureAssembly.Instance
            .GetTypes()
            .Where(x => x.Name.EndsWith(QueryClass))
            .ToList();

        var result = queryClasses.Any(x => x.IsPublic);
        result.Should().BeFalse();
    }
    
    [Fact]
    public void Query_classes_should_be_sealed_modifiers()
    {
        var queryClasses = InfrastructureAssembly.Instance
            .GetTypes()
            .Where(x => x.Name.EndsWith(QueryClass))
            .ToList();

        var result = queryClasses.Any(x => !x.IsSealed && !x.IsAbstract);
        result.Should().BeFalse();
    }
    
    [Fact]
    public void Repository_classes_should_not_be_public_access_modifiers()
    {
        var repositoryClasses = InfrastructureAssembly.Instance
            .GetTypes()
            .Where(x => x.Name.EndsWith(RepositoryClass))
            .ToList();

        var result = repositoryClasses.Any(x => x.IsPublic);
        result.Should().BeFalse();
    }
    
    [Fact]
    public void Repository_classes_should_be_sealed_modifiers()
    {
        var repositoryClasses = InfrastructureAssembly.Instance
            .GetTypes()
            .Where(x => x.Name.EndsWith(RepositoryClass))
            .ToList();

        var result = repositoryClasses.Any(x => !x.IsSealed && !x.IsAbstract);
        
        result.Should().BeFalse();
    }
    
    [Fact]
    public void Read_model_classes_should_not_be_public_access_modifiers()
    {
        var readModelClasses = InfrastructureAssembly.Instance
            .GetTypes()
            .Where(x => x.Name.EndsWith(ReadModelClass))
            .ToList();

        var result = readModelClasses.Any(x => x.IsPublic);
        
        result.Should().BeFalse();
    }
    
    [Fact]
    public void Read_model_classes_should_be_sealed_modifiers()
    {
        var readModelClasses = InfrastructureAssembly.Instance
            .GetTypes()
            .Where(x => x.Name.EndsWith(ReadModelClass))
            .ToList();

        var result = readModelClasses.Any(x => !x.IsSealed && !x.IsAbstract);
        result.Should().BeFalse();
    }
    
    [Fact]
    public void Read_model_classes_should_be_init_only_setter()
    {
        var readModelClasses = InfrastructureAssembly.Instance
            .GetTypes()
            .Where(x => x.Name.EndsWith(ReadModelClass))
            .ToList();
        
        var properties = readModelClasses.SelectMany(x => x.GetProperties());
        var result = properties.Any(x => !x.IsInitOnlySetter());

        result.Should().BeFalse();
    }
    
    [Fact]
    public void Entity_type_configuration_classes_should_not_be_public_access_modifiers()
    {
        var readModelClasses = InfrastructureAssembly.Instance
            .GetTypes()
            .Where(x => x.Name.EndsWith(EntityTypeConfigurationClass))
            .ToList();

        var result = readModelClasses.Any(x => x.IsPublic);
        
        result.Should().BeFalse();
    }
    
    [Fact]
    public void Entity_type_configuration_classes_should_be_sealed_modifiers()
    {
        var readModelClasses = InfrastructureAssembly.Instance
            .GetTypes()
            .Where(x => x.Name.EndsWith(EntityTypeConfigurationClass))
            .ToList();

        var result = readModelClasses.Any(x => !x.IsSealed && !x.IsAbstract);
        result.Should().BeFalse();
    }
    
    [Fact]
    public void DbContext_classes_should_be_sealed_modifiers()
    {
        var dbContextClasses = InfrastructureAssembly.Instance
            .GetTypes()
            .Where(x => x.BaseType is not null && x.BaseType == typeof(DbContext))
            .ToList();

        var result = dbContextClasses.Any(x => !x.IsSealed && !x.IsAbstract);
        result.Should().BeFalse();
    }
    
    [Fact]
    public void DbContext_classes_should_not_be_public_access_modifiers()
    {
        var dbContextClasses = InfrastructureAssembly.Instance
            .GetTypes()
            .Where(x => x.BaseType is not null && x.BaseType == typeof(DbContext))
            .ToList();

        var result = dbContextClasses.Any(x => x.IsPublic);
        
        result.Should().BeFalse();
    }
    
    [Fact]
    public void Interceptor_classes_should_not_be_public_access_modifiers()
    {
        var interceptorClasses = InfrastructureAssembly.Instance
            .GetTypes()
            .Where(x => x.Name.EndsWith(InterceptorClass))
            .ToList();

        var result = interceptorClasses.Any(x => x.IsPublic);
        result.Should().BeFalse();
    }
    
    [Fact]
    public void Interceptor_classes_should_be_sealed_modifiers()
    {
        var interceptorClasses = InfrastructureAssembly.Instance
            .GetTypes()
            .Where(x => x.Name.EndsWith(InterceptorClass))
            .ToList();

        var result = interceptorClasses.Any(x => !x.IsSealed && !x.IsAbstract);
        
        result.Should().BeFalse();
    }

    [Fact]
    public void LoggingDefinition_classes_should_not_be_public_access_modifiers()
    {
        var loggingDefinitionClasses = InfrastructureAssembly.Instance
            .GetTypes()
            .Where(x => x.Name.EndsWith(LoggingDefinitionClass))
            .ToList();

        var result = loggingDefinitionClasses.Any(x => x.IsPublic);
        result.Should().BeFalse();
    }

    [Fact]
    public void LoggingDefinition_classes_should_be_static()
    {
        var repositoryClasses = InfrastructureAssembly.Instance
            .GetTypes()
            .Where(x => x.Name.EndsWith(LoggingDefinitionClass))
            .ToList();

        var result = repositoryClasses.Any(x => !x.IsSealed && !x.IsAbstract);

        result.Should().BeFalse();
    }
}