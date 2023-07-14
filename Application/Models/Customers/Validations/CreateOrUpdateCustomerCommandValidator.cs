using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Infrastructure.Constants;
using Application.Models.Customers.RequestCommands;
using FluentValidation;

namespace Application.Models.Customers.Validations;
public class CreateOrUpdateCustomerCommandValidator : AbstractValidator<CreateOrUpdateCustomerCommand>
{
    public CreateOrUpdateCustomerCommandValidator()
    {
        RuleFor(x => x.Firstname)
            .NotEmpty().WithMessage("Please enter First name.")
            .Length(3, 30).WithMessage("First name must be between 3 and 30 characters.");

        RuleFor(x => x.Lastname)
            .NotEmpty().WithMessage("Please enter Last name.")
            .Length(3, 30).WithMessage("Last name must be between 3 and 30 characters.");

        RuleFor(x => x.DateOfBirth)
            .NotEmpty().WithMessage("Please enter Birth date.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Please enter Phone number.")
            .Matches(RegexPatterns.NumbersOnly).WithMessage("Please enter a valid phone number.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Please enter Email.")
            .Matches(RegexPatterns.Email).WithMessage("Please enter a valid email address.");

        RuleFor(x => x.BankAccountNumber)
            .NotEmpty().WithMessage("Please enter Bank account number.")
            .Matches(RegexPatterns.IranIban).WithMessage("Please enter a valid bank account number.");
    }
}

