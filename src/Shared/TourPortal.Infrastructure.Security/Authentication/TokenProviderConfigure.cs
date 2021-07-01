namespace TourPortal.Infrastructure.Security.Authentication
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Security.Principal;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;

    using Storage.Models;

    public static class TokenProviderConfigure
    {
        public static IApplicationBuilder UseJwtBearerTokens(this IApplicationBuilder builder)
        {
            var options = builder.ApplicationServices.GetRequiredService<IOptions<TokenProviderOptions>>();

            builder.JwtNullableValidation(options, GenericPrincipalResolver);
            builder.UseJwtMiddleware(options, GenericPrincipalResolver);
            builder.UseAuthentication();

            return builder;
        }

        private static IApplicationBuilder UseJwtMiddleware(
            this IApplicationBuilder builder,
            IOptions<TokenProviderOptions> options,
            Func<HttpContext, Task<GenericPrincipal>> genericPrincipal) =>
            builder.UseMiddleware<TokenProviderMiddleware>(options, genericPrincipal);

        private static async Task<GenericPrincipal> GenericPrincipalResolver(HttpContext context)
        {
            var userManager = context.RequestServices.GetRequiredService<UserManager<ApplicationUser>>();
            var email = context.Request.Form["email"];
            var user = await userManager.FindByEmailAsync(email);
            
            if (user == null || user.IsDeleted)
            {
                return null;
            }

            var password = context.Request.Form["password"];
            var isValidPassword = await userManager.CheckPasswordAsync(user, password);

            if (!isValidPassword)
            {
                return null;
            }

            var roles = await userManager.GetRolesAsync(user);
            var claims = await userManager.GetClaimsAsync(user);
            var identity = new GenericIdentity(email, "Token");
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
            identity.AddClaims(claims);

            return new GenericPrincipal(identity, roles.ToArray());
        }

        private static IApplicationBuilder JwtNullableValidation(
            this IApplicationBuilder builder,
            IOptions<TokenProviderOptions> options,
            Func<HttpContext, Task<GenericPrincipal>> genericPrincipal)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (options?.Value == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (genericPrincipal == null)
            {
                throw new ArgumentNullException(nameof(genericPrincipal));
            }

            return builder;
        }
    }
}