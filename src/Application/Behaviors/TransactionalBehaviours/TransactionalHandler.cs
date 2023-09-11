using Application.Behaviors.TransactionalBehaviours.Handlers;
using Application.Interfaces;
using Application.MaterialManagement.MaterialAggregate.Commands.CreateMaterial;
using Domain.SharedKernel.Base;

namespace Application.Behaviors.TransactionalBehaviours;

public sealed class TransactionalHandler
{
    private readonly ITransactionalReceiver[] _transactionalReceivers;
    private readonly string _requestName;
    private IOriginatorCommand _originatorCommand;

    internal TransactionalHandler(string requestName, Func<string, IOriginatorCommand> originatorCommand, params ITransactionalReceiver[] transactionalReceivers)
    {
        _requestName = requestName;
        _originatorCommand = originatorCommand(requestName);
        _transactionalReceivers = transactionalReceivers;
    }

    public async Task<IResult> HandleAsync()
    {
        for (int i = 0; i < _transactionalReceivers.Length; i++)
        {
            var transactionalReceiver = _transactionalReceivers[i];
            if (_requestName == nameof(CreateMaterialCommand))
            {
                // var careTakerCommandHandler = CareTakerCommand.GetRollBackCommand(commandRequest);
                // var memento = careTakerCommandHandler.
            }
            var result = await transactionalReceiver.HandleAsync();
            if (result.IsFailure)
            {
                await HandleRollBackAsync(i);
                return result;
            }
                
        }
            
        return Result.Success();
    }

    private async Task HandleRollBackAsync(int i)
    {
        await _originatorCommand.RollBack();
        for (; i >= 0; i--)
        {
            var type = _transactionalReceivers[i].GetType();
            // if (_transactionalReceivers[i] is RequestHandler<TRequest,TResponse> request)
            // {
            //     var commandRequest = request.Request;
            // }
        }
    }
}