using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.Models.Customers.RequestCommands;
public class DeleteCustomerCommand : IRequest<ApiResult<object>>
{
    public int Id { get; set; }
}
