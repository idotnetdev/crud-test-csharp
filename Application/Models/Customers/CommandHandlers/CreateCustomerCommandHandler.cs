using Application.Infrastructure;
using Application.Models.Customers.RequestCommands;
using Application.Models.Customers.Validations;
using Application.Services.Interfaces;
using Domain.Entities;
using FluentValidation;
using Mapster;
using MediatR;

namespace Application.Models.Customers.CommandHandlers;

public class CreateCustomerCommandHandler : IRequestHandler<CreateOrUpdateCustomerCommand, string>
{
    public async Task<string> Handle(CreateOrUpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var validator = EngineContext.Resolve<IValidator<CreateOrUpdateCustomerCommand>>();
        var result = validator.Validate(request);
        var customerService = EngineContext.Resolve<ICustomerService>();

        await customerService.Add(request.Adapt<Customer>());
        return "";
    }
}

