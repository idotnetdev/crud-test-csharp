using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Infrastructure;
using Application.Models.Customers.Queries;
using Application.Models.Customers.RequestCommands;
using Application.Models.Customers.ResponseModels;
using Application.Models.Pagination;
using Application.Services.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Models.Customers.CommandHandlers;
internal class GetCustomerByIdCommandHandler : IRequestHandler<GetCustomerCommand, CustomerModel>
{
    public async Task<CustomerModel> Handle(GetCustomerCommand request, CancellationToken cancellationToken)
    {
        var customerService = EngineContext.Resolve<ICustomerService>();
        var result = await customerService.GetById(request.Id);
        return result;
    }
}
