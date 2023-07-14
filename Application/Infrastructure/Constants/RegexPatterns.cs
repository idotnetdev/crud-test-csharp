using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Infrastructure.Constants;
public class RegexPatterns
{
    public const string Email = @"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})$";
    public const string NumbersOnlyFromOne = "^[1-9][0-9]*$";
    public const string NumbersOnly = "^[0-9]*$";
    public const string NumbersWithDash = @"^[0-9\-]*$";
    public const string NumbersAndNegativeOnly = @"^-?[0-9]\d*(\.\d+)?$";
    public const string TwoDecimalPlaces = @"((\d+)((\.\d{1,2})?))$";
    public const string IranIban = "^(?i:IR)(?=.{24}$)[0-9]*$";
    public const string AlphaNumeric = "^[0-9A-Za-z]*$";
}