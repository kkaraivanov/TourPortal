namespace TourPortal.Infrastructure.Security.Authorization
{
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
    }
}