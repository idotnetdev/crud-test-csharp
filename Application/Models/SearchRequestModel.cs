using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models;
public class SearchRequestModel
{
    public int? PageSize { get; set; } = 15;
    public int Page { get; set; } = 1;
    public string Search { get; set; }
    public string OrderBy { get; set; } = "Id Desc";
}
