namespace GamersHub.Api.Controllers
{
    public static class ApiRoutes
    {
        public const string Root = "api";

        public static class Identity
        {
            public const string Login = Root + "/identity/login";

            public const string Register = Root + "/identity/register";

            public const string Refresh = Root + "/identity/refresh";

            public const string UserWithEmailExists = Root + "/identity/userWithEmailExists/{email}";

            public const string UserWithUsernameExists = Root + "/identity/userWithUsernameExists/{username}";
        }

        public static class Test
        {
            public const string Get = Root + "/test";

            public const string GetAuth = Root + "/test/getAuth";
        }
    }
}

