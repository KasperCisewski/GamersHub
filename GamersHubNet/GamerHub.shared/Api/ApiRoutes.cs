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
            public const string SearchRoot = Root + "/search";
            public const string SearchGames = SearchRoot + "/games";
            public const string SearchUsers = SearchRoot + "/users";
        }

        public static class Games
        {
            public const string GetHomeScreenGames = Root + "/games/home";
            public const string AddGameToWishList = Root + "/games/addGameToWishList";
            public const string AddGameToVault = Root + "/games/addGameToVault";
            public const string GetGameScreenshots = Root + "/games/screenshots";
            public const string GetGameOffers = Root + "/games/offers";
            public const string GetVideoUrl = Root + "/games/video";
            public const string GetGamesByCategory = Root + "/games/getGamesByCategory";
            public const string GetFullGameDescription = Root + "/games/description";
            public const string GetGamesForUser = Root + "/games/getGamesForUser";
            public const string DeleteGameFromVault = Root + "/games/deleteGameFromVault";
            public const string DeleteGameFromWishList = Root + "/games/deleteGameFromWishList";
        }

        public static class Profile
        {
            public const string ProfileRoot = Root + "/profile";
            public const string GetUserFriends = ProfileRoot + "/friends";
            public const string GetGamesInVault = ProfileRoot + "/vault";
            public const string GetWishListGames = ProfileRoot + "/wishlist";
            public const string GetHeatMap = ProfileRoot + "/heatmap";

            public const string UserFriendsRoot = Root + "/friends";

            public const string ProfileGamesRoot = ProfileRoot + "/games";
            public const string GetUserGenres = ProfileGamesRoot + "/genres";
            public const string GetUserGamesNames = ProfileGamesRoot + "/names";
            public const string GetRecommendedGames = ProfileGamesRoot + "/recommended";

        }
    }
}
