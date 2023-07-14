using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Services.Interfaces;

public interface ICustomerService
{
    Task Add(Customer customer);
}
