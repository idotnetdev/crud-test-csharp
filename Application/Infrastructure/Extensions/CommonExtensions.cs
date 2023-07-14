using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Infrastructure.Extensions;
public static class CommonExtensions
{
    public static bool HasValue(this string value)
    {
        return !string.IsNullOrWhiteSpace(value);
    }
    public static string AsString(this List<FluentValidation.Results.ValidationFailure> errors)
    {
        return string.Join("\n", errors.Select(x => x.ErrorMessage).ToArray());
    }
}
