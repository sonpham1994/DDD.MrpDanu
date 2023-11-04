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
        return Ok(ApiResponse.Success());
    }

    [NonAction]
    protected OkObjectResult Success<T>(T result)
    {
        return Ok(ApiResponse<T>.Success(result));
    }

    [NonAction]
    protected BadRequestObjectResult BadRequest(in DomainError domainError)
    {
        return BadRequest(ApiResponse.Failure(domainError));
    }

    [NonAction]
    protected NotFoundObjectResult NotFound(in DomainError domainError)
    {
        return NotFound(ApiResponse.Failure(domainError));
    }
}
