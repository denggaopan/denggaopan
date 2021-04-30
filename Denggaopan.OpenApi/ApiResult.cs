using System;
using System.Collections.Generic;
using System.Text;

namespace Denggaopan.OpenApi
{
    public enum ApiResultCode
    {
        Success = 0,
        Fail
    }

    public class ApiResult
    {
        public ApiResult() { }
        public ApiResult(string msg)
        {
            Code = ApiResultCode.Fail;
            Message = msg;
        }

        public ApiResult(ApiResultCode code, string msg)
        {
            Code = code;
            Message = msg;
        }

        public ApiResult(ApiResultCode code, object data)
        {
            Code = code;
            Data = data;
        }
        public ApiResult(object data)
        {
            Data = data;
        }

        public ApiResultCode Code { get; set; } = ApiResultCode.Success;
        public string Message { get; set; } = string.Empty;
        public object Data { get; set; }
    }
}
