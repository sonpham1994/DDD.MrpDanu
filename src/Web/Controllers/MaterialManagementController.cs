using Web.Controllers.BaseControllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.MaterialManagement;

[Route("[controller]")]
public sealed partial class MaterialManagementController : BaseController
{
    public MaterialManagementController(ISender sender) : base(sender)
    {
    }
}