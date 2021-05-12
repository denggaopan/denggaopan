using System;
using System.Collections.Generic;
using System.Text;

namespace Denggaopan.OpenApi
{
    public enum ApiResultCode
    {
        Success = 0,
        Fail = 1,
        UnAuth = 2,
        Error = 3,
        NotExisted=4,
        ParamsError=5,
        PasswordError=6,
        Timeout = 7,
    }
}
