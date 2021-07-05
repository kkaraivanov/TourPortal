namespace TourPortal.Infrastructure.Security.Authorization
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.Extensions.Options;

    using Global.Types;
    using Services;

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
                Security.Policiy.IsAdministrator => (Policies.IsAdministratorPolicy(), true),

                Security.Policiy.IsUser => (Policies.IsUserPolicy(), true),

                Security.Policiy.IsOwner => (Policies.IsOwnerPolicy(), true)
            };

    }
}