using Application.Behaviors.Interceptors.Base;
using Application.Interfaces.Messaging;
using SharedKernel;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Behaviors.Interceptors;

internal sealed class ValidatorResponseInterceptor<TRequest, TResponse>(
    IEnumerable<IValidator<TRequest>> validators,
    ILogger<ValidatorResponseInterceptor<TRequest, TResponse>> logger)
    : ValidatorBaseInterceptor<TRequest, TResponse>(validators, logger), IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommand<TResponse>
    where TResponse : IResult<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        return await HandleRequest(request, next, cancellationToken);
    }
}