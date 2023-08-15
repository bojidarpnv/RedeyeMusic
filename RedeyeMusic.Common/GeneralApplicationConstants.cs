namespace RedeyeMusic.Common
{
    public static class GeneralApplicationConstants
    {
        public const int ReleaseYear = 2023;

        public const string AdminAreaName = "Admin";
        public const string AdminRoleName = "Administrator";
        public const string DevelopmentAdminEmail = "admin@redeye.com";

        public const string UsersCacheKey = "UsersCache";
        public const string GenresCacheKey = "GenresCache";
        public const int UsersCacheDurationMinutes = 5;
        public const int GenresCacheDurationMinutes = 10;

        public const string OnlineUsersCookieName = "IsOnline";
        public const int LastActivityBeforeOfflineMinutes = 10;
    }
}