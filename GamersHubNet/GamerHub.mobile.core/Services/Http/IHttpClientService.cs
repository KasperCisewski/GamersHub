using GamerHub.mobile.core.Models.Http;
using RestSharp;
using System.Threading.Tasks;

namespace GamerHub.mobile.core.Services.Http
{
    public interface IHttpClientService
    {
        Task<HttpResult<T>> ExecuteAsync<T>(IRestRequest request);
        Task<HttpResult<object>> ExecuteAsync(IRestRequest request);
    }
}
