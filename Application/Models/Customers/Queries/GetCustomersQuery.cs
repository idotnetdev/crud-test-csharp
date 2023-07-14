using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.Customers.ResponseModels;
using Application.Models.Pagination;
using Domain.Entities;
using MediatR;

namespace Application.Models.Customers.Queries;
public class GetCustomersQuery : CustomerListRequestModel, IRequest<PagedList<Customer>>
{
}
