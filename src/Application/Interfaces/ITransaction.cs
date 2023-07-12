using Domain.SharedKernel.Base;
using MediatR;

namespace Application.Interfaces;

public interface ITransaction
{
    Task<IResult> HandleAsync(Func<Task<IResult>> action);
}
