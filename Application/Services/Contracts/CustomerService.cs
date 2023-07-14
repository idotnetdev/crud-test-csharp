using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Infrastructure.Exceptions;
using Application.Infrastructure.Extensions;
using Application.Models.Pagination;
using Application.Services.Interfaces;
using Data.Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Application.Models.Customers.ResponseModels;
using Mapster;

namespace Application.Services.Contracts;
public class CustomerService : ICustomerService
{
    private readonly IApplicationDbContext _context;
    private readonly DbSet<Customer> _customer;

    public CustomerService(IApplicationDbContext context)
    {
        _context = context;
        _customer = _context.Set<Customer>();
    }

    public PagedList<Customer> GetCustomersAsPagedList(CustomerListRequestModel searchModel)
    {
        var query = _customer.AsQueryable();

        if (searchModel.Name.HasValue())
            query = query.Where(x => x.Firstname.Contains(searchModel.Name) || x.Lastname.Contains(searchModel.Name));

        if (searchModel.Email.HasValue())
            query = query.Where(x => x.Email.Contains(searchModel.Email));

        if (searchModel.PhoneNumber.HasValue())
            query = query.Where(x => x.PhoneNumber.Contains(searchModel.PhoneNumber));

        if (searchModel.BankAccountNumber.HasValue())
            query = query.Where(x => x.BankAccountNumber.Contains(searchModel.BankAccountNumber));

        if (searchModel.BirthDateFrom.HasValue)
            query = query.Where(x => x.DateOfBirth >= searchModel.BirthDateFrom.Value);

        if (searchModel.BirthDateTo.HasValue)
            query = query.Where(x => x.DateOfBirth <= searchModel.BirthDateTo.Value);

        query = query.OrderBy(searchModel.OrderBy);
        return new PagedList<Customer>(query, searchModel.Page, searchModel.PageSize.Value);
    }

    public virtual async Task Add(Customer customer)
    {
        var birthDate = customer.DateOfBirth.Date;
        var existsByNameAndBirthDate = _customer.Any(x => x.Firstname == customer.Firstname && x.Lastname == customer.Lastname && x.DateOfBirth.Date == birthDate);
        if (existsByNameAndBirthDate)
        {
            throw new DuplicateCustomerException("A customer with the same name and birthdate already exists.");
        }

        var existsByEmail = _customer.Any(x => x.Email == customer.Email);
        if (existsByEmail)
        {
            throw new DuplicateCustomerException("A customer with the same email already exists.");
        }

        _customer.Add(customer);
        await _context.SaveChangesAsync();
    }

    public virtual async Task<CustomerModel> GetById(int id)
    {
        var row = await _customer.Where(x => x.Id == id).ProjectToType<CustomerModel>().FirstOrDefaultAsync();
        return row;
    }

    public virtual async Task Update(Customer customer)
    {
        var row = _customer.Find(customer.Id);
        if (row != null)
        {
            var birthDate = customer.DateOfBirth.Date;
            var existsByNameAndBirthDate = _customer.Any(x => x.Id != customer.Id && x.Firstname == customer.Firstname && x.Lastname == customer.Lastname && x.DateOfBirth.Date == birthDate);
            if (existsByNameAndBirthDate)
            {
                throw new DuplicateCustomerException("A customer with the same name and birthdate already exists.");
            }

            var existsByEmail = _customer.Any(x => x.Id != customer.Id && x.Email == customer.Email);
            if (existsByEmail)
            {
                throw new DuplicateCustomerException("A customer with the same email already exists.");
            }

            _context.Entry(row).CurrentValues.SetValues(customer);
            await _context.SaveChangesAsync();
        }
    }

    public virtual async Task Delete(int id)
    {
        var row = _customer.Find(id);
        if (row != null)
        {
            _customer.Remove(row);
            await _context.SaveChangesAsync();
        }
    }
}
