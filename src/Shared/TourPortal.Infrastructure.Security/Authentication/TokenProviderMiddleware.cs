namespace TourPortal.Infrastructure.Security.Authentication
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Net;
    using System.Security.Claims;
    using System.Security.Principal;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Options;

    using GlobalTypes;

    public class TokenProviderMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly Func<HttpContext, Task<GenericPrincipal>> _principalResolver;
        private readonly TokenProviderOptions _options;

        public TokenProviderMiddleware(
            RequestDelegate requestDelegate,
            IOptions<TokenProviderOptions> options,
            Func<HttpContext, Task<GenericPrincipal>> principalResolver)
        {
            _requestDelegate = requestDelegate;
            _principalResolver = principalResolver;
            _options = options.Value;
        }

        public Task Invoke(HttpContext context)
        {
            if (!context.Request.Path.Equals(_options.Path, StringComparison.Ordinal))
            {
                return _requestDelegate(context);
            }

            if (context.Request.Method.Equals("POST") && context.Request.HasFormContentType)
            {
                return BuildToken(context);
            }

            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return context.Response.WriteAsync("Bad request");
        }

        private async Task BuildToken(HttpContext context)
        {
            var principal = await _principalResolver(context);

            if (principal == null)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsync("Invalid email address or password.");
                return;
            }

            var token = await BuildToken(principal);
            var response = new
            {
                access_token = token.token,
                expires_in = (int)_options.Expiration.TotalMilliseconds,
                roles = token.claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value)
            };

            context.Response.ContentType = ApplicationConstants.JsonContentType;
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));

        }

        private async Task<(string token, List<Claim> claims)> BuildToken(GenericPrincipal principal)
        {
            var utcNow = DateTime.UtcNow;
            var timeSeconds = (long)Math.Round(
                (utcNow.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
            var claims = principal.Claims.ToList();
            var fwtClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, principal.Identity.Name),
                new Claim(JwtRegisteredClaimNames.Jti, await _options.UniqueIdentifierGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat, timeSeconds.ToString(), ClaimValueTypes.Integer64)
            };

            foreach (var jwtClaim in fwtClaims)
            {
                var fwtClaimIndex = GetClaimIndex(claims, jwtClaim.Type);

                if (fwtClaimIndex < 0)
                {
                    claims.Add(jwtClaim);
                }
                else
                {
                    claims[fwtClaimIndex] = jwtClaim;
                }
            }

            var jwt = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                notBefore: utcNow,
                expires: utcNow.Add(_options.Expiration),
                signingCredentials: _options.SigningCredentials);

            return (new JwtSecurityTokenHandler().WriteToken(jwt), claims);
        }

        private static int GetClaimIndex(List<Claim> claims, string jwtClaimType)
        {
            for (int i = 0; i < claims.Count; i++)
            {
                if (claims[i].Type == jwtClaimType)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}