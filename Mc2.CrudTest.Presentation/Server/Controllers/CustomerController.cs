using Application.Infrastructure.Exceptions;
using Application.Infrastructure.Extensions;
using Application.Models.Customers.Queries;
using Application.Models.Customers.RequestCommands;
using Application.Models.Customers.ResponseModels;
using FluentValidation;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mc2.CrudTest.Presentation.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController : ApiControllerBase
{
    private readonly IValidator<CreateOrUpdateCustomerCommand> _validator;

    public CustomerController(IValidator<CreateOrUpdateCustomerCommand> validator)
    {
        _validator = validator;
    }

    [HttpGet("GetList")]
    public virtual async Task<IActionResult> GetList([FromQuery] CustomerListRequestModel searchModel)
    {
        var result = await Mediator.Send(searchModel.Adapt<GetCustomersQuery>());
        return OkResult(result);
    }

    [HttpPost("Add")]
    public virtual async Task<IActionResult> Add(CreateOrUpdateCustomerCommand command)
    {
        var validationResult = _validator.Validate(command);
        if (!validationResult.IsValid)
        {
            return BadResult(validationResult.Errors.AsString());
        }

        try
        {
            return OkResult(await Mediator.Send(command));
        }
        catch (DuplicateCustomerException ex)
        {
            return BadResult(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public virtual async Task<IActionResult> Get([FromRoute] int id)
    {
        var result = await Mediator.Send(new GetCustomerCommand { Id = id });
        return OkResult(result);
    }

    [HttpPut("Update")]
    public virtual async Task<IActionResult> Update(CreateOrUpdateCustomerCommand command)
    {
        var validationResult = _validator.Validate(command);
        if (!validationResult.IsValid)
        {
            return BadResult(validationResult.Errors.AsString());
        }

        try
        {
            return OkResult(await Mediator.Send(command));
        }
        catch (DuplicateCustomerException ex)
        {
            return BadResult(ex.Message);
        }
    }

    [HttpDelete("Delete")]
    public virtual async Task<IActionResult> Delete(int id)
    {
        try
        {
            return OkResult(await Mediator.Send(new DeleteCustomerCommand { Id = id }));
        }
        catch (DuplicateCustomerException ex)
        {
            return BadResult(ex.Message);
        }
    }
}
