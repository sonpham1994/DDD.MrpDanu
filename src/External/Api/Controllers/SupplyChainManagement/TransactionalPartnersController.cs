using Api.Controllers.BaseControllers;
using Application.SupplyChainManagement.TransactionalPartnerAggregate.Commands.CreateTransactionalPartner;
using Application.SupplyChainManagement.TransactionalPartnerAggregate.Commands.DeleteTransactionalPartner;
using Application.SupplyChainManagement.TransactionalPartnerAggregate.Commands.UpdateTransactionalPartner;
using Application.SupplyChainManagement.TransactionalPartnerAggregate.Queries.GetSuppliers;
using Application.SupplyChainManagement.TransactionalPartnerAggregate.Queries.GetTransactionalPartnerById;
using Application.SupplyChainManagement.TransactionalPartnerAggregate.Queries.GetTransactionalPartners;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.SupplyChainManagement;

[Route("material-management/[controller]")]
public sealed class TransactionalPartnersController : BaseApiController
{
    public TransactionalPartnersController(ISender sender) : base(sender)
    {
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAsync(CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new GetTransactionalPartnersQuery(), cancellationToken);
        return Success(result.Value);
    }
    
    [HttpGet("suppliers")]
    public async Task<IActionResult> GetSuppliersAsync(CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new GetSuppliersQuery(), cancellationToken);

        return Success(result.Value);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new GetTransactionalPartnerByIdQuery(id), cancellationToken);
        
        return result.IsSuccess ? Success(result.Value) : BadRequest(result.Error);
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