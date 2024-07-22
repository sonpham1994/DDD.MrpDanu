using Application.Behaviors.Interceptors.Base;
using Application.Interfaces.Messaging;
using SharedKernel;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Behaviors.Interceptors;

internal sealed class ValidatorInterceptor<TRequest, TResponse>(
    IEnumerable<IValidator<TRequest>> validators,
    ILogger<ValidatorInterceptor<TRequest, TResponse>> logger)
    : ValidatorBaseInterceptor<TRequest, TResponse>(validators, logger), IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommand
    where TResponse : IResult
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        return await HandleRequest(request, next, cancellationToken);
    }
}
