using GamerHub.mobile.core.Services.Base;

namespace GamerHub.mobile.core.Services.Http.Factory
{
    public interface IHttpClientFactoryService
    {
        IHttpClientService GetAuthorizedClient();
        HttpClientService GetNotAuthorizedClient();
    }
}