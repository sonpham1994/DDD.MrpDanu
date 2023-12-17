using Application.Extensions;
using Application.LoggingDefinitions;
using Domain.SharedKernel.Base;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Behaviors.Interceptors.Base;

internal abstract class ValidatorBaseInterceptor<TRequest, TResponse>
    where TResponse : IResult
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    private readonly ILogger<ValidatorBaseInterceptor<TRequest, TResponse>> _logger;

    protected ValidatorBaseInterceptor(IEnumerable<IValidator<TRequest>> validators
        , ILogger<ValidatorBaseInterceptor<TRequest, TResponse>> logger)
    {
        _validators = validators;
        _logger = logger;
    }

    protected async Task<TResponse> HandleRequest(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var requestTypeName = typeof(TRequest).Name;

        _logger.StartValidation(requestTypeName);
        
        IReadOnlyList<DomainError> domainErrors = _validators
            .Select(x => x.Validate(request))
            .SelectMany(x => x.Errors)
            .Where(x => x is not null)
            .ToDomainErrors();
        
        if (domainErrors.Count > 0)
        {
            _logger.ValidateFailure(requestTypeName, domainErrors);
            
            //boxing: cast from value type to reference type (struct to interface)
            IResult result = Result.Failure(domainErrors[0]);
            
            return (TResponse)result;
                
            //Should not throw an exception. Please check Functional Programming provided by Vladimir Khorikov
            //check this video to handle if we don't use throw Exception https://www.youtube.com/watch?v=85dxwd8HzEk&ab_channel=MilanJovanovi%C4%87
            //throw new DomainException(domainErrors);
        }
        
        _logger.CompletedValidation(requestTypeName);

        var response = await next();

        return response;
    }
}