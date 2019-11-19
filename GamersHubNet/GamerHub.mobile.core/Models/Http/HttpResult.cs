using System.Collections.Generic;
using System.Net;

namespace GamerHub.mobile.core.Models.Http
{
    public class HttpResult<T>
    {
        public bool Success { get; set; }
        public T ResponseData { get; set; }
        public byte ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public Dictionary<string, string> ErrorData { get; set; }
    }
}
