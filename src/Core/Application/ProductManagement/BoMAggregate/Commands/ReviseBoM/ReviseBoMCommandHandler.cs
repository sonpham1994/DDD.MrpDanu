using Application.Interfaces.Messaging;
using Domain.ProductManagement;
using Domain.SharedKernel.Base;

namespace Application.ProductManagement.BoMAggregate.Commands.ReviseBoM;

// internal sealed class ReviseBoMCommandHandler : ICommandHandler<ReviseBoMCommand>
// {
//     public async Task<Result> Handle(ReviseBoMCommand request, CancellationToken cancellationToken)
//     {
//         var bomMaterials = request.BoMMaterials.Select(x =>
//         {
//             var unit = Unit.Create(x.Unit);
//             if (unit.IsFailure)
//                 return unit;
//         });
//     }
// }