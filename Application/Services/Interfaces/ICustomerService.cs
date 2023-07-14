using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.Customers.ResponseModels;
using Application.Models.Pagination;
using Domain.Entities;

namespace Application.Services.Interfaces;

public interface ICustomerService
{
    PagedList<Customer> GetCustomersAsPagedList(CustomerListRequestModel searchModel);
    Task Add(Customer customer);
    Task<CustomerModel> GetById(int id);
    Task Update(Customer customer);
    Task Delete(int id);
}
