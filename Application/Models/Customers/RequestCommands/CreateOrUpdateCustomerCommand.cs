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
public class CreateOrUpdateCustomerCommand : IRequest<string>
{
    public int? Id { get; set; }

    [DisplayName("First name")]
    [Required(ErrorMessage = "Please enter {0}.", AllowEmptyStrings = false)]
    [StringLength(30, MinimumLength = 3, ErrorMessage = "{0} must be between {2} and {1} charachters.")]
    public string Firstname { get; set; }

    [DisplayName("Last name")]
    [Required(ErrorMessage = "Please enter {0}.", AllowEmptyStrings = false)]
    [StringLength(30, MinimumLength = 3, ErrorMessage = "{0} must be between {2} and {1} charachters.")]
    public string Lastname { get; set; }

    [DisplayName("Birth date")]
    [Required(ErrorMessage = "Please enter {0}.", AllowEmptyStrings = false)]
    public DateTime DateOfBirth { get; set; }

    [DisplayName("Phone number")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter {0}.")]
    [RegularExpression(RegexPatterns.NumbersOnly, ErrorMessage = "Please enter a valid phone number.")]
    public string PhoneNumber { get; set; }

    [DisplayName("Email")]
    [Required(ErrorMessage = "Please enter {0}.", AllowEmptyStrings = false)]
    [RegularExpression(RegexPatterns.Email, ErrorMessage = "Please enter a valid email address.")]
    public string Email { get; set; }

    [DisplayName("Bank account number")]
    [Required(ErrorMessage = "Please enter {0}.", AllowEmptyStrings = false)]
    [RegularExpression(RegexPatterns.IranIban, ErrorMessage = "Please enter a valid bank account number.")]
    public string BankAccountNumber { get; set; }
}