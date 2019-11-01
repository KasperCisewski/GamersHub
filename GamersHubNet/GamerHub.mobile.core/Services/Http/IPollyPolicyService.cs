using GamerHub.mobile.core.Services.Base;
using Polly;
using RestSharp;

namespace GamerHub.mobile.core.Services.Http
{
    public interface IPollyPolicyService : IService
    {
        IAsyncPolicy GetHttpRequestPolicy(IRestRequest request);
    }
}
