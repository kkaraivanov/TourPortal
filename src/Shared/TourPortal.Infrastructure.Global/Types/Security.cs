namespace TourPortal.Infrastructure.GlobalTypes
{
    public class Security
    {
        public const string Permision = "permission";

        public static class Role
        {
            public const string Administrator = "Administrator";
            public const string User = "User";
            public const string Owner = "Owner";
            public const string Employe = "Employe";
        }

        public static class Policiy
        {
            public const string IsAdministrator = "IsAdministrator";
            public const string IsUser = "IsUser";
            public const string IsOwner = "IsOwner";
        }
    }
}
