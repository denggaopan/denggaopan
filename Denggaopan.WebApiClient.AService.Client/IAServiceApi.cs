using System;
using System.Threading.Tasks;
using WebApiClientCore;
using WebApiClientCore.Attributes;

namespace Denggaopan.WebApiClient.AService.Client
{

    [HttpHost("http://aservice.host/")]
    public interface IAServiceApi : IHttpApi
    {
        [HttpGet("api/health")]
        ITask<string> CheckHealthAsync();
    }
}
