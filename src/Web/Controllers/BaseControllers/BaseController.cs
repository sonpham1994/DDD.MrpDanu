using Domain.SharedKernel.Base;
using Web.ApiModels.BaseResponses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.BaseControllers;

//[ApiController]
public abstract class BaseController : Controller
{
    protected ISender Sender;

    public BaseController(ISender sender) => Sender = sender;

    public OkObjectResult Success()
    {
        return Ok(AppResponse.Success());
    }

    public OkObjectResult Success(object result)
    {
        return Ok(AppResponse.Success(result));
    }

    public BadRequestObjectResult BadRequest(DomainError domainError)
    {
        return BadRequest(AppResponse.Error(domainError));
    }

    public NotFoundObjectResult NotFound(DomainError domainError)
    {
        return NotFound(AppResponse.Error(domainError));
    }
}
