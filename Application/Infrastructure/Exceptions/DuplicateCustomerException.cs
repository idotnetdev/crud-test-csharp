using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Infrastructure.Exceptions;
public class DuplicateCustomerException : Exception
{
    public DuplicateCustomerException()
    {
    }

    public DuplicateCustomerException(string message)
        : base(message)
    {
    }

    public DuplicateCustomerException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}