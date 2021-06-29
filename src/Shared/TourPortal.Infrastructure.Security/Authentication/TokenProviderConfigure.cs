namespace TourPortal.Infrastructure.Security.Authentication
{
    using System;
    using System.Security.Claims;
    using System.Security.Principal;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;

    using Service;

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
            builder.UseMiddleware<JwtTokenService>(options, genericPrincipal);

        private static async Task<GenericPrincipal> GenericPrincipalResolver(HttpContext context)
        {
            // TODO: Add ClaimsIdentity logic and roles
            return new GenericPrincipal(new ClaimsIdentity(), new string[] { "reole" });
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