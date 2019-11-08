using GamerHub.mobile.core.Services.Http.Factory;
using GamersHub.Shared.Api;
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

            var request = new RestRequest(ApiRoutes.Identity.Login);
            request.Method = Method.POST;
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(new UserLoginRequest { Email = userName, Password = password });

            var response = await client.ExecuteAsync(request);

            if (response.Success && response.ResponseData is AuthSuccessResponse result)
            {
                _globalStateService.UserData.Token = result.Token;
                return true;
            }

            return false;
        }

        public async Task<bool> RegisterUser(string userName, string email, string password)
        {
            var client = _httpClientFactoryService.GetNotAuthorizedClient();

            var request = new RestRequest(ApiRoutes.Identity.Register);
            request.Method = Method.POST;
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(new UserRegistrationRequest { Email = email, Password = password, Username = userName });

            var response = await client.ExecuteAsync(request);

            if (response.Success && response.ResponseData is AuthSuccessResponse result)
            {
                _globalStateService.UserData.Token = result.Token;
                return true;
            }

            return false;
        }

        public async Task<bool> CheckIfNameExist(string name)
        {
            var client = _httpClientFactoryService.GetNotAuthorizedClient();

            var request = new RestRequest(ApiRoutes.Identity.UserWithEmailExists);
            request.Method = Method.GET;

            var response = await client.ExecuteAsync<bool>(request);

            return response.ResponseData;
        }

        public async Task<bool> CheckIfEmailExist(string email)
        {
            var client = _httpClientFactoryService.GetNotAuthorizedClient();

            var request = new RestRequest(ApiRoutes.Identity.UserWithUsernameExists);
            request.Method = Method.GET;

            var response = await client.ExecuteAsync<bool>(request);

            return response.ResponseData;
        }
    }
}
