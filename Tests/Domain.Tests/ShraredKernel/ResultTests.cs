using System.Reflection;
using Domain.SharedKernel.Base;
using Domain.SharedKernel.Exceptions;
using FluentAssertions;

namespace Domain.Tests.ShraredKernel;

public class ResultTests
{
    [Fact]
    public void Create_result_instance_with_parameterless_access_isSuccess_should_throw_exception()
    {
        var interfaceResultType = typeof(IResult);
        var resultTypes = DomainAssembly.Instance
            .GetTypes()
            .Where(x => interfaceResultType.IsAssignableFrom(x)
                && !x.IsInterface
                && !x.IsGenericTypeDefinition).ToList();

        foreach (var resultType in resultTypes)
        {
            IResult instance = (IResult)Activator.CreateInstance(resultType);
            instance.Invoking(x => x.IsSuccess).Should().Throw<DomainException>();
        }
    }

    [Fact]
    public void Create_result_instance_with_parameterless_access_isFailure_should_throw_exception()
    {
        var interfaceResultType = typeof(IResult);
        var resultTypes = DomainAssembly.Instance
           .GetTypes()
           .Where(x => interfaceResultType.IsAssignableFrom(x)
               && !x.IsInterface
               && !x.IsGenericTypeDefinition).ToList();

        foreach (var resultType in resultTypes)
        {
            IResult instance = (IResult)Activator.CreateInstance(resultType);
            instance.Invoking(x => x.IsFailure).Should().Throw<DomainException>();
        }
    }

    [Fact]
    public void Create_result_instance_with_parameterless_access_error_should_throw_exception()
    {
        var interfaceResultType = typeof(IResult);
        var resultTypes = DomainAssembly.Instance
           .GetTypes()
           .Where(x => interfaceResultType.IsAssignableFrom(x)
               && !x.IsInterface
               && !x.IsGenericTypeDefinition).ToList();

        foreach (var resultType in resultTypes)
        {
            IResult instance = (IResult)Activator.CreateInstance(resultType);
            instance.Invoking(x => x.Error).Should().Throw<DomainException>();
        }
    }

    [Fact]
    public void Create_result_generic_instance_with_parameterless_access_value_should_throw_exception()
    {
        var interfaceResultType = typeof(IResult);
        Type targetType = typeof(int);
        var resultTypes = DomainAssembly.Instance
          .GetTypes()
          .Where(x => interfaceResultType.IsAssignableFrom(x)
              && !x.IsInterface
              && x.IsGenericTypeDefinition).ToList();

        foreach (var resultType in resultTypes)
        {
            Type constructedType = resultType.MakeGenericType(targetType);
            var instance = (IResult<int>)Activator.CreateInstance(constructedType);
            instance.Invoking(x => x.Value).Should().Throw<DomainException>();
        }
    }
}