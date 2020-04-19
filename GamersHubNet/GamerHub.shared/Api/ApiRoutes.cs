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
            public const string GamesRoot = Root + "/games";
            public const string GetHomeScreenGames = GamesRoot + "/home";
            public const string AddGameToWishList = GamesRoot + "/wishlist";
            public const string AddGameToVault = GamesRoot + "/vault";
            public const string GetGameScreenshots = GamesRoot + "/screenshots";
            public const string GetVideoUrl = GamesRoot + "/video";
            public const string GetGamesByCategory = GamesRoot + "/filter-category";
            public const string GetFullGameDescription = GamesRoot + "/description";
            public const string DeleteGameFromVault = GamesRoot + "/vault";
            public const string DeleteGameFromWishList = GamesRoot + "/wishlist";
        }

        public static class Profile
        {
            public const string ProfileRoot = Root + "/profile";
            public const string GetUserFriends = ProfileRoot + "/friends";
            public const string GetGamesInVault = ProfileRoot + "/vault";
            public const string GetWishListGames = ProfileRoot + "/wishlist";
            public const string GetHeatMap = ProfileRoot + "/heatmap";
            public const string ChangeProfileImage = ProfileRoot + "/profileImage";

            public const string UserFriendsRoot = Root + "/friends";

            public const string ProfileGamesRoot = ProfileRoot + "/games";
            public const string GetUserGenres = ProfileGamesRoot + "/genres";
            public const string GetUserGamesNames = ProfileGamesRoot + "/names";
            public const string GetRecommendedGames = ProfileGamesRoot + "/recommended";
        }
    }
}
