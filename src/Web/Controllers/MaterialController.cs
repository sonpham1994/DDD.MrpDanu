using Application.MaterialManagement.MaterialAggregate.Commands.CreateMaterial;
using Application.MaterialManagement.MaterialAggregate.Commands.DeleteMaterial;
using Application.MaterialManagement.MaterialAggregate.Commands.UpdateMaterial;
using Application.MaterialManagement.MaterialAggregate.Queries.MaterialQueries.GetMaterialById;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.MaterialManagement;

public sealed partial class MaterialManagementController
{
    [HttpGet("materials")]
    public async Task<IActionResult> IndexMaterial()
    {
        return View("Material/Index");
    }
    
    [HttpGet("material")]
    public async Task<IActionResult> CreateMaterial()
    {
        return View("Material/Create");
    }

    [HttpGet("materials/{id:guid}")]
    public async Task<IActionResult> EditMaterial(Guid id, CancellationToken cancellationToken)
    {
        return View("Material/Edit");
    }
}