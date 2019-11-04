﻿using System.Collections.Generic;
using System.Threading.Tasks;
using GamerHub.mobile.core.Models.Http;
using RestSharp;

namespace GamerHub.mobile.core.Services.Http
{
    public class HttpClientService : IHttpClientService
    {
        private readonly RestClient _restClient;
        private readonly IPollyPolicyService _pollyPolicyService;

        public HttpClientService(
            RestClient restClient,
            IPollyPolicyService pollyPolicyService)
        {
            _restClient = restClient;
            _pollyPolicyService = pollyPolicyService;
        }

        public async Task<HttpResult<T>> ExecuteAsync<T>(IRestRequest request)
        {
            var policy = _pollyPolicyService.GetHttpRequestPolicy(request);
            var result = await policy.ExecuteAndCaptureAsync(async () => (await ExecuteAndThrowIfError<T>(request)));

            if (result.FinalException != null)
            {
                return new HttpResult<T>
                {
                    Success = false,
                    ErrorMessage = result.FinalException.Message,
                    ErrorData = new Dictionary<string, string>()
                };
            }

            return result.Result ?? new HttpResult<T>();
        }

        public async Task<HttpResult<object>> ExecuteAsync(IRestRequest request)
        {
            return await ExecuteAsync<object>(request);
        }

        private async Task<HttpResult<T>> ExecuteAndThrowIfError<T>(IRestRequest request)
        {
            var response = await _restClient.ExecuteTaskAsync<HttpResult<T>>(request);
            if (response.ErrorException != null)
            {
                throw response.ErrorException;
            }

            return response.Data;
        }
    }
}
