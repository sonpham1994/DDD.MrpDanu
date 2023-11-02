using Domain.SharedKernel.Base;
using Web.ApiModels.BaseResponses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.BaseControllers;

[ApiController]
public abstract class BaseApiController : ControllerBase
{
    protected ISender Sender;

    public BaseApiController(ISender sender) => Sender = sender;

    [NonAction]
    public OkObjectResult Success()
    {
        return Ok(AppResponse.Success());
    }

    [NonAction]
    public OkObjectResult Success<T>(T result)
    {
        return Ok(AppResponse<T>.Success(result));
    }

    [NonAction]
    public BadRequestObjectResult BadRequest(in DomainError domainError)
    {
        return BadRequest(AppResponse.Failure(domainError));
    }

    [NonAction]
    public NotFoundObjectResult NotFound(in DomainError domainError)
    {
        return NotFound(AppResponse.Failure(domainError));
    }
}
