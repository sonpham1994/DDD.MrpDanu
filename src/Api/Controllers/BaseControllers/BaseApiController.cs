using Domain.SharedKernel.Base;
using Web.ApiModels.BaseResponses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.BaseControllers;

[ApiController]
public abstract class BaseApiController : ControllerBase
{
    protected ISender Sender;

    protected BaseApiController(ISender sender) => Sender = sender;

    [NonAction]
    protected OkObjectResult Success()
    {
        return Ok(AppResponse.Success());
    }

    [NonAction]
    protected OkObjectResult Success<T>(T result)
    {
        return Ok(AppResponse<T>.Success(result));
    }

    [NonAction]
    protected BadRequestObjectResult BadRequest(in DomainError domainError)
    {
        return BadRequest(AppResponse.Failure(domainError));
    }

    [NonAction]
    protected NotFoundObjectResult NotFound(in DomainError domainError)
    {
        return NotFound(AppResponse.Failure(domainError));
    }
}
