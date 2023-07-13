using Application.Behaviors.Interceptors.Base;
using Application.Interfaces.Messaging;
using Domain.SharedKernel.Base;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Behaviors.Interceptors;

internal sealed class ValidatorResponseInterceptor<TRequest, TResponse> : ValidatorBaseInterceptor<TRequest, TResponse>, IPipelineBehavior<TRequest, TResponse> 
    where TRequest : ICommand<TResponse>
    where TResponse : IResult<TResponse>
{
    public ValidatorResponseInterceptor(IEnumerable<IValidator<TRequest>> validators
        , ILogger<ValidatorResponseInterceptor<TRequest, TResponse>> logger) : base(validators, logger)
    {
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        return await HandleRequest(request, next, cancellationToken);
    }
}