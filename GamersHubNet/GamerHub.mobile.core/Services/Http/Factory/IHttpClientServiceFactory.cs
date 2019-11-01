using GamerHub.mobile.core.Services.Base;

namespace GamerHub.mobile.core.Services.Http.Factory
{
    public interface IHttpClientServiceFactory : IService
    {
        IHttpClientService GetAuthorizedClient();
        HttpClientService GetNotAuthorizedClient();
    }
}