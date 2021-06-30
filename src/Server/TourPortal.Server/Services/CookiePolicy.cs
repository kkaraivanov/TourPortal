namespace TourPortal.Server.Services
{
    using Microsoft.Extensions.DependencyInjection;

    public static class CookiePolicy
    {
        public static IServiceCollection SetCookiePolicy(this IServiceCollection services)
        {
            return services;
        }
    }
}