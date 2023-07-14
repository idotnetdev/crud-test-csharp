using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Infrastructure;
using Application.Models.Customers.RequestCommands;
using Application.Services.Interfaces;
using MediatR;

namespace Application.Models.Customers.CommandHandlers;
public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, ApiResult<object>>
{
    public async Task<ApiResult<object>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var customerService = EngineContext.Resolve<ICustomerService>();
        await customerService.Delete(request.Id);
        return new ApiResult<object>($"Record {request.Id} has been deleted successfully.");
    }
}
