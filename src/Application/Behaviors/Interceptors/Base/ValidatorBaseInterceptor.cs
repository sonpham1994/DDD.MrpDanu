using Application.Extensions;
using Application.LoggingDefinitions;
using Domain.SharedKernel.Base;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Behaviors.Interceptors.Base;

internal abstract class ValidatorBaseInterceptor<TRequest, TResponse>
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
        var typeName = request?.GetGenericTypeName();
        
        _logger.StartValidation(typeName);
        
        IReadOnlyList<DomainError> domainErrors = _validators
            .Select(x => x.Validate(request))
            .SelectMany(x => x.Errors)
            .Where(x => x is not null)
            .ToDomainErrors();
        
        if (domainErrors.Count > 0)
        {
            _logger.ValidateFailure(typeName, domainErrors);
            
            IResult result = Result.Failure(domainErrors[0]);
            
            return (TResponse)result;
                
            //Should not throw an exception. Please check Functional Programming provided by Vladimir Khorikov
            //check this video to handle if we don't use throw Exception https://www.youtube.com/watch?v=85dxwd8HzEk&ab_channel=MilanJovanovi%C4%87
            //throw new DomainException(domainErrors);
        }
        
        _logger.CompletedValidation(typeName);

        var response = await next();

        return response;
    }
}