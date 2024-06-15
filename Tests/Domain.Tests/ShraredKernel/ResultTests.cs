using System.Reflection;
using Domain.SharedKernel.Base;
using Domain.SharedKernel.Exceptions;
using FluentAssertions;

namespace Domain.Tests.ShraredKernel;

public class ResultTests
{
    [Fact]
    public void Create_result_instance_with_parameterless_and_accesses_isSuccess_should_throw_exception()
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
    public void Create_result_instance_with_parameterless_and_accesses_error_should_throw_exception()
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
    public void Create_result_generic_instance_with_parameterless_and_accesses_value_should_throw_exception()
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

    [Fact]
    public void Create_successful_result_should_be_empty_error()
    {
        var result = Result.Success();
        result.IsSuccess.Should().BeTrue();
        result.Error.Should().Be(DomainError.Empty);
    }
    
    [Fact]
    public void Create_failed_result_should_has_error()
    {
        var result = Result.Failure(new DomainError("error","message error"));
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().NotBe(DomainError.Empty);
    }
    
    [Fact]
    public void Create_successful_generic_result_and_value_should_not_be_null()
    {
        var result = Result.Success<string>("value");
        result.IsSuccess.Should().BeFalse();
        result.Value.Should().NotBeNull();
        result.Error.Should().NotBe(DomainError.Empty);
    }
}