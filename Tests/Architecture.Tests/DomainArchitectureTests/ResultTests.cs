using System.Reflection;
using Domain;
using FluentAssertions;
using NetArchTest.Rules;
using SharedKernel;
using SharedKernel.Exceptions;

namespace Architecture.Tests.DomainArchitectureTests;

public class ResultTests
{
    [Fact]
    public void Create_result_instance_with_parameterless_and_accesses_isSuccess_should_throw_exception()
    {
        var interfaceResultType = typeof(IResult);
        var resultTypes = Types.InAssemblies([DomainAssembly.Instance, SharedKernelAssembly.Instance])
            .GetTypes()
            .Where(x => interfaceResultType.IsAssignableFrom(x)
                && !x.IsInterface).ToList();
        Type targetType = typeof(int);
        
        foreach (var resultType in resultTypes)
        {
            if (resultType.IsGenericType)
            {
                Type constructedType = resultType.MakeGenericType(targetType);
                var instance = (IResult<int>)Activator.CreateInstance(constructedType)!;
                instance.Invoking(x => x.IsSuccess).Should().Throw<SafeFailResultDomainException>();
            }
            else
            {
                IResult instance = (IResult)Activator.CreateInstance(resultType)!;
                instance.Invoking(x => x.IsSuccess).Should().Throw<SafeFailResultDomainException>();
            }
        }
    }

    [Fact]
    public void Create_result_instance_with_parameterless_and_accesses_error_should_throw_exception()
    {
        var interfaceResultType = typeof(IResult);
        var resultTypes = Types.InAssemblies([DomainAssembly.Instance, SharedKernelAssembly.Instance])
           .GetTypes()
           .Where(x => interfaceResultType.IsAssignableFrom(x)
               && !x.IsInterface).ToList();
        Type targetType = typeof(int);

        foreach (var resultType in resultTypes)
        {
            if (resultType.IsGenericType)
            {
                Type constructedType = resultType.MakeGenericType(targetType);
                var instance = (IResult<int>)Activator.CreateInstance(constructedType)!;
                instance.Invoking(x => x.Error).Should().Throw<SafeFailResultDomainException>();
            }
            else
            {
                IResult instance = (IResult)Activator.CreateInstance(resultType)!;
                instance.Invoking(x => x.Error).Should().Throw<SafeFailResultDomainException>();
            }
        }
    }

    [Fact]
    public void Create_result_generic_instance_with_parameterless_and_accesses_value_should_throw_exception()
    {
        var interfaceResultGenericType = typeof(IResult<>);
        var resultTypes = Types.InAssemblies([DomainAssembly.Instance, SharedKernelAssembly.Instance])
            .GetTypes()
            .Where(t => t.GetInterfaces()
                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == interfaceResultGenericType))
            .ToArray();

        Type targetType = typeof(int);
        foreach (var resultType in resultTypes)
        {
            if (resultType.IsGenericType)
            {
                Type constructedType = resultType.MakeGenericType(targetType);
                var instance = (IResult<int>)Activator.CreateInstance(constructedType)!;
                instance.Invoking(x => x.Value).Should().Throw<SafeFailWithCodeResultDomainException>();
            }
            else
            {
                var instance = Activator.CreateInstance(resultType);
                var valueProperty = instance!.GetType().GetProperty(nameof(IResult<int>.Value));
            
                // because it throws SafeFailWithCodeResultDomainException inside TargetInvocationException
                valueProperty.Invoking(x => x.GetValue(instance))
                    .Should()
                    .Throw<TargetInvocationException>()
                    .Which
                    .InnerException
                    .Should()
                    .BeOfType<SafeFailWithCodeResultDomainException>();
            }
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
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Error.Should().Be(DomainError.Empty);
    }
}