namespace TourPortal.Infrastructure.Security.Authentication.Service
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    public interface IJwtTokenService
    {
        Task BuildToken(HttpContext context);
    }
}