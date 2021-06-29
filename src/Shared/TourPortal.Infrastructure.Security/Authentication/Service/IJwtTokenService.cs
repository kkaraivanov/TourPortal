namespace TourPortal.Infrastructure.Security.Authentication.Service
{
    using System;
    using System.Security.Principal;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Options;

    public interface IJwtTokenService
    {
        Task BuildToken(HttpContext context);
    }

    public class JwtTokenService : IJwtTokenService
    {
        private readonly Func<HttpContext, Task<GenericPrincipal>> _principalResolver;
        private readonly TokenProviderOptions _options;

        public JwtTokenService(
            IOptions<TokenProviderOptions> options,
            Func<HttpContext, Task<GenericPrincipal>> principalResolver)
        {
            _principalResolver = principalResolver;
            _options = options.Value;
        }

        public async Task BuildToken(HttpContext context)
        {
            
        }
    }
}