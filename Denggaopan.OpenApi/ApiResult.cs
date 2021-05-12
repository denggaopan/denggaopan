using System;
using System.Collections.Generic;
using System.Text;

namespace Denggaopan.OpenApi
{

    public class ApiResult
    {
        public ApiResult() { }

        public ApiResult(ApiResultCode code, string msg)
        {
            Code = code;
            Message = msg;
        }

        public ApiResultCode Code { get; set; } = ApiResultCode.Success;
        public string Message { get; set; } = string.Empty;
    }

    public class ApiResult<T> : ApiResult
    {
        public ApiResult() : base() { }

        public ApiResult(ApiResultCode code, string message) : base(code, message)
        {
        }

        public ApiResult(T data)
        {
            Data = data;
        }

        public T Data { get; set; }
    }
}
