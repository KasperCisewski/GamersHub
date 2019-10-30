﻿namespace GamersHub.Api.Contracts
{
    public static class ApiRoutes
    {
        public const string Root = "api";

        public static class Identity
        {
            public const string Login = Root + "/identity/login";

            public const string Register = Root + "/identity/register";

            public const string Refresh = Root + "/identity/refresh";
        }

        public static class Test
        {
            public const string Get = Root + "/test";

            public const string GetAuth = Root + "/test/getAuth";
        }
    }
}

