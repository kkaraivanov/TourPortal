namespace TourPortal.Infrastructure.Global
{
    public static class ApplicationConstants
    {
        public const string JsonContentType = "application/json";
        public const string TokenType = "bearer";
        public const string AuthenticationTokenType = "jwt";
        public const string IsAuthenticationString = "authUserMail";
        public const string AuthenticatedTokenString = "tourPortalToken";

        public const string LoginUrl = "api/account/Login";
        public const string RegisterUrl = "api/account/register";

        public const string DefaultAdministratorName = "admin";
        public const string DefaultAdministratorPassword = "Admin!23";
    }
}