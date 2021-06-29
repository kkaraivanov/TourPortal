﻿namespace TourPortal.Infrastructure.Security.Authentication.Service
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Net;
    using System.Security.Claims;
    using System.Security.Principal;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Options;

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
            var principal = await _principalResolver(context);

            if (principal == null)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsync("Invalid email address or password.");
                return;
            }

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
        }

        private int GetClaimIndex(List<Claim> claims, string jwtClaimType)
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