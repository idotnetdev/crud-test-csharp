﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Customer : BaseAuditedEntity
{
    public string Firstname { get; set; }

    public string Lastname { get; set; }

    public DateTime DateOfBirth { get; set; }

    public string PhoneNumber { get; set; }

    public string Email { get; set; }

    public string BankAccountNumber { get; set; }

    [NotMapped]
    public string FullName
    {
        get
        {
            return $"{Firstname} {Lastname}";
        }
    }

    public override string ToString()
    {
        return $"{FullName} => {Email}, {PhoneNumber}";
    }
}
