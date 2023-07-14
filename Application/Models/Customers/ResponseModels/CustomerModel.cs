using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Customers.ResponseModels;
public class CustomerModel
{
    public int Id { get; set; }
    public string Firstname { get; set; }

    public string Lastname { get; set; }

    public DateTime DateOfBirth { get; set; }

    public string PhoneNumber { get; set; }

    public string Email { get; set; }

    public string BankAccountNumber { get; set; }

    public string FullName { get; set; }
}
