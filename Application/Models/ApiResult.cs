using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models;
public class ApiResult<T>
{
    public ApiResult(T result)
    {
        Result = result;
        Code = 200;
    }

    public ApiResult(T result, int code)
    {
        Result = result;
        Code = code;
    }

    public ApiResult(T result, int code, string message)
    {
        Result = result;
        Code = code;
        Message = message;
    }

    public bool IsSucceded
    {
        get
        {
            bool result = Code switch
            {
                _ when Code == 200 => true,
                _ when Code == 400 || Code == 500 => false,
                _ => false,
            };
            return result;
        }
    }

    public int Code { get; set; }
    public string Message { get; set; }
    public T Result { get; set; }

    public override string ToString()
    {
        return Newtonsoft.Json.JsonConvert.SerializeObject(this);
    }
}

public class ResponseModel
{

}