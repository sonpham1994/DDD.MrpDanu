using Api.Controllers.BaseControllers;
using Application.MaterialManagement.TransactionalPartnerAggregate.Commands.CreateTransactionalPartner;
using Application.MaterialManagement.TransactionalPartnerAggregate.Commands.DeleteTransactionalPartner;
using Application.MaterialManagement.TransactionalPartnerAggregate.Commands.UpdateTransactionalPartner;
using Application.MaterialManagement.TransactionalPartnerAggregate.Queries.GetTransactionalPartnerById;
using Application.MaterialManagement.TransactionalPartnerAggregate.Queries.GetTransactionalPartners;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.MaterialManagement;

[Route("material-management/[controller]")]
public sealed class TransactionalPartnerController : BaseApiController
{
    public TransactionalPartnerController(ISender sender) : base(sender)
    {
    }
    
    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new GetTransactionalPartnersQuery(), cancellationToken);
        return Success(result.Value);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new GetTransactionalPartnerByIdQuery(id), cancellationToken);
        
        return result.IsSuccess ? Success() : BadRequest(result.Error);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateTransactionalPartnerCommand request, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(request, cancellationToken);
        
        return result.IsSuccess ? Success() : BadRequest(result.Error);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(UpdateTransactionalPartnerCommand request,
        CancellationToken cancellationToken)
    {
        var result = await Sender.Send(request, cancellationToken);
        
        return result.IsSuccess ? Success() : BadRequest(result.Error);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new DeleteTransactionalPartnerCommand(id), cancellationToken);
        
        return result.IsSuccess ? Success() : BadRequest(result.Error);
    }
}