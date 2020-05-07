using RestSharp;
using RestSharp.Authenticators;

namespace GamerHub.mobile.core.Services.Http.Factory
{
    public class HttpClientFactoryService : IHttpClientFactoryService
    {
        private readonly IPollyPolicyService _pollyPolicyService;
        private readonly IGlobalStateService _globalStateService;

#if DEBUG
        private const string _apiUrl = "https://10.0.2.2:5001/";
#else
        private const string _apiUrl ="https://10.0.2.2:5001/"; //azure
#endif
        //TODO: get url from app settings -> https://www.andrewhoefling.com/Blog/Post/xamarin-app-configuration-control-your-app-settings?fbclid=IwAR1clIxqp4TM1xto0YzJg70duaLT8Tdwt5sY-maEYpvV0TnbiYzoWpShnIk

        public HttpClientFactoryService(
            IPollyPolicyService pollyPolicyService,
            IGlobalStateService globalStateService)
        {
            _pollyPolicyService = pollyPolicyService;
            _globalStateService = globalStateService;
        }

        public IHttpClientService GetHttpClient()
        {
            var client = new RestClient(_apiUrl);

            if (_globalStateService.UserData != null && !string.IsNullOrWhiteSpace(_globalStateService.UserData?.Token))
            {
                client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(_globalStateService.UserData.Token, "Bearer");

                client.AddDefaultHeader("Authorization", $"Bearer {_globalStateService.UserData.Token}");
            }

            return new HttpClientService(client, _pollyPolicyService);
        }
    }
}
