namespace GamersHub.Shared.Api
{
    public static class ApiRoutes
    {
        public const string Root = "api";

        public static class Identity
        {
            public const string Login = Root + "/identity/login";

            public const string Register = Root + "/identity/register";

            public const string Refresh = Root + "/identity/refresh";

            public const string UserWithEmailExists = Root + "/identity/userWithEmailExists";

            public const string UserWithUsernameExists = Root + "/identity/userWithUsernameExists";
        }

        public static class Search
        {
            public const string SearchGames = Root + "/search/searchgames";
        }

        public static class GameImages
        {
            public const string AddGameImage = Root + "/images/add";
        }

        public static class Games
        {
            public const string GetGamesForHomeScreen = Root + "/games/getGamesForHomeScreen";
            public const string AddGameToWishList = Root + "/games/addGameToWishList";
            public const string AddGameToVault = Root + "/games/addGameToVault";
            public const string GetScreenshotsForGame = Root + "/games/GetScreenShotsForGame";
            public const string GetPricesForGame = Root + "/games/getPricesForGame";
            public const string GetVideoUrl = Root + "/games/getVideoUrl";
            public const string GetGamesByCategory = Root + "/games/getGamesByCategory";
        }

        public static class Test
        {
            public const string Get = Root + "/test";

            public const string GetAuth = Root + "/test/getAuth";
        }
    }
}
