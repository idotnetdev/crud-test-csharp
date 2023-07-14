using Application.Infrastructure.Exceptions;
using Application.Models.Customers.RequestCommands;
using FluentValidation;
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

    [HttpPost("Add")]
    public virtual async Task<ActionResult<string>> Add(CreateOrUpdateCustomerCommand command)
    {
        var validationResult = _validator.Validate(command);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        try
        {
            return await Mediator.Send(command);
        }
        catch (DuplicateCustomerException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
