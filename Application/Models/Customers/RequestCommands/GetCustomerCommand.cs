using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.Customers.ResponseModels;
using MediatR;

namespace Application.Models.Customers.RequestCommands;
public class GetCustomerCommand : IRequest<CustomerModel>
{
    public int Id { get; set; }
}
