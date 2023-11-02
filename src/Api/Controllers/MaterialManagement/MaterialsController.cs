using Api.Controllers.BaseControllers;
using Application.MaterialManagement.MaterialAggregate.Commands.CreateMaterial;
using Application.MaterialManagement.MaterialAggregate.Commands.DeleteMaterial;
using Application.MaterialManagement.MaterialAggregate.Commands.UpdateMaterial;
using Application.MaterialManagement.MaterialAggregate.Queries.MaterialQueries.GetMaterials;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.MaterialManagement;

[Route("material-management/[controller]")]
public sealed class MaterialsController : BaseApiController
{
    public MaterialsController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetMaterials(CancellationToken cancellationToken)
    {
        var materials = await Sender.Send(new GetMaterialsQuery(), cancellationToken);

        return Success(materials.Value);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateMaterialCommand request, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(request, cancellationToken);

        return result.IsSuccess ? Success() : BadRequest(result.Error);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Edit(UpdateMaterialCommand request, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(request, cancellationToken);

        return result.IsSuccess ? Success() : BadRequest(result.Error);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new DeleteMaterialCommand(id), cancellationToken);

        return result.IsSuccess ? Success() : BadRequest(result.Error);
    }
}
