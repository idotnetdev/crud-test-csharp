using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.EntityConfigurations;
public class CustomerConfig : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasIndex(x => new { x.Firstname, x.Lastname, x.DateOfBirth }).IsUnique();

        builder.HasIndex(x=> x.Email).IsUnique();

        builder.Property(x => x.Firstname).IsRequired().HasMaxLength(30);
        builder.Property(x => x.Lastname).IsRequired().HasMaxLength(30);
        builder.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(13).IsUnicode(false);
        builder.Property(x => x.Email).IsRequired().HasMaxLength(50).IsUnicode(false);
        builder.Property(x => x.BankAccountNumber).IsRequired().HasMaxLength(26).IsUnicode(false);
    }
}
