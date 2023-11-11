using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers;

public sealed partial class MaterialManagementController
{
    [HttpGet("TransactionalPartners")]
    public IActionResult IndexTransactionalPartner(CancellationToken cancellationToken)
    {
        return View("TransactionalPartner/Index");
    }
    
    [HttpGet("TransactionalPartner")]
    public IActionResult CreateTransactionalPartner()
    {
        return View("TransactionalPartner/Create");
    }
    
    [HttpGet("TransactionalPartners/{id:guid}")]
    public IActionResult GetTransactionalPartner(Guid id, CancellationToken cancellationToken)
    {
        return View("TransactionalPartner/Edit");
    }
}