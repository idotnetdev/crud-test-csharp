using Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Mc2.CrudTest.Presentation.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    private ISender? _mediator;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

    protected IActionResult OkResult<T>(T result)
    {
        return Ok(new ApiResult<T>(result));
    }

    protected IActionResult OkResult<T>(T result, string message)
    {
        return Ok(new ApiResult<T>(result, 200, message));
    }

    protected IActionResult BadResult(string message)
    {
        //return BadRequest()
        return BadRequest(new ApiResult<object>(null, 400, message));
    }

    protected IActionResult BadResult<T>(T result, string message)
    {
        return BadRequest(new ApiResult<T>(result, 400, message));
    }

    protected IActionResult ServerErrorResult<T>(T result)
    {
        return BadRequest(new ApiResult<T>(result, 500, "Internal server error was occured."));
    }
}
