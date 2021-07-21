namespace TourPortal.Infrastructure.Global
{
    public static class Global
    {
        public static class Routes
        {
            public const string Login = "api/account/login";
            public const string Register = "api/account/register";
            public const string GetRoles = "api/account/getUserRoles";
            public const string GetUserInfo = "api/account/getUserInfo?userEmail=";
        }
    }
}