using GamerHub.mobile.core.Services.Http.Factory;
using GamersHub.Shared.Api;
using GamersHub.Shared.Contracts.Requests;
using GamersHub.Shared.Contracts.Responses;
using Newtonsoft.Json;
using RestSharp;
using System.Threading.Tasks;
using GamerHub.mobile.core.Models.Db;
using GamerHub.mobile.core.Services.Db;

namespace GamerHub.mobile.core.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly IHttpClientFactoryService _httpClientFactoryService;
        private readonly IGlobalStateService _globalStateService;
        private readonly ISqlLiteService _sqlLiteService;

        public AccountService(
            IHttpClientFactoryService httpClientFactoryService,
            IGlobalStateService globalStateService,
            ISqlLiteService sqlLiteService)
        {
            _httpClientFactoryService = httpClientFactoryService;
            _globalStateService = globalStateService;
            _sqlLiteService = sqlLiteService;
        }

        public async Task<bool> LogInUserAsync(string userName, string password)
        {
            var client = _httpClientFactoryService.GetNotAuthorizedClient();

            var request = new RestRequest(ApiRoutes.Identity.Login, Method.POST)
            {
                RequestFormat = DataFormat.Json
            };
            request.AddJsonBody(new UserLoginRequest { Email = userName, Password = password });

            var response = await client.ExecuteAsync<string>(request);

            if (response.Success && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseModel = JsonConvert.DeserializeObject<AuthSuccessResponse>(response.ResponseData);
                _globalStateService.UserData = responseModel;

                _sqlLiteService.SaveCredentials(new UserCredentialsModel
                {
                    RefreshToken = responseModel.RefreshToken,
                    Token = responseModel.Token
                });

                return true;
            }

            return false;
        }

        public async Task<bool> RegisterUser(string userName, string email, string password)
        {
            var client = _httpClientFactoryService.GetNotAuthorizedClient();

            var request = new RestRequest(ApiRoutes.Identity.Register)
            {
                Method = Method.POST,
                RequestFormat = DataFormat.Json
            };
            request.AddJsonBody(new UserRegistrationRequest { Email = email, Password = password, Username = userName });

            var response = await client.ExecuteAsync<string>(request);

            if (response.Success && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseModel = JsonConvert.DeserializeObject<AuthSuccessResponse>(response.ResponseData);
                _globalStateService.UserData.Token = responseModel.Token;
                return true;
            }
            //TODO: obsługa błędów

            return false;
        }

        public async Task<bool> LogInByToken(UserCredentialsModel credentialsStoredInDb)
        {
            var client = _httpClientFactoryService.GetNotAuthorizedClient();

            var request = new RestRequest(ApiRoutes.Identity.Refresh);
            request.Method = Method.POST;
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(new AuthSuccessResponse { Token = credentialsStoredInDb.Token, RefreshToken = credentialsStoredInDb.RefreshToken });

            var response = await client.ExecuteAsync<string>(request);

            if (response.Success && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseModel = JsonConvert.DeserializeObject<AuthSuccessResponse>(response.ResponseData);
                _globalStateService.UserData = responseModel;

                return true;
            }

            return false;
        }

        public async Task<bool> CheckIfNameExist(string name)
        {
            var client = _httpClientFactoryService.GetNotAuthorizedClient();

            var request = new RestRequest(ApiRoutes.Identity.UserWithUsernameExists)
            {
                Method = Method.GET
            };
            request.AddQueryParameter("username", name);

            var response = await client.ExecuteAsync<bool>(request);

            return response.ResponseData;
        }

        public async Task<bool> CheckIfEmailExist(string email)
        {
            var client = _httpClientFactoryService.GetNotAuthorizedClient();

            var request = new RestRequest(ApiRoutes.Identity.UserWithEmailExists)
            {
                Method = Method.GET
            };
            request.AddQueryParameter("email", email);

            var response = await client.ExecuteAsync<bool>(request);

            return response.ResponseData;
        }
    }
}
