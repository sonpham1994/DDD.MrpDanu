using System.Reflection;
using Domain;
using Shouldly;
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
                Should.Throw<SafeFailResultDomainException>(() => instance.IsSuccess);
            }
            else
            {
                IResult instance = (IResult)Activator.CreateInstance(resultType)!;
                Should.Throw<SafeFailResultDomainException>(() => instance.IsSuccess);
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
                Should.Throw<SafeFailResultDomainException>(() => instance.Error);
            }
            else
            {
                IResult instance = (IResult)Activator.CreateInstance(resultType)!;
                Should.Throw<SafeFailResultDomainException>(() => instance.Error);
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
                Should.Throw<SafeFailWithCodeResultDomainException>(() => instance.Value);
            }
            else
            {
                var instance = Activator.CreateInstance(resultType);
                var valueProperty = instance!.GetType().GetProperty(nameof(IResult<int>.Value));
            
                // because it throws SafeFailWithCodeResultDomainException inside TargetInvocationException
                var exception = Should.Throw<TargetInvocationException>(() => valueProperty.GetValue(instance));
                exception.InnerException.ShouldBeOfType<SafeFailWithCodeResultDomainException>();
            }
        }
    }

    [Fact]
    public void Create_successful_result_should_be_empty_error()
    {
        var result = Result.Success();
        result.IsSuccess.ShouldBeTrue();
        result.Error.ShouldBe(DomainError.Empty);
    }
    
    [Fact]
    public void Create_failed_result_should_has_error()
    {
        var result = Result.Failure(new DomainError("error","message error"));
        result.IsSuccess.ShouldBeFalse();
        result.Error.ShouldNotBe(DomainError.Empty);
    }
    
    [Fact]
    public void Create_successful_generic_result_and_value_should_not_be_null()
    {
        var result = Result.Success<string>("value");
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldNotBeNull();
        result.Error.ShouldBe(DomainError.Empty);
    }
}