using RestSharp;
using RestSharp.Authenticators;

namespace GamerHub.mobile.core.Services.Http.Factory
{
    public class HttpClientServiceFactory : IHttpClientServiceFactory
    {
        private readonly IPollyPolicyService _pollyPolicyService;
        private readonly IGlobalStateService _globalStateService;
        private string _apiUrl; // get from app settings

        public HttpClientServiceFactory(
            IPollyPolicyService pollyPolicyService,
            IGlobalStateService globalStateService)
        {
            _pollyPolicyService = pollyPolicyService;
            _globalStateService = globalStateService;
        }

        public IHttpClientService GetAuthorizedClient()
        {
            var client = new RestClient(_apiUrl)
            {
                Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(_globalStateService.UserData.Token, "Bearer")
            };

            return new HttpClientService(client, _pollyPolicyService);
        }


        public HttpClientService GetNotAuthorizedClient()
        {
            return new HttpClientService(new RestClient(_apiUrl), _pollyPolicyService);
        }
    }
}
