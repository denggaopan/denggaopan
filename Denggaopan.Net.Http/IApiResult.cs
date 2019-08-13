using System;
using System.Collections.Generic;
using System.Text;

namespace Denggaopan.Net.Http
{
    public interface IApiResult<T> where T : class
    {
        int Code { get; set; }
        string Message { get; set; }
        T Data { get; set; }
    }
    public class ApiResult : IApiResult<object>
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }

    public class ApiResult<T> : IApiResult<T> where T : class
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
