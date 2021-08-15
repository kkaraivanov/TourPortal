namespace TourPortal.Infrastructure.Security.Authorization
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Options;

    using Storage.Models;

    public class AdditionalUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, ApplicationRole>
    {
        public AdditionalUserClaimsPrincipalFactory(
            UserManager<ApplicationUser> userManager, 
            RoleManager<ApplicationRole> roleManager, 
            IOptions<IdentityOptions> options) 
            : base(userManager, roleManager, options)
        {

        }

        public override async Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
        {
            var principal = await base.CreateAsync(user);
            var identity = (ClaimsIdentity)principal.Identity;

            if (!string.IsNullOrWhiteSpace(user.FirstName))
            {
                identity.AddClaims(new[] { new Claim(ClaimTypes.GivenName, user.FirstName) });
            }

            if (!string.IsNullOrWhiteSpace(user.LastName))
            {
                identity.AddClaims(new[] { new Claim(ClaimTypes.Surname, user.LastName) });
            }

            if (!string.IsNullOrWhiteSpace(user.Email))
            {
                identity.AddClaims(new[] { new Claim(ClaimTypes.Email, user.Email) });
            }

            return principal;
        }
    }
}