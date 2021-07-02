namespace TourPortal.Infrastructure.GlobalTypes
{
    public static class ApplicationConstants
    {
        public const string JsonContentType = "application/json";
        public const string TokenType = "bearer";
        public const string AuthenticationTokenType = "jwt";
        public const string TokenString = "tourPortalToken";

        public const string LoginUrl = "api/account/Login";

        public const string DefaultAdministratorName = "admin";
        public const string DefaultAdministratorPassword = "Admin!23";
    }
}