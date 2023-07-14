using System.Collections.Generic;
using Application.Infrastructure.Exceptions;
using Application.Services.Contracts;
using Data.Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace UnitTests;

public class AddCustomerServiceTests
{
    [Fact]
    public async Task Add_ValidCustomer_Success()
    {
        // Arrange
        var dbContextMock = new Mock<IApplicationDbContext>();
        var customerDbSetMock = GetCustomerDbSetMock();

        dbContextMock.Setup(c => c.Set<Customer>()).Returns(customerDbSetMock.Object);
        var customerService = new CustomerService(dbContextMock.Object);

        var customer = new Customer
        {
            Firstname = "Hamid",
            Lastname = "Hossein vand",
            DateOfBirth = new DateTime(1991, 5, 27),
            Email = "idotnetdev@gmail.com"
        };

        // Act
        await customerService.Add(customer);
    }

    [Fact]
    public async Task Add_DuplicateNameAndBirthDate_ThrowsException()
    {
        // Arrange
        var dbContextMock = new Mock<IApplicationDbContext>();
        var customerDbSetMock = GetCustomerDbSetMock();

        dbContextMock.Setup(c => c.Set<Customer>()).Returns(customerDbSetMock.Object);

        var customerService = new CustomerService(dbContextMock.Object);

        var customer = new Customer
        {
            Firstname = "Hamid",
            Lastname = "Hossein vand",
            DateOfBirth = new DateTime(1990, 1, 1),
            Email = "idotnetdev@gmail.com"
        };

        // Act and Assert
        await Assert.ThrowsAsync<DuplicateCustomerException>(() => customerService.Add(customer));
    }

    [Fact]
    public async Task Add_DuplicateNameAndBirthDate2_ThrowsException()
    {
        // Arrange
        var dbContextMock = new Mock<IApplicationDbContext>();
        var customerDbSetMock = GetCustomerDbSetMock();

        dbContextMock.Setup(c => c.Set<Customer>()).Returns(customerDbSetMock.Object);

        var customerService = new CustomerService(dbContextMock.Object);

        var customer = new Customer
        {
            Firstname = "Hamid",
            Lastname = "Hossein vand",
            DateOfBirth = new DateTime(1990, 1, 1),
            Email = "idotnetdev@gmail.com"
        };

        // Act and Assert
        await Assert.ThrowsAsync<DuplicateCustomerException>(() => customerService.Add(customer));
    }

    [Fact]
    public async Task Add_DuplicateEmail_ThrowsException()
    {
        // Arrange
        var dbContextMock = new Mock<IApplicationDbContext>();
        var customerDbSetMock = GetCustomerDbSetMock();

        dbContextMock.Setup(c => c.Set<Customer>()).Returns(customerDbSetMock.Object);
        var customerService = new CustomerService(dbContextMock.Object);

        var customer = new Customer
        {
            Firstname = "Hamid",
            Lastname = "Hossein vand",
            DateOfBirth = new DateTime(1990, 1, 1),
            Email = "idotnetdev@gmail.com"
        };

        // Act and Assert
        await Assert.ThrowsAsync<DuplicateCustomerException>(() => customerService.Add(customer));
    }

    private Mock<DbSet<Customer>> GetCustomerDbSetMock()
    {
        var customerDbSetMock = new Mock<DbSet<Customer>>();

        customerDbSetMock.As<IQueryable<Customer>>().Setup(m => m.Provider).Returns(GetTestCustomers().Provider);
        customerDbSetMock.As<IQueryable<Customer>>().Setup(m => m.Expression).Returns(GetTestCustomers().Expression);
        customerDbSetMock.As<IQueryable<Customer>>().Setup(m => m.ElementType).Returns(GetTestCustomers().ElementType);
        customerDbSetMock.As<IQueryable<Customer>>().Setup(m => m.GetEnumerator()).Returns(GetTestCustomers().GetEnumerator());

        return customerDbSetMock;
    }

    private IQueryable<Customer> GetTestCustomers()
    {
        var customers = new List<Customer>
            {
                new Customer
                {
                    Firstname = "Hamid",
                    Lastname = "Hossein vand",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    Email = "idotnetdev@gmail.com"
                }
            };
        return customers.AsQueryable();
    }
}