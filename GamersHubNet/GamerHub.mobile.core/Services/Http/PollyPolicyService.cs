using System;
using Polly;
using RestSharp;

namespace GamerHub.mobile.core.Services.Http
{
    public class PollyPolicyService : IPollyPolicyService
    {
        public IAsyncPolicy GetHttpRequestPolicy(IRestRequest request)
        {
            return request.Method == Method.GET ? Policy.Handle<Exception>().RetryAsync(3) : (IAsyncPolicy)Policy.NoOpAsync();
        }
    }
}
