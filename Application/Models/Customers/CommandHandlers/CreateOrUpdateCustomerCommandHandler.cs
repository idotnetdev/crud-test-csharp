using Application.Infrastructure;
using Application.Infrastructure.Extensions;
using Application.Models.Customers.RequestCommands;
using Application.Models.Customers.Validations;
using Application.Services.Interfaces;
using Domain.Entities;
using FluentValidation;
using Mapster;
using MediatR;

namespace Application.Models.Customers.CommandHandlers;

public class CreateOrUpdateCustomerCommandHandler : IRequestHandler<CreateOrUpdateCustomerCommand, ApiResult<object>>
{
    public async Task<ApiResult<object>> Handle(CreateOrUpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var validator = EngineContext.Resolve<IValidator<CreateOrUpdateCustomerCommand>>();
        var result = validator.Validate(request);
        if (!result.IsValid)
        {
            return new ApiResult<object>(null, 400, result.Errors.AsString());
        }

        var customerService = EngineContext.Resolve<ICustomerService>();

        var row = request.Adapt<Customer>();

        if (request.Id.HasValue && request.Id.Value > 0)
            await customerService.Update(row);
        else
            await customerService.Add(row);

        return new ApiResult<object>(row.Id);
    }
}

