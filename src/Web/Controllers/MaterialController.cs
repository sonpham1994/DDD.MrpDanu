using Application.MaterialManagement.MaterialAggregate.Commands.CreateMaterial;
using Application.MaterialManagement.MaterialAggregate.Commands.DeleteMaterial;
using Application.MaterialManagement.MaterialAggregate.Commands.UpdateMaterial;
using Application.MaterialManagement.MaterialAggregate.Queries.MaterialQueries.GetMaterialById;
using Application.MaterialManagement.MaterialAggregate.Queries.MaterialQueries.GetMaterials;
using Application.MaterialManagement.TransactionalPartnerAggregate.Queries.GetSuppliers;
using Domain.MaterialManagement.MaterialAggregate;
using Web.ApiModels.BaseResponses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Controllers.MaterialManagement;

public sealed partial class MaterialManagementController
{
    [HttpGet("materials")]
    public async Task<IActionResult> IndexMaterial(CancellationToken cancellationToken)
    {
        var materials = await Sender.Send( new GetMaterialsQuery(), cancellationToken);
        return View("Material/Index", AppResponse<IReadOnlyList<MaterialsResponse>>.Success(materials.Value));
    }
    
    [HttpGet("material")]
    public async Task<IActionResult> CreateMaterial(CancellationToken cancellationToken)
    {
        var suppliers = await Sender.Send(new GetSuppliersQuery(), cancellationToken);
        ViewBag.MaterialTypes = MaterialType.List
            .Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
            .ToList();
        
        ViewBag.RegionalMarkets = RegionalMarket.List
            .Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
            .ToList();
        
        ViewBag.Suppliers = suppliers.Value;
        
        return View("Material/Create");
    }
    
    [HttpPost("material")]
    public async Task<IActionResult> CreateMaterial(CreateMaterialCommand request, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(request, cancellationToken);

        return result.IsSuccess ? Success() : BadRequest(result.Error);
    }

    [HttpGet("materials/{id:guid}")]
    public async Task<IActionResult> GetMaterial(Guid id, CancellationToken cancellationToken)
    {
        var materialResult = await Sender.Send(new GetMaterialByIdQuery(id), cancellationToken);
        if (materialResult.IsFailure)
            return BadRequest(materialResult.Error);

        var suppliers = await Sender.Send(new GetSuppliersQuery(), cancellationToken);
        var material = materialResult.Value;
        
        ViewBag.MaterialTypes = MaterialType.List
            .Select(x => new SelectListItem
            {
                Text = x.Name, 
                Value = x.Id.ToString(),
                Selected = material.MaterialType.Id == x.Id
            })
            .ToList();
        
        ViewBag.RegionalMarkets = RegionalMarket.List
            .Select(x => new SelectListItem 
                { 
                    Text = x.Name, 
                    Value = x.Id.ToString(), 
                    Selected = material.RegionalMarket.Id == x.Id
                    
                })
            .ToList();
        
        ViewBag.Suppliers = suppliers.Value;
        
        return View("Material/Edit", material);
    }
    
    [HttpPut("materials/{id:guid}")]
    public async Task<IActionResult> EditMaterial(UpdateMaterialCommand request, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(request, cancellationToken);

        return result.IsSuccess ? Success() : BadRequest(result.Error);
    }
    
    [HttpDelete("materials/{id:guid}")]
    public async Task<IActionResult> DeleteMaterial(Guid id, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new DeleteMaterialCommand(id), cancellationToken);

        return result.IsSuccess ? Success() : BadRequest(result.Error);
    }
}