using Architecture.Tests.Helpers;
using Infrastructure;
using Shouldly;
using Microsoft.EntityFrameworkCore;
using NetArchTest.Rules;
using Infrastructure.Persistence.Reads;
using Application.Interfaces.Writes;
using Application.Interfaces;

namespace Architecture.Tests;
using static ArchitecturePreparingData;

public class InfrastructureTests
{
    private const string QueryClass = "Query";
    

    [Fact]
    public void Infrastructure_should_not_have_dependencies_on_other_projects()
    {
        var infrastructureAssembly = InfrastructureAssembly.Instance;

        var result = Types.InAssembly(infrastructureAssembly).ShouldNot().HaveDependencyOn(ApiNamespace).GetResult();

        result.IsSuccessful.ShouldBeTrue();
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

        result.IsSuccessful.ShouldBeTrue();
    }

    //Should rewrite this test
    //[Fact]
    //public void Persistence_reads_should_not_depend_on_writes()
    //{
    //    var result = Types.InAssembly(InfrastructureAssembly.Instance)
    //        .That()
    //        .ResideInNamespaceContaining(PersistenceReadsNamespace)
    //        .ShouldNot()
    //        .HaveDependencyOn(PersistenceWritesNamespace)
    //        .GetResult();

    //   result.IsSuccessful.ShouldBeTrue();
    //}
}