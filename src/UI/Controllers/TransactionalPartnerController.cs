using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.MaterialManagement;

public sealed partial class MaterialManagementController
{
    [HttpGet("TransactionalPartners")]
    public async Task<IActionResult> IndexTransactionalPartner(CancellationToken cancellationToken)
    {
        return View("TransactionalPartner/Index");
    }
    
    [HttpGet("TransactionalPartner")]
    public IActionResult CreateTransactionalPartner()
    {
        return View("TransactionalPartner/Create");
    }
    
    [HttpGet("TransactionalPartners/{id:guid}")]
    public async Task<IActionResult> GetTransactionalPartner(Guid id, CancellationToken cancellationToken)
    {
        return View("TransactionalPartner/Edit");
    }
}