namespace TourPortal.Server.AuthentivationMiddleware
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using Infrastructure.Security.Authentication;
    using Infrastructure.Security.Authentication.Service;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Options;

    public class JwtTokenProviderMiddleware
    {
        //private readonly RequestDelegate _requestDelegate;
        //private readonly TokenProviderOptions _options;
        //private readonly IJwtTokenService _jwtTokenService;

        //public JwtTokenProviderMiddleware(
        //    RequestDelegate requestDelegate,
        //    IOptions<TokenProviderOptions> options,
        //    IJwtTokenService jwtTokenService)
        //{
        //    _requestDelegate = requestDelegate;
        //    _options = options.Value;
        //    _jwtTokenService = jwtTokenService;
        //}

        //public Task Invoke(HttpContext context)
        //{
        //    if (!context.Request.Path.Equals(_options.Path, StringComparison.Ordinal))
        //    {
        //        return _requestDelegate(context);
        //    }

        //    if (context.Request.Method.Equals("POST") && context.Request.HasFormContentType)
        //    {
        //        return _jwtTokenService.BuildToken(context);
        //    }

        //    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        //    return context.Response.WriteAsync("Bad request");
        //}
    }
}