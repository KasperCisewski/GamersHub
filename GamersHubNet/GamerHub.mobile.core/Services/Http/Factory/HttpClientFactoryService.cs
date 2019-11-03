using RestSharp;
using RestSharp.Authenticators;
using System;

namespace GamerHub.mobile.core.Services.Http.Factory
{
    public class HttpClientFactoryService : IHttpClientFactoryService
    {
        private readonly IPollyPolicyService _pollyPolicyService;
        private readonly IGlobalStateService _globalStateService;
        private string _apiUrl ="http://10.0.2.2:5000/api/";
 // get url from app settings

        public HttpClientFactoryService(
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
