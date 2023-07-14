using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Infrastructure.Constants;
using MediatR;

namespace Application.Models.Customers.RequestCommands;
public class CreateOrUpdateCustomerCommand : IRequest<ApiResult<object>>
{
    public int? Id { get; set; }

    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string BankAccountNumber { get; set; }
}