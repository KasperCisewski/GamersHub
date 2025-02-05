﻿using System;
using System.Collections.Generic;

namespace GamersHub.Api.Domain
{
    public class AuthenticationResult
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public bool Success { get; set; }
        public DateTime TokenExpiryDate { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
