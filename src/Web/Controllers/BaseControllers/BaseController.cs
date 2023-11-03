using Domain.SharedKernel.Base;
using Web.ApiModels.BaseResponses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.BaseControllers;

//[ApiController]
public abstract class BaseController : Controller
{
    // protected ISender Sender;
    //
    // public BaseController(ISender sender) => Sender = sender;
    //
    // public OkObjectResult Success()
    // {
    //     return Ok(AppResponse.Success());
    // }
    //
    // public OkObjectResult Success<T>(T result)
    // {
    //     return Ok(AppResponse<T>.Success(result));
    // }
    //
    // public BadRequestObjectResult BadRequest(in DomainError domainError)
    // {
    //     return BadRequest(AppResponse.Failure(domainError));
    // }
    //
    // public NotFoundObjectResult NotFound(in DomainError domainError)
    // {
    //     return NotFound(AppResponse.Failure(domainError));
    // }
}
