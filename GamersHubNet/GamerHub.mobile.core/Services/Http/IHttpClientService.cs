using GamerHub.mobile.core.Models.Http;
using GamerHub.mobile.core.Services.Base;
using RestSharp;
using System.Threading.Tasks;

namespace GamerHub.mobile.core.Services.Http
{
    public interface IHttpClientService : IService
    {
        Task<HttpResult<T>> ExecuteAsync<T>(IRestRequest request);
        Task<HttpResult<object>> ExecuteAsync(IRestRequest request);
    }
}
