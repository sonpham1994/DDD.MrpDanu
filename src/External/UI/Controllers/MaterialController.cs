using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers;

public sealed partial class MaterialManagementController
{
    [HttpGet("materials")]
    public IActionResult IndexMaterial()
    {
        return View("Material/Index");
    }
    
    [HttpGet("material")]
    public IActionResult CreateMaterial()
    {
        return View("Material/Create");
    }

    [HttpGet("materials/{id:guid}")]
    public IActionResult EditMaterial(Guid id, CancellationToken cancellationToken)
    {
        return View("Material/Edit");
    }
}