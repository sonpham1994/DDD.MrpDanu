using Api.ApiResponses;
using Domain.SharedKernel.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.BaseControllers;

[ApiController]
public abstract class BaseApiController : ControllerBase
{
    protected readonly ISender Sender;

    protected BaseApiController(ISender sender) => Sender = sender;

    [NonAction]
    protected OkObjectResult Success()
    {
        return Ok(ApiResponseExtensions.Success());
    }

    [NonAction]
    protected OkObjectResult Success<T>(T result)
    {
        return Ok(ApiResponseExtensions<T>.Success(result));
    }

    [NonAction]
    protected BadRequestObjectResult BadRequest(in DomainError domainError)
    {
        return BadRequest(ApiResponseExtensions.Failure(domainError));
    }

    [NonAction]
    protected NotFoundObjectResult NotFound(in DomainError domainError)
    {
        return NotFound(ApiResponseExtensions.Failure(domainError));
    }
}
