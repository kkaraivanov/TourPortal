using Microsoft.AspNetCore.Authorization;

namespace TourPortal.Infrastructure.Security.Authorization
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.Extensions.Options;

    using Global.Types;

    public class BaseAuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
    {
        private readonly AuthorizationOptions _options;

        public BaseAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options)
            : base(options) =>
            _options = options.Value;

        public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            var policy = await base.GetPolicyAsync(policyName);

            if (policy == null)
            {
                var builder = await AuthorizationBuilder(policyName);
                bool isCreated = builder.isCreated;

                if (isCreated)
                {
                    policy = builder.policy;
                    _options.AddPolicy(policyName, policy);
                }
            }

            return policy;
        }

        private async Task<(AuthorizationPolicy policy, bool isCreated)> AuthorizationBuilder(string policyName) =>
            policyName switch
            {
                Security.Policiy.IsAdministrator => (new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .RequireRole(Security.Role.Administrator)
                    .Build(), true),

                Security.Policiy.IsUser => (new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .RequireRole(new string[] { Security.Role.User, Security.Role.Owner, Security.Role.Employe })
                    .Build(), true),

                Security.Policiy.IsOwner => (new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .RequireAssertion(ctx => 
                        ctx.User.HasClaim(claim => claim.Type == "IsUser") || 
                        (ctx.User.IsInRole(Security.Role.Owner) || 
                        ctx.User.IsInRole(Security.Role.Employe)))
                    .Build(), true)
            };

    }
}