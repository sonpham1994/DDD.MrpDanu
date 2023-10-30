using Domain.SharedKernel.Base;
using Web.ApiModels.BaseResponses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.BaseControllers;

[ApiController]
[Route("api/")]
public abstract class BaseApiController : ControllerBase
{
    protected ISender Sender;

    public BaseApiController(ISender sender) => Sender = sender;

    public OkObjectResult Success()
    {
        return Ok(AppResponse.Success());
    }

    public OkObjectResult Success<T>(T result)
    {
        return Ok(AppResponse<T>.Success(result));
    }

    public BadRequestObjectResult BadRequest(in DomainError domainError)
    {
        return BadRequest(AppResponse.Error(domainError));
    }

    public NotFoundObjectResult NotFound(in DomainError domainError)
    {
        return NotFound(AppResponse.Error(domainError));
    }
}
