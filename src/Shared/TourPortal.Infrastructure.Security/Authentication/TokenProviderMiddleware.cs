namespace TourPortal.Infrastructure.Security.Authentication
{
    using System;
    using System.Net;
    using System.Security.Principal;
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
                return context.BuildToken(_principalResolver, _options);
            }

            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return context.Response.WriteAsync("Bad request");
        }
    }
}