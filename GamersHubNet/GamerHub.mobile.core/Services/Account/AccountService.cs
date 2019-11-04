using GamerHub.mobile.core.Services.Http.Factory;
using GamersHub.Shared.Contracts.Requests;
using GamersHub.Shared.Contracts.Responses;
using RestSharp;
using System.Threading.Tasks;

namespace GamerHub.mobile.core.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly IHttpClientFactoryService _httpClientFactoryService;
        private readonly IGlobalStateService _globalStateService;

        public AccountService(
            IHttpClientFactoryService httpClientFactoryService,
            IGlobalStateService globalStateService)
        {
            _httpClientFactoryService = httpClientFactoryService;
            _globalStateService = globalStateService;
        }

        public async Task<bool> LogInUserAsync(string userName, string password)
        {
            var client = _httpClientFactoryService.GetNotAuthorizedClient();

            var request = new RestRequest("identity/login");
            request.Method = Method.POST;
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(new UserLoginRequest { Email = userName, Password = password });

            var response = await client.ExecuteAsync(request);

            if (response.Success && response.ResponseData is AuthSuccessResponse)
            {
                var result = response.ResponseData as AuthSuccessResponse;
                _globalStateService.UserData.Token = result.Token;
                return true;
            }

            return false;
        }
    }
}

