namespace TourPortal.Server.Test
{
    using System.Text;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using MyTested.AspNetCore.Mvc;

    using Infrastructure.Services;
    using Mocks;

    public class TestStartup : Startup
    {
        public TestStartup(
            IWebHostEnvironment environment,
            IConfiguration configuration) 
            : base(environment, configuration)
        {
            
        }
        public void ConfigureTestServices(IServiceCollection services)
        {
            base.ConfigureServices(services);
            var jwtKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSecurity:JwtSecurityKey"]));
            var expireTime = int.Parse(Configuration["JwtSecurity:JwtExpiryInDays"]);

            services.Replace<IAccountService>(_ =>
                AccountServiceMock.AddUserProfileInstace, ServiceLifetime.Scoped);
        }
    }
}