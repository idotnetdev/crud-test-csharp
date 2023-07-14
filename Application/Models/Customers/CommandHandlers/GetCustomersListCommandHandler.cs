using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Infrastructure;
using Application.Models.Customers.Queries;
using Application.Models.Customers.RequestCommands;
using Application.Models.Pagination;
using Application.Services.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Models.Customers.CommandHandlers;
public class GetCustomersListCommandHandler : IRequestHandler<GetCustomersQuery, PagedList<Customer>>
{
    public Task<PagedList<Customer>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
    {
        var customerService = EngineContext.Resolve<ICustomerService>();
        var result =  customerService.GetCustomersAsPagedList(request);
        return Task.FromResult(result);
    }
}
