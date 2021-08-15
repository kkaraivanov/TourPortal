namespace TourPortal.Infrastructure.Services
{
    using Microsoft.AspNetCore.Authorization;

    using Global.Types;

    public static class Policies
    {
        public static AuthorizationPolicy IsAdministratorPolicy() => 
            new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .RequireRole(Security.Role.Administrator)
                .Build();

        public static AuthorizationPolicy IsUserPolicy() =>
            new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .RequireRole(new string[]
                {
                    Security.Role.User, 
                    Security.Role.Owner, 
                    Security.Role.Employe
                })
                .Build();

        public static AuthorizationPolicy IsOwnerPolicy() =>
            new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .RequireRole(Security.Role.Owner)
                .Build();
    }
}