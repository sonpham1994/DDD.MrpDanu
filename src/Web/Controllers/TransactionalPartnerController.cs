using Application.MaterialManagement.TransactionalPartnerAggregate.Commands.CreateTransactionalPartner;
using Application.MaterialManagement.TransactionalPartnerAggregate.Commands.DeleteTransactionalPartner;
using Application.MaterialManagement.TransactionalPartnerAggregate.Commands.UpdateTransactionalPartner;
using Application.MaterialManagement.TransactionalPartnerAggregate.Queries.GetTransactionalPartnerById;
using Application.MaterialManagement.TransactionalPartnerAggregate.Queries.GetTransactionalPartners;
using Domain.MaterialManagement.TransactionalPartnerAggregate;
using Domain.SharedKernel;
using Web.ApiModels.BaseResponses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Controllers.MaterialManagement;

public sealed partial class MaterialManagementController
{
    [HttpGet("TransactionalPartners")]
    public async Task<IActionResult> IndexTransactionalPartner(CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new GetTransactionalPartnersQuery(), cancellationToken);
        return View("TransactionalPartner/Index", AppResponse<IReadOnlyList<TransactionalPartnersResponse>>.Success(result.Value));
    }
    
    [HttpGet("TransactionalPartner")]
    public IActionResult CreateTransactionalPartner()
    {
        ViewBag.Countries = Country.List
            .Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
            .ToList();
        ViewBag.LocationTypes = LocationType.List
            .Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
            .ToList();
        ViewBag.TransactionalPartnerTypes = TransactionalPartnerType.List
            .Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
            .ToList();
        ViewBag.CurrencyTypes = CurrencyType.List
            .Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
            .ToList();
        return View("TransactionalPartner/Create");
    }
    
    [HttpPost("TransactionalPartners")]
    public async Task<IActionResult> CreateTransactionalPartner(CreateTransactionalPartnerCommand request, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(request, cancellationToken);
        
        return result.IsSuccess ? Success() : BadRequest(result.Error);
    }
    
    [HttpGet("TransactionalPartners/{id:guid}")]
    public async Task<IActionResult> GetTransactionalPartner(Guid id, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new GetTransactionalPartnerByIdQuery(id), cancellationToken);
        if (result.IsFailure)
            return BadRequest(result.Error);
        
        ViewBag.Countries = Country.List
            .Select(x => new SelectListItem 
                { 
                    Text = x.Name, 
                    Value = x.Id.ToString(),
                    Selected = x.Id == result.Value.Address.Country.Id
                })
            .ToList();
        ViewBag.LocationTypes = LocationType.List
            .Select(x => new SelectListItem
            {
                Text = x.Name, 
                Value = x.Id.ToString(),
                Selected = x.Id == result.Value.LocationType.Id
            })
            .ToList();
        ViewBag.TransactionalPartnerTypes = TransactionalPartnerType.List
            .Select(x => new SelectListItem
            {
                Text = x.Name, 
                Value = x.Id.ToString(),
                Selected = x.Id == result.Value.TransactionalPartnerType.Id
            })
            .ToList();
        ViewBag.CurrencyTypes = CurrencyType.List
            .Select(x => new SelectListItem
            {
                Text = x.Name, 
                Value = x.Id.ToString(),
                Selected = x.Id == result.Value.CurrencyType.Id
            })
            .ToList();

        return View("TransactionalPartner/Edit", result.Value);
    }

    [HttpPut("TransactionalPartners/{id:guid}")]
    public async Task<IActionResult> UpdateTransactionalPartner(UpdateTransactionalPartnerCommand request,
        CancellationToken cancellationToken)
    {
        var result = await Sender.Send(request, cancellationToken);
        
        return result.IsSuccess ? Success() : BadRequest(result.Error);
    }
    
    [HttpDelete("TransactionalPartners/{id:guid}")]
    public async Task<IActionResult> DeleteTransactionalPartner(Guid id, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new DeleteTransactionalPartnerCommand(id), cancellationToken);
        if (result.IsFailure)
            return BadRequest(result.Error);
        return Success();
    }
}