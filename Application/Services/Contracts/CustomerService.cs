using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Infrastructure.Exceptions;
using Application.Services.Interfaces;
using Data.Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

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
}
