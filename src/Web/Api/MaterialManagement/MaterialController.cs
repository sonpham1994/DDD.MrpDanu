using Application.MaterialManagement.MaterialAggregate.Queries.MaterialQueries.GetMaterials;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Api.BaseControllers;

namespace Web.Api.MaterialManagement;

[Route("api/material-management")]
public sealed class MaterialController : BaseApiController
{
    public MaterialController(ISender sender) : base(sender)
    {
    }

    [HttpGet("materials")]
    public async Task<IActionResult> GetMaterials(CancellationToken cancellationToken)
    {
        var materials = await Sender.Send(new GetMaterialsQuery(), cancellationToken);

        return Success(materials.Value);
    }
}
