using Api.Controllers.BaseControllers;
using Application.SupplyChainManagement.MaterialAggregate.Commands.CreateMaterial;
using Application.SupplyChainManagement.MaterialAggregate.Commands.DeleteMaterial;
using Application.SupplyChainManagement.MaterialAggregate.Commands.UpdateMaterial;
using Application.SupplyChainManagement.MaterialAggregate.Queries.GetMaterialById;
using Application.SupplyChainManagement.MaterialAggregate.Queries.GetMaterials;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.SupplyChainManagement;

[Route("material-management/[controller]")]
public sealed class MaterialsController : BaseApiController
{
    public MaterialsController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetMaterialsAsync(CancellationToken cancellationToken)
    {
        var materials = await Sender.Send(new GetMaterialsQuery(), cancellationToken);
        
        return Success(materials.Value);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetMaterialByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var materialResult = await Sender.Send(new GetMaterialByIdQuery(id), cancellationToken);
        
        return materialResult.IsSuccess ? Success(materialResult.Value) : BadRequest(materialResult.Error);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateMaterialCommand request, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(request, cancellationToken);

        return result.IsSuccess ? Success() : BadRequest(result.Error);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(UpdateMaterialCommand request, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(request, cancellationToken);

        return result.IsSuccess ? Success() : BadRequest(result.Error);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new DeleteMaterialCommand(id), cancellationToken);

        return result.IsSuccess ? Success() : BadRequest(result.Error);
    }
}
