using System.Collections.Generic;

namespace GamerHub.mobile.core.Models.Http
{
    public class HttpResult<T>
    {
        public bool Success { get; set; }
        public T ResponseData { get; set; }
        public byte ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public Dictionary<string, string> ErrorData { get; set; }
    }
}
