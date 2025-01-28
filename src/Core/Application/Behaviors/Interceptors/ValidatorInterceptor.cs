using System.Reflection;
using Application.Extensions;
using Application.Interfaces.Messaging;
using Application.LoggingDefinitions;
using SharedKernel;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Behaviors.Interceptors;

internal sealed class ValidatorInterceptor<TRequest, TResponse>(
    IEnumerable<IValidator<TRequest>> validators,
    ILogger<ValidatorInterceptor<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IValidationCommand
    where TResponse : IResult
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var requestTypeName = typeof(TRequest).Name;

        logger.StartValidation(requestTypeName);
        
        IReadOnlyList<DomainError> domainErrors = validators
            .Select(x => x.Validate(request))
            .SelectMany(x => x.Errors)
            .Where(x => x is not null)
            .ToDomainErrors();
        if (domainErrors.Count > 0)
        {
            logger.ValidateFailure(requestTypeName, domainErrors);
            //case Result<T>
            if (typeof(TResponse).IsGenericType && typeof(TResponse).GetGenericTypeDefinition() == typeof(Result<>))
            {
                return CreateFailureResponse(domainErrors[0]);
            }
            else
            {
                //boxing: cast from value type to reference type (struct to interface)
                IResult result = Result.Failure(domainErrors[0]);
                return (TResponse)result;
            }
                
            //Should not throw an exception. Please check Functional Programming provided by Vladimir Khorikov
            //check this video to handle if we don't use throw Exception https://www.youtube.com/watch?v=85dxwd8HzEk&ab_channel=MilanJovanovi%C4%87
            //throw new DomainException(domainErrors);
        }
        
        logger.CompletedValidation(requestTypeName);

        var response = await next();

        return response;
    }
    
    private TResponse CreateFailureResponse(in DomainError error)
    {
        var genericResultType = typeof(TResponse).GetGenericArguments()[0];
        if (genericResultType is not null)
        {
            var invalidMethod = typeof(Result<>)
                .MakeGenericType(genericResultType)
                .GetMethods()
                .FirstOrDefault(m => m.Name == nameof(Result<int>.Failure));

            return (TResponse)invalidMethod!.Invoke(null, new object[] { error })!;
        }

        return (TResponse)(object)Result.Failure(error);
    }
}
