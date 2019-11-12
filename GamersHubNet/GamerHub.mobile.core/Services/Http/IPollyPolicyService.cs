using Polly;
using RestSharp;

namespace GamerHub.mobile.core.Services.Http
{
    public interface IPollyPolicyService
    {
        IAsyncPolicy GetHttpRequestPolicy(IRestRequest request);
    }
}
