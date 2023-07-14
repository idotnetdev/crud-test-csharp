using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Infrastructure;
using Application.Infrastructure.Exceptions;
using Application.Models.Customers.CommandHandlers;
using Application.Models.Customers.RequestCommands;
using Application.Models.Customers.Validations;
using Application.Services.Contracts;
using Application.Services.Interfaces;
using Data.Context;
using Domain.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Shouldly;

namespace UnitTests;
public class AddCustomerHandlerTests
{
    [Fact]
    public async Task Add_WhenBankAccountNumberInvalid_ShouldBeFalse()
    {
        var command = new CreateOrUpdateCustomerCommand
        {
            Firstname = "Hamid",
            Lastname = "Hossein vand",
            DateOfBirth = new DateTime(1991, 5, 27),
            PhoneNumber = "09149190764",
            Email = "idotnetdev@gmail.com",
            BankAccountNumber = "IR12345678901234567890123"
        };

        var validation = await new CreateOrUpdateCustomerCommandValidator().ValidateAsync(command);
        validation.IsValid.ShouldBeFalse(); //Becasue of Bank account number length lower than 24
    }

    [Fact]
    public async Task Add_WhenEmailIsInvalid_ShouldBeFalse()
    {
        var command = new CreateOrUpdateCustomerCommand
        {
            Firstname = "Hamid",
            Lastname = "Hossein vand",
            DateOfBirth = new DateTime(1991, 5, 27),
            PhoneNumber = "09149190764",
            Email = "123gmail.com",
            BankAccountNumber = "IR123456789012345678901234"
        };

        var validation = await new CreateOrUpdateCustomerCommandValidator().ValidateAsync(command);
        validation.IsValid.ShouldBeFalse();
    }

    [Fact]
    public async Task Add_WhenPhoneNumberIsInvalid_ShouldBeFalse()
    {
        var command = new CreateOrUpdateCustomerCommand
        {
            Firstname = "Hamid",
            Lastname = "Hossein vand",
            DateOfBirth = new DateTime(1991, 5, 27),
            PhoneNumber = "",
            Email = "idotnetdev@gmail.com",
            BankAccountNumber = "IR123456789012345678901234"
        };

        var validation = await new CreateOrUpdateCustomerCommandValidator().ValidateAsync(command);
        validation.IsValid.ShouldBeFalse(); //Becasue of Bank account number length lower than 24
    }

    [Fact]
    public async Task Add_InvalidCommand_ReturnsBadRequestResult()
    {
        // Arrange
        var customerServiceMock = new Mock<ICustomerService>();
        var validatorMock = new Mock<IValidator<CreateOrUpdateCustomerCommand>>();

        var services = new ServiceCollection();

        services.AddSingleton(validatorMock.Object);
        services.AddSingleton(customerServiceMock.Object);

        EngineContext.Initialize(services.BuildServiceProvider());

        var handler = new CreateOrUpdateCustomerCommandHandler();

        var command = new CreateOrUpdateCustomerCommand
        {
            Firstname = "Hamid",
            Lastname = "Hossein vand",
            DateOfBirth = new DateTime(1991, 5, 28),
            PhoneNumber = "09149190764",
            Email = "idotnetdev@gmail.com",
            BankAccountNumber = "IR123456789012345678901234"
        };

        var validationResult = new ValidationResult();
        validationResult.Errors.Add(new ValidationFailure("PropertyName", "Error message"));
        validatorMock.Setup(v => v.Validate(command)).Returns(validationResult);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        result.Message.ShouldNotBeNull();
        result.Code.ShouldBe(400);

        customerServiceMock.Verify(cs => cs.Add(It.IsAny<Customer>()), Times.Never);
        customerServiceMock.Verify(cs => cs.Update(It.IsAny<Customer>()), Times.Never);
    }

    [Fact]
    public async Task Add_InvalidCommand_ReturnsOkResult()
    {
        // Arrange
        var customerServiceMock = new Mock<ICustomerService>();
        var validatorMock = new Mock<IValidator<CreateOrUpdateCustomerCommand>>();

        var validationResult = new ValidationResult();
        validationResult.Errors.Add(new ValidationFailure("PropertyName", "Error message"));

        var services = new ServiceCollection();

        services.AddSingleton(validatorMock.Object);
        services.AddSingleton(customerServiceMock.Object);

        EngineContext.Initialize(services.BuildServiceProvider());

        var handler = new CreateOrUpdateCustomerCommandHandler();

        var command = new CreateOrUpdateCustomerCommand
        {
            Firstname = "Radin",
            Lastname = "Hossein vand",
            DateOfBirth = new DateTime(1991, 5, 28),
            PhoneNumber = "09149190761",
            Email = "test@gmail.com",
            BankAccountNumber = "IR123456789012345678901234"
        };
        validatorMock.Setup(v => v.Validate(command)).Returns(validationResult);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        result.IsSucceded.ShouldBeTrue();
        result.Code.ShouldBe(200);

        customerServiceMock.Verify(cs => cs.Add(It.IsAny<Customer>()), Times.Once);
        customerServiceMock.Verify(cs => cs.Update(It.IsAny<Customer>()), Times.Never);
    }
}
