using Application.Behaviors.Interceptors.Base;
using Application.Interfaces.Messaging;
using Domain.SharedKernel.Base;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Behaviors.Interceptors;

internal sealed class ValidatorInterceptor<TRequest, TResponse> : ValidatorBaseInterceptor<TRequest, TResponse>, IPipelineBehavior<TRequest, TResponse> 
    where TRequest : ICommand
    where TResponse : IResult
{
    public ValidatorInterceptor(IEnumerable<IValidator<TRequest>> validators
        , ILogger<ValidatorInterceptor<TRequest, TResponse>> logger) : base(validators, logger)
    {
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        return await HandleRequest(request, next, cancellationToken);
    }
}
